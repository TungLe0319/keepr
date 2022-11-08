import { AppState } from "../AppState.js";
import { Account } from "../models/Account.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
class ProfilesService {
  async getAllProfiles() {}

  async getProfileById(id) {
    const res = await api.get(`api/profiles/${id}`);
    // console.log("[profile]", res.data);
    AppState.activeProfile = res.data;
    // console.log(AppState.activeProfile);
  }

  async getVaultsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/vaults`);
    //  console.log('[profileVaults]',res.data);
     AppState.profileVaults = res.data.map((p) => new Vault(p));
    //  console.log(AppState.profileVaults);
  }
  async getKeepsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/keeps`);
    AppState.profileKeeps = res.data.map((p) => new Keep(p));
  }
}
export const profilesService = new ProfilesService();
