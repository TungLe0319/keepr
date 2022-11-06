import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async setActive(keep) {
    AppState.activeKeep = keep;
    let id = AppState.activeKeep.id;
    await api.get(`api/keeps/${id}`);
    let account = AppState.account
    if (AppState.activeKeep.creator.id != account.id) {
      
      AppState.activeKeep.views++;
    }
  }
  // async getById(id) {
  //   const res = await api.get(`api/keeps/${id}`);
  //   AppState.activeKeep = new Keep(res.data);
  // }
  async getAllKeeps() {
    const res = await api.get("api/keeps");
    // console.log("[keeps]", res.data);
    let keeps = res.data.map((k) => new Keep(k));
    AppState.offSet += keeps.length
    AppState.keeps = [...AppState.keeps, ...keeps];
    console.log(AppState.offSet);
  }

  async getKeepsByScroll() {
    let offSet = AppState.offSet;
    const res = await api.get("api/keeps", {
      params: {
        offSet: offSet,
      },
    });
    console.log("[keeps]", res.data);
    let keeps = res.data.map((k) => new Keep(k));
    AppState.offSet += keeps.length;
    AppState.keeps = [...AppState.keeps, ...keeps];
    // console.log(AppState.keeps);
    console.log(AppState.offSet);
  }
  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData);
    let newKeep = new Keep(res.data);
    AppState.keeps = [ newKeep,...AppState.keeps];
  }
  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);

    let index = AppState.keeps.findIndex((k) => k.id == keepId);

    AppState.keeps.splice(index, 1);
  }
  async editKeep(keepData){
    console.log(keepData);
const res = await api.put(`api/keeps/${keepData.id}`,keepData)
console.log(res.data);
let updated = new Keep(res.data)
AppState.activeKeep = updated
  let index = AppState.keeps.findIndex((k) => {
      k.id == keepData.id;
    });

   AppState.keeps.splice(index, 1,updated);
  
  let index2 = AppState.accountKeeps.findIndex((k) => {
      k.id == keepData.id;
    });

   AppState.accountKeeps.splice(index2, 1,updated);
  }


}
export const keepsService = new KeepsService();
