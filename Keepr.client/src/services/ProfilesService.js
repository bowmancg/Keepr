import { AppState } from "../AppState"
import { Profile } from "../models/Account"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfilesService {

    async getProfileById(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log('[Found Profile]')
        AppState.activeProfile = new Profile(res.data)
    }

    async getProfileKeeps(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('[Found Keeps]', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getVaultsForProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('[Found Vaults]', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }
}

export const profilesService = new ProfilesService()