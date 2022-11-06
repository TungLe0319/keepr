import { AppState } from "../AppState.js";
import { VaultedKeep } from "../models/VaultedKeep.js";
import { VaultKeepIds } from "../models/VaultKeepIds.js";
import { api } from "./AxiosService.js";

class VaultKeepService {
  async createVaultKeep(vKeepData) {
    console.log(vKeepData);
    const res = await api.post("api/vaultkeeps", vKeepData);
    let newVaultKeep = new VaultKeepIds(res.data);
  //  let found= AppState.keeps.find((v) => v.id == newVaultKeep.keepId);
  //  console.log(found);

    AppState.vKeepIds.push(newVaultKeep);
    console.log("[vaultKeeps]", res.data);
    console.log(AppState.vKeepIds);
  }

  async deleteVaultKeep(id) {
    await api.delete(`api/vaultkeeps${id}`);
    let index = AppState.vKeepIds.findIndex((v) => {
      v.id == id;
    });
    AppState.vKeepIds.splice(index, 1);
  }

  async getVaultKeepIds() {
    const res = await api.get("api/vaultkeeps");

    AppState.vKeepIds = res.data.map((v) => new VaultKeepIds(v));
    // console.log("[getVaultKeepIds]", res.data);
    // console.log(AppState.vaultKeeps);
  }
}
export const vaultKeepService = new VaultKeepService();
