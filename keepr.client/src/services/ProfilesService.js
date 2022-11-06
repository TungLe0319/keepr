import { AppState } from "../AppState.js";
import { Account } from "../models/Account.js";
import { api } from "./AxiosService.js";
class ProfilesService {
  async getAllProfiles() {}

  async getProfileById(id) {
    const res = await api.get(`api/profiles/${id}`);
    console.log("[profile]", res.data);
    AppState.activeProfile = (res.data)
    console.log(AppState.activeProfile);
  }

  async getVaultsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/vaults`);
  }
  async getKeepsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/keeps`);

  }
}
export const profilesService = new ProfilesService();
