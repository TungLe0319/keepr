<template>
  <div class="modal-body p-0 bg-warning" v-if="keep">
     <div class="vaulted markoOne " v-if="vaulted">
       <div class="d-flex align-items-center">
    
 <img src="https://cdn-icons-png.flaticon.com/512/2489/2489398.png" alt="" width="100" height="100">
      </div>

    </div>
    <div class="row">
      <div class="col-md-6 pictureCol">
        <img
          :src="keep.img"
          alt=""
          title="keep"
          class="rounded-start img-fluid"
        />
      </div>
      <div class="col-md-6 d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-center mt-2 align-items-center">
          <div class="d-flex">
            <img
              src="https://cdn-icons-png.flaticon.com/512/58/58976.png"
              alt=""
              width="30"
              height="30"
            />
            <h5 class="ms-2">
              {{ keep.views }}
            </h5>
          </div>
          <div class="ms-4 d-flex">
            <img
              src="https://cdn-icons-png.flaticon.com/512/3564/3564077.png"
              alt=""
              width="30"
              height="30"
            />
            <h5 class="ms-1">
              {{ keep.kept }}
            </h5>
          </div>
          <!-- Add to vault -->
          <div class="btn-group dropend bg-transparent" v-if="creator">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-2"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li class="dotHover rounded">
                <a
                  class="dropdown-item rounded bg-info"
                  data-bs-toggle="modal"
                  data-bs-target="#createForm"
                  @click="toggleEditForm()"
                  >Edit</a
                >
              </li>
              <li class="dotHover rounded">
                <a class="dropdown-item rounded bg-info" @click="deleteKeep()"
                  >Delete</a
                >
              </li>
            </ul>
          </div>
          <!-- ------------- -->
        </div>
        <div>
          <div class="px-5 justify-content-center d-flex flex-column">
            <h2 class="text-center mb-2 markoOne">{{ keep.name }}</h2>
            <p>
              {{ keep.description }}
            </p>
          </div>
        </div>
        <div>
          <div class="d-flex justify-content-between">
            <div class="d-flex">
              <!-- ----------------------NOTE ADD TO VAULT---------------------------------- -->
              <AddToVault />
              <!-- ----------------------NOTE ADD TO VAULT---------------------------------- -->
            </div>
            <span class="m-2 d-flex align-items-center">
              <router-link
                :to="{ name: 'Profile', params: { id: keep.creator.id } }"
              >
                <img
                  :src="keep.creator.picture"
                  :alt="keep.creator.name"
                  class="pImg"
                  data-bs-dismiss="modal"
                />
              </router-link>
              <h5 class="ms-2 d-flex align-items-center">
                {{ keep.creator.name.split("@")[0] }}
              </h5>
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div v-else></div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import AddToVault from "./AddToVault.vue";

export default {
  props: {
    keep: { type: Keep, required: true },
  },
  setup(props) {
    async function getById() {
      try {
        await keepsService.getById(props.keep.id);
      } catch (error) {
        Pop.error(error, "[]");
      }
    }
    onMounted(() => {});
    watchEffect(() => {});
    const editable = ref({});
    return {
      editable,
      saved: computed(() =>
        AppState.vKeepIds.find((v) => v.creatorId == AppState.account.id)
      ),
       vaulted: computed(() =>
        AppState.vKeepIds.find((v) => v.keepId == AppState.activeKeep.id)
      ),
      account: computed(() => AppState.account),
      creator: computed(
        () => AppState.account.id == AppState.activeKeep.creator.id
      ),
      user: computed(() => AppState.user),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id);
            Modal.getOrCreateInstance("#activeKeep").hide();
            Pop.success(`${props.keep.name} deleted`);
          }
        } catch (error) {
          Pop.error(error, "[deleteKeep]");
        }
      },
      toggleEditForm() {
        AppState.keepEditForm = true;
        console.log(AppState.keepEditForm);
      },
    };
  },
  components: { AddToVault },
};
</script>

<style lang="scss" scoped>
.block {
  width: 100%;
  position: absolute;
  bottom: 0px;
  top: 0px;
  box-shadow: inset -10px -10px 10px 20px rgba(160, 151, 151, 0.281);
}
.vaulted{
  position: absolute;
  right: -40px;
  top:-50px;

  transition: all 0.5s ease;
}
.vaulted:hover{
  z-index: auto;
  transition: all 0.5s ease;
}

@media only screen and (max-width: 68px) {
  .pictureCol img {
    border-radius: 0;
  }
}

</style>
