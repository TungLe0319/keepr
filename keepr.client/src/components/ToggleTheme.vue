<template>
  <button class="btn dotHover" @click="toggleTheme()" v-if="user.isAuthenticated" >
    <i class="mdi mdi-theme-light-dark fs-1 text-dark"></i>
    <!-- <i class="mdi mdi-theme-light-dark"></i> -->
  </button>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {},
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {
      document.body.setAttribute(
        "data-theme",
        AppState.account.theme? "dark" : "light"
      );
    });

    return {
      editable,
      account : computed(()=> AppState.account),
      user : computed(()=> AppState.user),
      async toggleTheme() {
        // console.log("hi");
        // AppState.darkTheme = !AppState.darkTheme;
        try {
          {
       
         if (AppState.account) {
          
           await accountService.toggleTheme();
         }
          }
        } catch (error) {
          Pop.error(error, "[toggleTheme]");
        }
        // document.body.classList.toggle = "dark";
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
