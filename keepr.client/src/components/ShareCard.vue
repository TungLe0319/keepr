<template>
  <button
    class="btn dotHover p-0 px-2 position-relative"
    type="button"
    data-bs-toggle="collapse"
    data-bs-target="#collapseShare"
    aria-expanded="false"
    aria-controls="collapseExample"
  >
    <i class="mdi mdi-share-variant-outline fs-3"></i>
  </button>

  <div
    class="collapse position-absolute end-50 bottom-100 z-index-top"
    id="collapseShare"
  >
    <div class="card card-body">
      <div class="d-flex p-2">
        <img
          class="dotHover rounded-circle"
          src="https://cdn-icons-png.flaticon.com/512/1377/1377257.png"
          alt="pinterest icon"
          height="40"
          width="40"
          title="share to pinterest"
        />
        <img
          class="dotHover rounded-5"
          src="https://cdn-icons-png.flaticon.com/512/2504/2504903.png"
          alt="facebook icon"
          height="40"
          width="40"
          title="share to facebook"
        />
        <img
          class="dotHover rounded-4"
          src="https://cdn-icons-png.flaticon.com/512/1409/1409946.png"
          alt="instagram icon"
          height="40"
          width="40"
          title="share to instagram"
        />

        <img
          class="dotHover rounded-circle"
          src="https://cdn-icons-png.flaticon.com/512/552/552486.png"
          alt="email icon"
          height="40"
          width="40"
          title="share through email"
        />
      </div>
      <div class="border-bottom border-3 border-dark"></div>
      <div class="d-flex p-2">
        <img
          class="dotHover rounded-circle"
          src="https://cdn-icons-png.flaticon.com/512/3541/3541854.png"
          alt="link icon"
          height="40"
          width="40"
          title="copy link"
          @click="copyLink()"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {},
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});

    return {
      editable,
      copyLink() {
        AppState.activeKeep.img;
        navigator.clipboard.writeText(AppState.activeKeep.img);

        // Pop.toast(`${AppState.activeKeep.img}`)
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
