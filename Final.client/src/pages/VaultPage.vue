<template>
    <div class="row">
        <div class="col-12 d-flex justify content-between">
            <h2>{{vault.name}}</h2>
            <button class="btn btn-outline-info" v-if=" account.id == vault.creatorId" @click="deleteVault"> Delete
                Vault</button>

        </div>
        <div class="col-12 mt-2">
            Keeps: <span>{{keeps.length }}</span>
        </div>
    </div>
    <div class="masonry">
        <div v-for="k in keeps" :key="k.id" class="">
            <KeepCard :keep="k" />
        </div>
    </div>
</template>
<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState';
import KeepCard from '../components/KeepCard.vue';
import { router } from '../router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/vaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRoute } from 'vue-router';
export default {
    setup() {
        const route = useRoute()
        onMounted(async () => {
            try {
                await vaultsService.getVault(route.params.id)
                await vaultsService.getVaultKeeps(route.params.id)

            } catch (error) {
                router.push({ name: 'Home' })
                logger.log(error)
                Pop.toast(error.message)

            }
        })
        return {
            vault: computed(() => AppState.activeVault),

            account: computed(() => AppState.account),
            keeps: computed(() => AppState.vaultKeeps),
            async deleteVault(id) {
                try {
                    if (await Pop.confirm('are you sure you want to delete?')) {
                        await vaultsService.deleteVault(AppState.activeVault.id)

                        router.push({ name: 'Profile', params: AppState.account.id })

                    }
                } catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            }
        };
    },
    components: { KeepCard }
};
</script>
<style scoped lang="scss">
.masonry {
    columns: 250px;
    column-gap: 1em;
    grid-template-columns: repeat(auto-fit, minmax(min(100%/3, max(64px, 100%/5)), 1fr));

    div {
        display: block;
        margin-bottom: 1em;
    }
}
</style>