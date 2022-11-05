import { Keep } from "./Keep.js";

export class VaultedKeep extends Keep {
  constructor(data) {
    super(data);
    this.keepId = data.keepId;
    this.vaultId = data.vaultId;
    this.vaultKeep = data.vaultKeep;
    this.vaultKeepCreator = data.vaultKeepCreator;
  }
}
