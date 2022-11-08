<template>
  <div class="modal-body position-relative bg-warning " >
    <div class="position-absolute bottom-100 end-50 markoOne ">
      <h1 class="formTitle" v-if="editForm">EDIT </h1>
      <h1 class="formTitle" v-else>Create</h1>
    </div>
<div class="row">
  <div class="col-md-6">
        <form @submit.prevent="editForm==true? handleEdit(): handleSubmit()" class="">
      <div class="mt-3 inputBox">
        <label for="name">Name</label>
        <input type="text" required name="name" v-model="editable.name" />
      </div>
      <div class="mt-3 inputBox">
        <label for="img">Image</label>
        <input type="url" required name="img" v-model="editable.img" />
      </div>
      <div class="mt-3 ">
        <label for="description">Description</label>
        <div class="form-floating">
  <textarea    v-model="editable.description" class="form-control " placeholder="describe your vault" style="height: 100px"></textarea>

</div>
        
      </div>
      <div class="my-3">
        <button
          class="btn p-2 dotHover"
          type="submit"
          data-bs-dismiss="modal"

        >
        <h6 v-if="editForm" > Submit Edit</h6>
        <h3 v-else> Create Vault</h3>
        </button>
      </div>
    </form>
  </div>
  <div class="col-md-6  d-flex flex-column justify-content-center align-items-center">
      <div
    class="card rounded border-0 my-1 "
 
  >
    <img v-if="editable.img" :src="editable?.img" alt="" title="keep" class="img-fluid  rounded" />
    <img v-else src="https://wallpapercave.com/wp/wp1873707.jpg" alt="" title="keep" class="img-fluid  rounded" />
    <div class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end">
      <span>
        <h5 class="mb-0 fw-bold text-light ms-1 markoOne ">{{ editable?.name }}</h5>
      </span>
    </div>

  </div>
  <!-- <div class="text-dark mt-3">
    <h5 class="markoOne">Description</h5>
   <p>{{editable?.description}}</p>
  </div> -->
  </div>
</div>

  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {},
  setup(props) {
    const editable = ref({});
const testForm = ref({img: "https://partners.wsj.com/xinhua/anhui/wp-content/uploads/sites/304/2018/05/xinhua-anhui1.jpg?resize=1200%2C720",
  description:" Lorem ipsum dolor sit, amet consectetur adipisicing elit. Cumque error animi quasi, a distinctio, nulla temporibus delectus minus quis suscipit ducimus, accusamus consequuntur assumenda. Alias.",
  name:"What a beautiful world"})
    onMounted(() => {});
    watchEffect(() => {
    if (AppState.activeKeep) {
      editable.value.img = AppState.activeKeep.img || ''
      editable.value.name = AppState.activeKeep.name || ''
      editable.value.description = AppState.activeKeep.description || ''
    }
    });

    return {
      editable,
editForm : computed(() => AppState.keepEditForm ),
      async handleSubmit(){
        try {

            await keepsService.createKeep(editable.value)
          } catch (error) {
            Pop.error(error,'[createKeep]')
          }
      },
      async handleEdit(){
try {
  editable.value.id = AppState.activeKeep.id
    await keepsService.editKeep(editable.value)
    
  } catch (error) {
    Pop.error(error,'[editKeep]')
  }1
      }
    };
  },
};
</script>

<style lang="scss" scoped>



.card {
  transition: all 0.5s ease;
 box-shadow: rgba(0, 0, 0, 0.4) 0px 2px 4px, rgba(0, 0, 0, 0.3) 0px 7px 13px -3px, rgba(0, 0, 0, 0.2) 0px -3px 0px inset;
}
.cardImg{
   box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
}
.card:hover {
  transform: scale(1.009);
  filter: brightness(95%);
  transition: all 0.25s ease;
  cursor: pointer;
  
box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px, rgba(0, 0, 0, 0.22) 0px 15px 12px;
}
.shimmer:hover {
  -webkit-mask:linear-gradient(-60deg,#000 30%,#0005,#000 70%) right/300% 100%;
  animation: shimmer 2.5s infinite;
  box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px, rgba(0, 0, 0, 0.22) 0px 15px 12px;
}

.bottomhalf:hover{

}

@keyframes shimmer {
  100% {-webkit-mask-position:left}
}
</style>
