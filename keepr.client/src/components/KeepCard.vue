<template>
  <div
    class="card rounded border-0 my-1"
    @click="setActive()"
    data-bs-toggle="modal"
    data-bs-target="#activeKeep"
    v-if="keep"
  >
    <img :src="keep?.img" alt="" title="keep" class="img-fluid  rounded" />
    <div class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end">
      <span>
        <p class="mb-0 fw-bold text-light">{{ keep?.name }}</p>
      </span>
      <span>
        <img
          :src="keep?.creator?.picture"
          :alt="keep?.creator?.name"
          class="pImg
          cardImg"
        />
      </span>
    </div>
  </div>
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
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});

    return {
      editable,

      setActive() {
        keepsService.setActive(props.keep);
      },
    };
  },
};
</script>

<style lang="scss" scoped>

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
</style>
