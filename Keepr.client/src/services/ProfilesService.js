import { AppState } from "../AppState"
import { Profile } from "../models/Account"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfilesService {

    async getProfileById(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log('[Found Profile]')
        AppState.activeProfile = new Profile(res.data)
    }
}

export const profilesService = new ProfilesService()