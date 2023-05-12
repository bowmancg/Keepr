import { Keep } from "./Keep"


export class VaultKeep {
    constructor(data) {
        if (data.keep) {
            this.keep = data.keep
        }
        this.name = data.name
        this.description = data.description
        this.id = data.id
        this.img = data.img
        this.vaultKeepId = data.vaultKeepId
        this.vaultId = data.vaultId
        this.keepId = data.keepId
        this.creatorId = data.creatorId
    }
}