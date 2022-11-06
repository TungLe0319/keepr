<template>
  <div class="about text-center">
    <h1>Welcome {{ profile?.name }}</h1>
    <img class="rounded" :src="profile?.picture" alt="" />
    <p>{{ profile?.email }}</p>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import {profilesService} from "../services/ProfilesService.js"
import Pop from "../utils/Pop.js"

export default {
  
  setup() {
    async function getProfileById(){
      try {
          await profilesService.getProfileById(route.params.id)
        } catch (error) {
          Pop
          .error(error,'[getProfileById]')
        }
    };
    onMounted(()=>{
      getProfileById()
  })
  const route = useRoute();
    return {
      route,
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
