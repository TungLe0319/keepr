import { api } from "./AxiosService.js";

export class ProfilesService {
  async getAllProfiles() {}

  async getVaultsForProfiles(id) {
    const res = await api.get(`api/profiles/${id}/vaults`);
  }
}
