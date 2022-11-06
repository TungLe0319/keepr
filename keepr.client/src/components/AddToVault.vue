<template>
  <form @submit.prevent="addToVault()">
    <div class="btn-group">
      <select
        v-model="editable"
        class="form-select "
        aria-label="Default select example"
      >
        <option class="bg-dark" :value="v" v-for="v in vaults">{{ v.name }}</option>
      </select>
    </div>
    <button type="submit" class="btn">Add To Vault</button>
  </form>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import { vaultKeepService } from "../services/VaultKeepService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {},
  setup(props) {
    const editable = ref({});
    async function getAccountVaults() {
      try {
        await accountService.getAccountVaults();
      } catch (error) {
        Pop.error(error, "[getAccountVaults]");
      }
    }

    onMounted(() => {
      getAccountVaults();
    });
    watchEffect(() => {});

    return {
      editable,
      vaults: computed(() => AppState.accountVaults),
      keep: computed(() => AppState.activeKeep),
      async addToVault() {
        try {
          let data = {
            vaultId: editable.value.id,
            keepId: this.keep.id,
          };
          await vaultKeepService.createVaultKeep(data);
          Pop.success(`${this.keep.name} saved to ${editable.value.name} `);
        } catch (error) {
          Pop.error(error, "[addToVault]");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
