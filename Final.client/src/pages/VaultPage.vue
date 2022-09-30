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
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/vaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRoute, useRouter } from 'vue-router';
export default {
    name: 'Vault',
    setup() {
        const route = useRoute();
        const router = useRouter();

        async function getVault() {
            try {
                await vaultsService.getVault(route.params.id)
            } catch (error) {
                router.push({ name: "Home" })
            }
        }
        async function getKeepsByVault() {
            try {
                await vaultKeepsService.getKeepsByVault(route.params.id)
            } catch (error) {
                logger.log(error)
            }
        }


        onMounted(() => {
            getVault();
            getKeepsByVault();

        });
        return {
            vault: computed(() => AppState.activeVault),

            account: computed(() => AppState.account),
            keeps: computed(() => AppState.activeVaultKeeps),
            async deleteVault() {
                try {
                    if (await Pop.confirm('are you sure you want to delete?')) {
                        await vaultsService.deleteVault(AppState.activeVault.id)

                        router.push({ name: 'Home', params: AppState.account.id })

                    }
                } catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            },
            async getVaultKeeps() {
                try {
                    await vaultKeepsService.getKeepsByVault(route.params.id)
                } catch (error) {

                }
            }
        };


    },
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