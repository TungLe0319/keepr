import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { VaultedKeep } from "../models/VaultedKeep.js";
import { router } from "../router.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async setActive(vault){
  // AppState.activeVault   = vault

//  await api.get(`api/vaults/${vault.id}`)
 

  }
  async getAllVaults() {
    const res = await api.get("api/vaults");
    console.log("[vaults]", res.data);
    let vaults = res.data.map((v) => new Vault(v));
    AppState.vaults = [...AppState.vaults, vaults];
    console.log(AppState.vaults);
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`);
    AppState.activeVault = new Vault(res.data);
  }

  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    console.log('[vaults]',res.data);
    let newVault = new Vault(res.data);
    AppState.activeVault = newVault
    // router.push({name:'Vault', params:{id: newVault.id}})
         AppState.accountVaults = [...AppState.accountVaults, newVault];
  }
  async editVault(vaultData) {
    let id = vaultData.id
    const res = await api.put(`api/vaults/${id}`, vaultData);
         console.log('[vaults]',res.data);
      
   let newVault = new Vault(res.data);
   AppState.activeVault = newVault

    let index = AppState.accountVaults.findIndex((a) => {
        a.id == vaultData.id;
      });
  
      AppState.accountVaults.splice(index, 1,newVault);
  }

  async deleteVault(vaultId) {

    await api.delete(`api/vaults/${vaultId}`);

    let index = AppState.vaults.findIndex((v) => {
      v.id == vaultId;
    });
    AppState.vaults.splice(index, 1);
  }

  async getKeepsByVaultId(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultedKeeps = res.data.map(v=> new VaultedKeep(v))
         console.log('[vaultKeeps]',res.data);
         console.log(AppState.vaultedKeeps);
  }
}
export const vaultsService = new VaultsService();
