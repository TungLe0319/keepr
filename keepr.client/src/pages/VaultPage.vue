<template>
  <div class="container">
    <div class="row">
      <div class="col-md-12 text-center mt-2">
        <div class="card border-0">

          <img
            :src="vault?.img"
            alt=""
            class="img-fluid coverImg bShadow rounded"
          />
          <div class="card-img-overlay d-flex justify-content-center align-items-end">
              <div class="text-center  ">
       <h2>{{vault?.name}} </h2>
        </div>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column justify-content-center align-items-center"
      >
        <div class="col-md-12 justify-content-end d-flex mb-5 px-5">
          <div class="dropdown">
            <i
              class="mdi mdi-dots-horizontal  ms-3 fs-1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu">
              <li>
                <a
                  class="btn"
                  data-bs-toggle="modal"
                  data-bs-target="#vaultForm"
                  >Edit Vault</a
                >
              </li>
              <li>
                <a
                  class="btn"
 @click="deleteVault()"
                  >Delete Vault</a
                >
              </li>
            </ul>
          </div>
        </div>
      
       
        <div> <span>{{keeps?.length}} Keeps</span> </div>
      </div>
    </div>


  <div class="bricks">
 <div class="my-3" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
  </div>
  </div>


</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { propsToAttrMap } from "@vue/shared";
import { computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import VaultCard from "../components/VaultCard.vue";
import { router } from "../router.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
    setup() {
  
        onMounted(() => {
          getKeepsByVaultId()
        });
        onAuthLoaded(() => {
     
        });
        async function getKeepsByVaultId(){
          try {
              await vaultsService.getKeepsByVaultId(route.params.id)
            } catch (error) {
              Pop.error(error,'[getKeepsByVaultId]')
            }
        }
   async function getVaultById(){
    try {
        await vaultsService.getVaultById(route.params.id)
      } catch (error) {
        Pop.error(error,'[getVaultById]')
      }
   }
        const route = useRoute()
        return {
          route,
            keeps: computed(() => AppState.vaultedKeeps),
            account: computed(() => AppState.account),
            vault:computed(() => AppState.activeVault),

            async deleteVault(){
              try {
                const yes = await Pop.confirm(`Are you sure you want to delete ${AppState.activeVault.name} ?`,'theres no undoing this')
                      if (!yes) {
                        return
                      }
                let vaultId=AppState.activeVault.id
                await vaultsService.deleteVault(vaultId)
                router.push({name: "Home"})
                } catch (error) {
                  Pop.error(error,'[deleteVault]')
                }
            }
        };
    },
    components: { VaultCard }
};
</script>

<style scoped>
.coverImg {
  height: 300px;

  object-fit: cover;
}
.pImg {

  box-shadow: none;
}
.profilePic {
  border-radius: 50%;
  width: 200px;
  height: 200px;
}
.bricks{
  columns: 4;
    grid-template-columns: auto minmax(0,1fr);
}


@media only screen and (max-width: 768px){
.bricks{
  columns:2
}
}

</style>
