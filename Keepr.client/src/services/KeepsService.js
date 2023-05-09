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

    async createKeep(keepData) {
        const res = await api.post('api/keeps' , keepData)
        logger.log('[Created Keep]', res.data)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep
    }

    async editKeep(keepData) {
        const res = await api.put(`api/keeps/${keepData.id}`, keepData)
        logger.log('[Edited Keep]', res.data)
        AppState.activeKeep = new Keep(res.data)
    }

    async deleteKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log(res.data)
    }
}

export const keepsService = new KeepsService()