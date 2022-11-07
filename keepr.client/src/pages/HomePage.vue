<template>
  <div class="container">
    <PopperTip />
    <PaginationStyle />
    <div class="bricks my-3">
      <div class="my-3" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from "vue";
import KeepCard from "../components/KeepCard.vue";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import { computed } from "vue";
import { AppState } from "../AppState";
import Pop from "../utils/Pop.js";
import { vaultKeepService } from "../services/VaultKeepService.js";
import PaginationStyle from "../components/PaginationStyle.vue";
import { createPopper } from "@popperjs/core";
import PopperTip from "../components/PopperTip.vue";
import { onAuthLoaded } from "@bcwdev/auth0provider-client";

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
      getVaultKeepIds();
      infiniteScroll();
    });
onAuthLoaded(()=>{
 this.  $emit('getAccountVaults')
 
})
    async function getVaultKeepIds() {
      try {
        await vaultKeepService.getVaultKeepIds();
      } catch (error) {
        Pop.error(error, "[getAllVaultKeepIds]");
      }
    }

    async function getAllKeeps() {
      try {
        console.log(AppState.accountVaults);
        await keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error, "[getAllKeeps]");
      }
    }
    async function getKeepsByScroll() {
      try {
        await keepsService.getKeepsByScroll();
      } catch (error) {
        Pop.error(error, "[getKeepsByScroll]");
      }
    }
    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight ===
          document.documentElement.offsetHeight;
        if (bottomOfWindow) {
          console.log("hi");
          getKeepsByScroll();
        }
      };
    }

    return {
      keeps: computed(() => AppState.keeps.sort(() => Math.random() - 0.5)),
    };
  },
  components: { KeepCard, PaginationStyle, PopperTip },
};
</script>

<style scoped lang="scss">
//when screen is 768px OR LESS
@media only screen and (min-width: 768px) {
  .c-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(30%, 1fr));
    grid-auto-rows: 4rem;
    grid-gap: 1rem;
  }
  .grid-item {
    margin: 0;
  }
}
.bricks {
  columns: 4;
}

@media only screen and (max-width: 768px) {
  .bricks {
    columns: 2;
  }
}
</style>
