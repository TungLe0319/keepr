<template>
  <div class="container">
    <!-- <PaginationStyle /> -->
    <SearchBar />
    <div class="bricks my-3">
      <!-- <TransitionGroup
        name=""
        enterActiveClass="animate__fadeIn animate__animated"
        leaveActiveClass="animate__fadeOut animate__animated"
      >
        <div class="mt-3 keepCards" v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </TransitionGroup>
    </div> -->

<div v-masonry="containerId" transition-duration="0.3s" item-selector=".item">
  <div v-masonry-tile class="item" v-for="k in keeps" :key="k.id">
           <KeepCard :keep="k" />
  </div>
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
import { accountService } from "../services/AccountService.js";
import SearchBar from "../components/SearchBar.vue";

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
      infiniteScroll();
    });
    async function getAllKeeps() {
      try {
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
          document.documentElement.scrollTop + (window.innerHeight +100);
        let whatEver = document.documentElement.offsetHeight;
        if (bottomOfWindow >= whatEver ) {

          getKeepsByScroll();
        }
      };
    }

    return {
      keeps: computed(() => AppState.keeps),
      // .sort(() => Math.random() - 0.5)
    };
  },
  components: { KeepCard, PaginationStyle, PopperTip, SearchBar },
};
</script>

<style scoped lang="scss">
.keepCards {
  --animate-duration: 500ms;
  --animate-delay: 1s;
}
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
