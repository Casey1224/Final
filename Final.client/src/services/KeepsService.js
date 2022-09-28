import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService.js"
class KeepsService {

    async createKeep(keep) {
        const res = await api.post('/api/keeps', keep)
        logger.log('Created keep', res.data)
        AppState.keeps.push(res.data)
    }
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
    async removeKeep(id) {
        const res = await api.delete('api/keeps/' + id)
        logger.log('Delete Cultist', res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)

    }
    async removeVaultKeep(id) {
        const res = await api.delete('api/vaultkeeps/' + id)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }

}

export const keepsService = new KeepsService()