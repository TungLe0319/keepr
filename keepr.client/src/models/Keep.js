import { Account } from "./Account.js";

export class Keep {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.description = data.description;
    this.img = data.img;
    this.views = data.views;
    this.creator = new Account(data.creator);
    this.kept = data.kept;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    // this.vaulted=data.vaulted || false
    this.vaultId = data.vaultId || this.vaultId
    this.vaultKeepId = data.vaultKeepId || delete this.vaultKeepId
    this.vaultKeepCreatorId = data.vaultKeepCreatorId || delete this.vaultKeepCreatorId
  }
}
