import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async createKeep(keepData) {
    const res = await api.put("api/keeps", keepData);
    let newKeep = new Keep(res.data);
    AppState.keeps = [...AppState.keeps, newKeep];
  }
  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);
    let index = AppState.keeps.findIndex((k) => k.id == keepId);

    AppState.keeps.splice(index, 1);
  }
}
export const keepsService = new KeepsService();
