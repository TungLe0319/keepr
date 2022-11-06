<template>
  <div class="modal-body">
  <form @submit.prevent="handleSubmit" class="">
    <div class="row">
      <div class="col-md-6 ">
        <div class="inputBox mt-2">
          <input
            type="text"
            v-model="editable.name"
            placeholder="name"
            class=""
          />
          <span>ProfileName</span>
        </div>
        <div class="inputBox my-4">
          <input
            type="text"
            v-model="editable.email"
            placeholder="name"
            class=""
          />
          <span>email</span>
        </div>

        <div class="inputBox mt-2">
          <input
            type="url"
            v-model="editable.picture"
            placeholder="url..."
            class=""
          />
          <span>profile picture</span>
        </div>
          <div class="mt-3 inputBox">
          <input type="url" v-model="editable.coverImg" class=" " />
          <span>CoverImg</span>
        </div>
      </div>

      <div class="col-md-6 justify-content-center d-flex">
        <img
        v-if="editable.picture"
          :src="editable.picture"
          alt=""
          class="img-fluid w-50 forcedImg  bShadow rounded"
        />
            <img
        v-else
          :src="account.picture"
          alt=""
          class="img-fluid w-50 forcedImg bShadow rounded"
        />
        
      </div>

    
      <div class="col-md-12 text-center mt-3 ">
   <div><h3>CoverImg</h3> </div>
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
}</style>
