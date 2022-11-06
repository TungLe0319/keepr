<template>
  <div class="modal-body p-0 bg-warning" v-if="keep">
    <div class="row">
      <div class="col-md-6 pictureCol">
        <img
          :src="keep.img"
          alt=""
          title="keep"
          class="rounded-start img-fluid"
        />
      </div>
      <div class="col-md-6 d-flex flex-column justify-content-between ">
        <div class="d-flex justify-content-center mt-2 align-items-center">
          <i class="mdi mdi-eye">{{ keep.views }}</i>
          <i class="mdi mdi-fingerprint">{{ keep.kept}}</i>
          

          <div class="dropdown">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-3"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu">
              <li>
                <a
                  class="dropdown-item"
                  data-bs-toggle="modal"
                  data-bs-target="#createForm"
                  >Edit</a
                >
              </li>
              <li><a class="dropdown-item" @click="deleteKeep()">Delete</a></li>
            </ul>
          </div>
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
       <AddToVault/>
              <!-- ----------------------NOTE ADD TO VAULT---------------------------------- -->
            </div>
            <span class="m-2 d-flex">
              <router-link :to="{ name: 'Profile', params: { id: keep.id } }">
                <img
                :src="keep.creator.picture"
                :alt="keep.creator.name"
                class="pImg"
                data-bs-dismiss="modal"
                />
              </router-link>
              <h5 class="ms-3">{{keep.creator.name.split("@")[0]}}</h5>
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
            }
            catch (error) {
                Pop.error(error, "[]");
            }
        }
        onMounted(() => { });
        watchEffect(() => { });
        const editable = ref({});
        return {
            editable,
            account: computed(() => AppState.account),
            user: computed(() => AppState.user),
            async deleteKeep() {
                try {
                    const yes = await Pop.confirm();
                    if (!yes) {
                        return;
                    }
                    await keepsService.deleteKeep(props.keep.id);
                    Pop.success(`${props.keep.name} deleted`);
                }
                catch (error) {
                    Pop.error(error, "[deleteKeep]");
                }
            },
        };
    },
    components: { AddToVault }
};
</script>

<style lang="scss" scoped></style>
