import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService {

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        logger.log('[Found a Vault]', res.data)
        AppState.activeVault = new Vault(res.data)
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log('[Created Vault]', res.data)
        const newVault = new Vault(res.data)
        AppState.vaults.push(newVault)
        return newVault
    }

    async getProfileVaults(profileId) {
        const res = await api.get('/account/vaults')
        AppState.myVaults = res.data.map(v => new Vault(v))
    }
}

export const vaultsService = new VaultsService()