<template>
  <div class="card ps-2 rounded pe-5 my-1" :class="test ? 'bg-dark no-select disabled ' : ''">
    <div class="py-2 d-flex align-items-center">
      <img
        :src="vault.img"
        alt=""
        class="rounded position-relative"
        height="80"
        width="80"
      />
      <span v-if="vault?.isPrivate" class="position-absolute txt-shadow">
        <i
          class="mdi mdi-shield-lock-outline fs-1 text-constantLight txt-Shadow"
        ></i>
      </span>
      <h6 class="ms-2">{{ vault.name }}</h6>
      <div class="save">
        <h6 class="rounded-5 bg-danger p-2">Save</h6>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    vault: { type: Vault, required: true },
  },
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});
    return {
      editable,
      theme: computed(() =>
        AppState.account.theme == false ? "#5d595917" : "#e0d1d1cf"
      ),
      test: computed(() =>
        AppState.vKeepIds?.find(
          (v) =>
            v?.vaultId == props.vault?.id &&
            v?.keepId == AppState.activeKeep?.id
        )
      ),
    };
  },
};
</script>

<style lang="scss" scoped>
.card {
  background: transparent;
  border: 0;
  transition: all 0.25s ease;
}
.save {
  opacity: 0;
  transition: all 0.25s ease;
  position: absolute;
  right: -12px;
}
.save:hover {
  filter: brightness(80%);
}
.card:hover {
  background: v-bind(theme);
  transition: all 0.75 ease;
  filter: brightness(90%);
  cursor: pointer;
  box-shadow: rgba(0, 0, 0, 0.09) 0px 2px 1px,
    rgba(224, 209, 209, 0.811) 0px 4px 2px, rgba(0, 0, 0, 0.09) 0px 8px 4px,
    rgba(0, 0, 0, 0.09) 0px 16px 8px, rgba(0, 0, 0, 0.09) 0px 32px 16px;
  .save {
    opacity: 1;

    transition: all 0.5s ease;
  }
}
</style>
