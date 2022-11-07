<template>
  <div class="modal-body">
  <form @submit.prevent="handleSubmit" class="">
    <div class="row">
      <div class="col-md-6 ">
        <div class="inputBox mt-2">
          <label for="">Name</label>
          <input
            type="text"
            v-model="editable.name"
            placeholder="name"
            class=""
          />
        </div>
        <div class="inputBox my-4">
          <label for="">Email</label>

          <input
            type="text"
            v-model="editable.email"
            placeholder="name"
            class=""
          />
        </div>

        <div class="inputBox mt-2">
          <label for="">Profile Picture</label>
          <input
            type="url"
            v-model="editable.picture"
            placeholder="url..."
            class=""
          />
         
        </div>
          <div class="mt-3 inputBox">
            <label for=""> Cover Image</label>
          <input type="url" v-model="editable.coverImg" class=" " />
        </div>
      </div>

      <div class="col-md-6 justify-content-center d-flex">
        <img
        v-if="editable.picture"
          :src="editable.picture"
          alt=""
          class="img-fluid profilePicture  bShadow rounded-circle"
        />
            <img
        v-else
          :src="account.picture"
          alt=""
          class="img-fluid w-50 forcedImg bShadow rounded"
        />
        
      </div>

    
      <div class="col-md-6 text-center mt-3 ">

          <img
        v-if="editable.coverImg"
            :src="editable.coverImg"
            alt=""
            class="forcedImg smallerImg mt-2 bShadow rounded"
          />
          <img
          v-else
            :src="account.coverImg"
            alt=""
            class="forcedImg  mt-2 img-fluid bShadow rounded"
          />
 
      
      </div>

      <div class="col-md-6">
        
      </div>
    </div>



    <div class="my-3">
      <button class="btn btn-success selectable" type="submit">
        Edit Account
      </button>
    </div>
  </form>
  </div>

</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
export default {
  props: {},
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {
      editable.value = { ...AppState.account };
    });

    return {
      editable,
      account: computed(() => AppState.account),
      async handleSubmit() {
        try {
          await accountService.editProfile(editable.value);
          Pop.success("Account Edit Approved");
        } catch (error) {
          Pop.error(error, [""]);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>

.forcedImg{
  height: 300px;
  width: auto;
  object-fit: cover;
}

.profilePicture{
  user-select: none;
  margin-top: 30px;
  width: 300px;
  height: 300px;
}

//when screen is 768px OR LESS
@media only screen and (max-width: 768px){
.forcedImg{
  height: 200px;
  width: auto;
  object-fit: cover;
}

.profilePicture{
  user-select: none;
  margin-top: 30px;
  width: 150px;
  height: 150px;
}
}

</style>
