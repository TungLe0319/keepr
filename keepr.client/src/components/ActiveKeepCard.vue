<template>
  <div class="modal-body p-0" v-if="keep">
    <div class="row">
      <div class="col-md-6">
        <img
          :src="keep.img"
          alt=""
          title="keep"
          class="rounded-start img-fluid"
        />
      </div>
      <div
        class="col-md-6 d-flex flex-column justify-content-between "
      >
        <div class="d-flex justify-content-center mt-2 align-items-center">
          <i class="mdi mdi-eye  ">{{ keep.views }}</i>
          <i class="mdi mdi-more selectable ms-3 fs-5"></i>
        </div>
        <div>
          <div>
            <h3>{{ keep.name }}</h3>
            <p>
              {{ keep.description }}
            </p>
          </div>
        </div>
        <div>
          <div  class="d-flex justify-content-between">
            <div class="d-flex">
            <div class="btn-group">
  <button type="button" class="btn  dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
  {{keep.name}}
  </button>
  <ul class="dropdown-menu">
    <li><a class="dropdown-item" href="#">Action</a></li>
    <li><a class="dropdown-item" href="#">Another action</a></li>
    <li><a class="dropdown-item" href="#">Something else here</a></li>
    <li><hr class="dropdown-divider"></li>
    <li><a class="dropdown-item" href="#">Separated link</a></li>
  </ul>
</div>
<button class="btn btn-info "> save</button>
            </div>
              <span class="m-2">
                <router-link :to="{name: 'Profile', params:{ id: keep.id }}" > 
                
                  <img
                    :src="keep.creator.picture"
                    :alt="keep.creator.name"
                    class="pImg"
                    data-bs-dismiss="modal"
                  />
                </router-link>
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
    };
  },
};
</script>

<style lang="scss" scoped>
modal-body {
  height: 60vh;
  width: 60vw;
}
</style>
