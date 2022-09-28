import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService {
    async getVaults() {
        const res = await api.get('account/vaults')
        AppState.vaults = res.data
    }

    async addKeep(body) {
        const res = await api.post('api/vaultkeeps', body)
    }
    async getVault(id) {
        const res = await api.get('api/vaults/' + id)
        AppState.activeVault = res.data
    }
    async getVaultKeeps(id) {
        const res = await api.get('api/vaults/' + id + '/keeps')
        AppState.keeps = res.data
    }
    async deleteVault(id) {
        const res = await api.delete('api/vaults/' + id)
    }
    async createVault(body) {
        const res = await api.post('api/vaults/', body)
        AppState.vaults = res.data
    }
}





export const vaultsService = new VaultsService()