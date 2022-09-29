import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async getKeepsByVault(id) {
        const res = await api.get("api/vaults/" + id + "/keeps")

        logger.log(res.data)
        AppState.activeVaultKeeps = res.data
    }
    async addToVaultKeep(body) {
        const res = await api.post("api/vaultkeeps", body)
        logger.log(res.data)
    }
    async removeKeep(id) {
        const res = await api.delete("api/vaultkeeps/" + id)
        logger.log(res.data)
    }
}
export const vaultKeepsService = new VaultKeepsService();