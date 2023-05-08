import { Profile } from "./Account";

export class VaultKeep {
    constructor(data) {
        this.id = data.id
        this.vaultKeepId = data.vaultKeepId
        this.vaultId = data.vaultId
        this.keepId = data.keepId
        this.accountId = data.accountId
    }
}