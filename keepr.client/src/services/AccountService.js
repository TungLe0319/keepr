import { AuthPlugin } from "@bcwdev/auth0provider-client";
import { AppState } from "../AppState";
import { Account } from "../models/Account.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = res.data;
      // console.log(AppState.account);
      console.log(res.data);
    } catch (err) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }
  async editProfile(formData) {
    const res = await api.put("/account", formData);
    AppState.account = new Account(res.data);
  }
  async toggleTheme() {
    // console.log(themeState);
    AppState.account.theme = !AppState.account.theme;

    // console.log(themeState);
    //  console.log(AppState.account.theme);
    const res = await api.put("/account", AppState.account);
    // console.log(res.data);
    // AppState.account = new Account(res.data);
  }

  async getAccountKeeps() {
    // AppState.keeps =[]
    const res = await api.get("/account/keeps");
    // console.log("[keeps]", res.data);
    // let keeps = res.data.map((k) => new Keep(k));
    // AppState.offSet += keeps.length
    AppState.accountKeeps = res.data.map((k) => new Keep(k));
  }
  async getAccountVaults() {
    const res = await api.get("/account/vaults");
    // console.log(res.data);
    // let vaults = res.data.map(v=> new Vault(v))

console.log(res.data);
    AppState.accountVaults = res.data.map((v) => new Vault(v));
    // console.log(AppState.accountVaults);
  }
}

export const accountService = new AccountService();
