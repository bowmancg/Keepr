import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService {

    async createVaultKeep(keepId, vaultId) {
        const res = await api.post('api/vaultkeeps', {keepId, vaultId})
        logger.log('[Created VaultKeep]', res.data)
        const keeps = AppState.keeps

        keeps.forEach(k => {
            if (k.id === keepId) {
                k.kept++
            }
        })
        AppState.keeps = keeps
        const newVaultKeep = new VaultKeep(res.data)
        AppState.vaultKeeps.push(newVaultKeep)
        return newVaultKeep
    }

    async getKeepsForVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('[Found VaultKeep]', res.data)
        AppState.vaultKeeps = res.data.map(v => new VaultKeep(v))
    }

    async deleteVaultKeep(vaultKeepId) {
        const res = api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log('[Delete VaultKeep]', (await res).data)
        const index = AppState.vaultKeeps.findIndex(k => k.vaultKeepId == vaultKeepId)
        AppState.keeps.splice(index, 1)
        AppState.activeVaultKeep = null
    }

    async selectVaultKeep(vaultKeepId) {
        const res = await api.get(`api/vaultkeeps/${vaultKeepId}`)
        const vaultKeep = new VaultKeep(res.data)
        logger.log('[Select Vaultkeep]', vaultKeep)
        AppState.activeKeep = vaultKeep.keep
        AppState.activeVaultKeep = vaultKeep
    }
}

export const vaultKeepsService = new VaultKeepsService()