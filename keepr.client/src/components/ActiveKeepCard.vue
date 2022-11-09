<template>
  <div class="modal-body p-0 bg-warning" v-if="keep">
    <div class="row">
      <!--  :style="{backgroundImage:   `url(${keep?.img})`}" -->
      <div class="col-md-6 animate__animated animate__fadeIn">
        <img
          :src="keep?.img"
          alt=""
          title="keep"
          class="rounded img-fluid h-100 image"
        />
      </div>
      <div class="col-md-6 d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-center mt-2 align-items-center">
          <div class="me-3">
            <ShareCard />
          </div>
          <div class="d-flex" title="View count for this keep">
            <i class="mdi mdi-eye fs-2"></i>
            <h5
              class="ms-2 align-items-center d-flex animate__animated animate__fadeInDown"
            >
              {{ keep?.views }}
            </h5>
          </div>
          <div
            class="ms-4 d-flex"
            title="Count of vaults this keep is apart of"
          >
            <img
              v-if="!account?.theme"
              src="https://cdn-icons-png.flaticon.com/512/3564/3564077.png"
              alt=""
              width="30"
              height="30"
            />
            <i v-else class="mdi mdi-alpha-k fs-1"></i>
            <h5 class="ms-1 animate__animated animate__fadeInDown">
              {{ keep?.kept }}
            </h5>
          </div>
          <!-- Add to vault -->
          <div class="btn-group dropend bg-transparent" v-if="creator">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-2 dotHover rounded px-1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li class="dotHover rounded p-1">
                <a
                  class="dropdown-item rounded bg-info"
                  data-bs-toggle="modal"
                  data-bs-target="#createForm"
                  @click="toggleEditForm()"
                  >Edit</a
                >
              </li>
              <li class="dotHover rounded p-1">
                <a class="dropdown-item rounded bg-info" @click="deleteKeep()"
                  >Delete</a
                >
              </li>
            </ul>
          </div>
          <!-- ------------- -->
        </div>

        <div>
          <div
            class="px-5 justify-content-center d-flex flex-column animate__animated animate__fadeIn"
          >
            <h2 class="text-center my-4 md-my-2 markoOne keepName">
              {{ keep?.name }}
            </h2>
            <p>
              {{ keep?.description }}
            </p>
          </div>
        </div>
        <div>
          <div class="d-flex justify-content-between">
            <div class="d-flex animate__animated animate__fadeInLeft">
              <!-- ----------------------NOTE ADD TO VAULT---------------------------------- -->
              <AddToVault />
              <!-- ----------------------NOTE ADD TO VAULT---------------------------------- -->
            </div>

            <div class="dropdown">
              <button
                class="btn p-0 dotHover me-1 rounded border-0"
                type="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <span
                  class="m-2 d-flex align-items-center animate__animated animate__fadeInRight"
                >
                  <img
                    :src="keep?.creator?.picture"
                    :alt="keep?.creator?.name"
                    class="pImg"
                  />
                  <h5 class="ms-2 d-flex align-items-center text-dark">
                    {{ keep?.creator?.name.split("@")[0] }}
                  </h5>
                </span>
              </button>
              <ul class="dropdown-menu p-0 bg-info border-0 bShadow">
                <li
                  data-bs-dismiss="modal"
                  class="-rounded bg-info text-center selectable p-0 border-0 dotHover rounded"
                  @click="pushToProfile()"
                >
                  <h4 class="bg-info" v-if="keep?.creator?.id != account?.id">
                    Visit Profile
                  </h4>
                  <h5 class="bg-info rounded mb-0 p-1" v-else>Go to Account</h5>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { onMounted, ref, watchEffect } from "vue";
import { useRouter } from "vue-router";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import AccountVaultList from "./AccountVaultList.vue";
import AddToVault from "./AddToVault.vue";
import ShareCard from "./ShareCard.vue";
export default {
  props: {
    keep: { type: Keep, required: true },
  },
  setup(props) {
    onMounted(() => {});
    watchEffect(() => {});
    const editable = ref({});
    const router = useRouter();
    return {
      editable,
      router,

      saved: computed(() =>
        AppState.vKeepIds.find((v) => v.creatorId == AppState.account?.id)
      ),
      vaulted: computed(() =>
        AppState.vKeepIds.find((v) => v.keepId == AppState.activeKeep?.id)
      ),
      account: computed(() => AppState.account),
      creator: computed(
        () => AppState.account?.id == AppState.activeKeep.creator?.id
      ),
      user: computed(() => AppState.user),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep?.id);
            Modal.getOrCreateInstance("#activeKeep").hide();
            Pop.success(`${props.keep.name} deleted`);
          }
        } catch (error) {
          Pop.error(error, "[deleteKeep]");
        }
      },
      toggleEditForm() {
        AppState.keepEditForm = true;
        // console.log(AppState.keepEditForm);
      },
      pushToProfile() {
        Modal.getOrCreateInstance("#activeKeep").hide();
        if (this.creator) {
          router.push({ name: "Account" });
          document.documentElement.scrollTop = 0;
        } else
          router.push({
            name: "Profile",
            params: { id: props.keep?.creator?.id },
          });
        document.documentElement.scrollTop = 0;
      },
    };
  },
  components: { AddToVault, AccountVaultList },
};
</script>

<style lang="scss" scoped>
.modal-body {
  --animate-duration: 500ms;
  --animate-delay: 1s;
}
.activeImg {
  width: 50px;
}
.pictureCol {
  height: 600px;
  background-position: center;
  background-size: cover;
  border-top-left-radius: 4px;
  border-bottom-left-radius: 4px;
}

.collapse {
  height: 800px;
}
.image {
  object-fit: cover;
}
.test {
}
.block {
  width: 100%;
  position: absolute;
  bottom: 0px;
  top: 0px;
  box-shadow: inset -10px -10px 10px 20px rgba(160, 151, 151, 0.281);
}
.vaulted {
  position: absolute;
  right: -40px;
  top: -50px;

  transition: all 0.5s ease;
}
.vaulted:hover {
  z-index: auto;
  transition: all 0.5s ease;
}

@media only screen and (max-width: 768px) {
  .keepName {
    margin-top: 10px;
  }
  .pictureCol {
  }
  .image {
    border-bottom-left-radius: 0px !important;
    border-bottom-right-radius: 0px !important;
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
    height: 300px !important;
    width: 400px !important;
    object-fit: cover;
  }
}
</style>
