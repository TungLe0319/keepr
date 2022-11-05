<template>
  <div class="container">
    <div class="row masonry">
      <div class="col-md-2 mt-4" v-for="k in keeps" :key="k.id">
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
export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error, "[getAllKeeps]");
      }
    }
async function getKeepsByScroll(){
  try {
      await keepsService.getKeepsByScroll()
    } catch (error) {
      Pop.error(error,'[getKeepsByScroll]')
    }
}
      function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight ===
          document.documentElement.offsetHeight;
        if (bottomOfWindow) {
          getKeepsByScroll()
        }
      };
    }

    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard },
};
</script>

<style scoped lang="scss">
//when screen is 768px OR LESS
@media only screen and (min-width: 768px){

  .c-grid{
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(30%,1fr));
    grid-auto-rows:4rem;
    grid-gap:1rem;

  }
  .grid-item{margin: 0;}
}
</style>
