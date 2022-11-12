<template>
  <div
    class="card rounded-4  border-0 "
    @click="setActive()"
    data-bs-toggle="modal"
    data-bs-target="#activeKeep"
    v-if="keep"
  >
    <img
      v-if="keep?.img"
      :src="keep?.img"
      alt=""
      title="keep"
      class="img-fluid rounded-4"
    />
    <div
      class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end"
    >
      <span>
        <p
          class="mb-0 mb-2 ms-2 fs-4 fw-bold text-light markoOne cardName text-shadow text-constantLight"
        >
          {{ keep?.name }}
        </p>
      </span>
      <span>
        <img
          :src="keep?.creator?.picture"
          :alt="keep?.creator?.name"
          class="pImg cardImg m-2"
        />
      </span>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { onMounted, ref, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: { type: Object, required: true },
  },
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});
    const route = useRoute();
    return {
      editable,
      route,
      activeVault: computed(() => AppState.activeVault),

      async setActive() {
        try {
          await keepsService.setActive(props.keep);
          Modal.getOrCreateInstance("#activeKeep").show;
        } catch (error) {
          Pop.error(error, "[setActive]");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.deleteBtn {
  opacity: 0;
  z-index: 9999;
  transition: all 0.5s ease;
}

.deleteBtn:hover {
  transition: all 1.75s ease;
  transform: scale(2);
}
.card {

  transition: all 0.5s ease;
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
  .cardName {
    text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.25);
  }
}

.cardImg {
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
}
.card:hover {
  transform: scale(1.01);
  filter: brightness(90%);
  transition: all 0.5s ease;
  cursor: pointer;
  box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px,
    rgba(0, 0, 0, 0.22) 0px 15px 12px;
  .deleteBtn {
    z-index: 9999;
    color: rgb(194, 139, 219);
    opacity: 1;
    transition: all 0.25s ease;
    transform: scale();
  }
}
</style>
