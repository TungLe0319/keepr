import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";

class VaultsService {
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
    newVault = new Vault(res.data);
    AppState.vaults.push(newVault);
  }

  async deleteVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`);
    let index = AppState.vaults.findIndex((v) => {
      v.id == vaultId;
    });
    AppState.vaults.splice(index, 1);
  }


  async getVaultedKeeps(){
    
  }
}
export const vaultsService = new VaultsService();
