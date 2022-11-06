<template>
  <div class="modal-body position-relative bg-info" >
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
      <div class="mt-3 inputBox">
        <label for="description">Description</label>
        <input
          type="text"
          required
          name="description"
          v-model="editable.description"
        />
      </div>
      <div class="mt-3 inputBox">
        <label>Tag</label>
         <input
          type="text"
         
          name="instructions"
          v-model="editable.tag"
        />
      </div>
  
      <div class="my-3">
        <button
          class="btn btn-success selectable"
          type="submit"
          data-bs-dismiss="modal"

        >
        <h6 v-if="editForm">Edit</h6>
        <h6 v-else> Create</h6>
        </button>
      </div>
    </form>
  </div>
  <div class="col-md-6 d-flex flex-column justify-content-center align-items-center">
      <div
    class="card rounded border-0 my-1 "
 
  >
    <img v-if="editable.img" :src="editable?.img" alt="" title="keep" class="img-fluid  rounded" />
    <img v-else src="https://dm0qx8t0i9gc9.cloudfront.net/thumbnails/video/H8WuRINimqur8ud/soap-bubbles-zen-symbol-pyramide-from-stones-on-the-beach-at-the-background-of-the-gold-sunset-sea-slow-motion-background-for-spa-relax-tourism-theme-scene-with-zen-pyramide-and-soap-bubbles_bvrit5wx_thumbnail-1080_02.png" alt="" title="keep" class="img-fluid  rounded" />
    <div class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end">
      <span>
        <h5 class="mb-0 fw-bold text-light ms-1">{{ editable?.name }}</h5>
      </span>
    </div>

  </div>
  <div class="text-dark mt-3">
    <h5>Description</h5>
   <p>{{editable?.description}}</p>
  </div>
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
    watchEffect(() => {});

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
.inputBox input {
  width: 100%;
  padding: 10px;
  border: 1px solid #92c5f5;
  border-bottom: 3px solid #c4def7;
  border-radius: 5px;
  outline: none;
  color: rgb(0, 0, 0);
  font-size: 1em;
  background-color: transparent;
  transition: all 1.5s ease;
  box-shadow: 0.25px 0.25px 10px rgba(243, 236, 236, 0.308);
}
.inputBox span {
  position: absolute;
  left: 0;
  bottom: 3em;
  padding: 10px;
  pointer-events: none;
  font-size: 1em;
  text-transform: uppercase;
  color: #f1ecf180;
  transition: all 1s ease;
}
.inputBox input:valid ~ span,
.inputBox input:focus ~ span {
  color: #27132a;
  transform: translateX(10px) translateY(-7px);
  padding: 0 10px;
  font-size: 0.65em;
  border-radius: 4px;
  font-weight: bold;
  background: #92c5f5;
  border-left: 1px solid #92c5f5;
  border-right: 1px solid #92c5f5;
  letter-spacing: 0.2em;
  transition: all 1s ease;
}

.card {
  transition: all 0.5s ease;
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
}
.cardImg{
   box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
}
.card:hover {
  transform: scale(1.01);
  filter: brightness(95%);
  transition: all 0.5s ease;
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
