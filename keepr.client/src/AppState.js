import { reactive } from "vue";

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Account.js').Account | null} */
  activeProfile: null,

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  accountVaults: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],
  /** @type {import('./models/Vault.js').Vault | null} */
  activeVault: null,
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep[]} */
  accountKeeps: [],
  /** @type {import('./models/Keep.js').Keep[]} */
  profileKeeps: [],
  /** @type {import('./models/Keep.js').Keep | null} */
  activeKeep: null,

  /** @type {import('./models/VaultKeepIds.js').VaultKeepIds[]} */
  vKeepIds: [],

  /** @type {import('./models/VaultedKeep.js').VaultedKeep[]} */
  vaultedKeeps: [],
  offSet: 12,
  vaultEditForm: false,
  keepEditForm: false,
  paginationOn: false,
  darkTheme :false,
});
