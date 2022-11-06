<template>
  <router-link :to="{name: 'Vault', params:{id: vault.id }}" > 
  
    <div
      class="card rounded border-0 my-1"
      v-if="vault"
    >
      <img :src="vault?.img" alt="" title="keep" class="img-fluid  rounded" />
      <div class="card-img-overlay p-1 border-none d-flex justify-content-between align-items-end">
        <span>
          <p class="mb-0 vaultName  text-light">{{ vault?.name }}</p>
        </span>
        <span v-if="vault?.isPrivate">
          <i class="mdi mdi-shield-lock-outline fs-2 text-light"></i>
  
        </span>
      </div>
    </div>
  </router-link>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
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

    return {
      editable,

    async   setActive() {
     try {
         await    vaultsService.setActive(props.vault);
       } catch (error) {
         Pop.error(error,'[setActive]')
       }
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

.vaultName{
  font-weight: 700;
  letter-spacing: 0.06em;
  font-size: xx-large;
  font-family: monospace;

    text-shadow: 2px 2px 0px rgba(0, 0, 0, 0.459);
  
}
</style>
