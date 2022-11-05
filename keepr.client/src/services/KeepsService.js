import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";

class KeepsService {
   setActive(keep){
    AppState.activeKeep = keep
 
  }
  async getById(id){
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = new Keep(res.data)
  }
  async getAllKeeps() {
    const res = await api.get("api/keeps");
         console.log('[keeps]',res.data);
         let keeps = res.data.map((k) => new Keep(k));
        AppState.keeps = 
        [...AppState.keeps,...keeps]
         console.log(AppState.keeps);
  }
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
