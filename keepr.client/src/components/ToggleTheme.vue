<template>

 <button class="btn dotHover" @click="toggleTheme()">
            <i class="mdi mdi-theme-light-dark fs-1 text-dark"></i>
            <!-- <i class="mdi mdi-theme-light-dark"></i> -->
          </button>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js";

export default {
props:{

       },
  setup(props) {
    const editable = ref({});
    
    onMounted(() => {

    });
    watchEffect(() => {
   document.body.setAttribute('data-theme', AppState.darkTheme ? 'dark' : 'light')
    });

    return {
      editable,
      darkthemeActive: computed(()=> AppState.darkTheme),
       toggleTheme() {
        console.log("hi");
        AppState.darkTheme = !AppState.darkTheme
        // document.body.classList.toggle = "dark";
      },
      }
    }
  }
</script>

<style lang="scss" scoped>
</style>
