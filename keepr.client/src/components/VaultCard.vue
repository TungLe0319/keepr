<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="card rounded border-0 my-1" v-if="vault">
      <img
        :src="vault?.img"
        alt="vault Img"
        title="keep image"
        class="img-fluid rounded"
      />
      <div
        class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end"
      >
        <span>
          <p class="mb-0 vaultName text-constantLight">{{ vault?.name }}</p>
        </span>
        <span v-if="vault?.isPrivate">
          <i
            class="mdi mdi-lock-check-outline fs-4 text-constantLight vaultName"
          ></i>
        </span>
      </div>
    </div>
  </router-link>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
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
    const route = useRoute();
    return {
      route,
      editable,

      async setActive() {
        try {
          await vaultsService.setActive(props.vault);
        } catch (error) {
          Pop.error(error, "[setActive]");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.card {
  transition: all 0.5s ease;
  filter: drop-shadow(0px 4px 4px rgba(0, 0, 0, 0.25));
  background: linear-gradient(
      180.3deg,
      rgba(0, 0, 0, 0) 49.73%,
      rgba(0, 0, 0, 0.33) 74.73%
    ),
    #b2bec3;
  box-shadow: 0px 4px 6px rgba(180, 180, 180, 0.25);
}
.cardImg {
  box-shadow: 0px 4px 6px rgba(180, 180, 180, 0.25);
}
.card:hover {
  transform: scale(1.005);
  filter: brightness(95%);
  transition: all 0.5s ease;
  cursor: pointer;
  box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px,
    rgba(0, 0, 0, 0.22) 0px 15px 12px;
}

.vaultName {
  font-family: "Quando";
  font-style: normal;
  font-weight: 400;
  font-size: 24px;
  line-height: 30px;
  letter-spacing: 0.06em;
  text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.25);
}
</style>
