import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  profile: {},
  /** @type {import('./models/Account.js').Profile[]} */
  profiles: [],
  /**@type {import('./models/Keep.js').Keep[]} */
  keeps: [],
    /**@type {import('./models/Keep.js').Keep} */
  keep: {},
  /**@type {import('./models/Keep.js').Keep} */
  activeKeep: {},
  /**@type {import('./models/Vault.js').Vault|null} */
  activeVault: null,
  /**@type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  myVaults: [],
  /**@type {import('./models/Vault.js').Vault} */
  vault: {},
  /**@type {import('./models/Account.js').Profile|null} */
  activeProfile: null,
  /**@type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeeps: [],
  /**@type {import('./models/VaultKeep.js').VaultKeep} */
  vaultKeep: {},
  /**@type {import('./models/VaultKeep.js').VaultKeep|null} */
  activeVaultKeep: null,

})
