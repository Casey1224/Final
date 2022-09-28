import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService.js"
class KeepsService {


    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('got keeps', res.data)
        AppState.keeps = res.data
    }
    async getOne(id) {
        const res = await api.get(`api/keeps/${id}`)
        logger.log('get one', res.data)
        AppState.activeKeep = res.data
        logger.log('active', AppState.activeKeep)
    }
}

export const keepsService = new KeepsService()