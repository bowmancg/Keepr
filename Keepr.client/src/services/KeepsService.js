import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService {

    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('[Get Keeps]', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getKeepById(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        logger.log('[Found Keep]', res.data)
        AppState.activeKeep = new Keep(res.data)
    }
}

export const keepsService = new KeepsService()