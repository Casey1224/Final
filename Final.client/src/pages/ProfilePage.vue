<template>
    <div class="row container m-2">

        <img :src="profile?.picture" class="   modal-woah border border-circle" alt="">

        <div class="col-4">
            <h2>{{profile?.name}}</h2>

            <h6 v-if="account.id != profile.id">keeps:{{myKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Keeps:{{myKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Vaults:{{accountVaults.length}}</h6>
            <h6 v-if="account.id != profile.id">vaults:{{myVaults.length}}</h6>
        </div>
    </div>
    <div class="row">
        <div class="col-12 d-flex m-2">

            <CreateVaultModal>
                <template #button>
                    <button class="btn btn-success" data-bs-target="#create-vault" data-bs-toggle="modal">Create Vault
                        +</button>
                </template>
            </CreateVaultModal>

        </div>
    </div>

    <div class="row container " v-if="account.id == profile.id">
        <div class="col-md-3 fixed-height selectable rounded border border-info mb-4  mr-1" :title="v.name"
            v-for="v in accountVaults" :key="v.id" @click="goToVault(v.id)">
            <h4><b>{{ v.name }}</b></h4>
        </div>
    </div>
    <div class="row container " v-else="account.id != profile.id">
        <div class="col-md-3 fixed-height selectable rounded border border-info mb-4 mr-1" :title="v.name"
            v-for="v in myVaults" :key="v.id" @click="goToVault(v.id)">
            <h4><b>{{ v.name }}</b></h4>
        </div>
    </div>

    <div>


        <div class="row">
            <div class="col-12 m-2 d-flex">

                <CreateKeepModal>
                    <template #button>
                        <button class="btn btn-success" data-bs-target="#create-keep" data-bs-toggle="modal">Create
                            Keep
                            +</button>
                    </template>
                </CreateKeepModal>



            </div>
        </div>


    </div>
    <div class="row">
        <div class="masonry">
            <div class="" v-for="k in myKeeps" :key="k.id">
                <KeepCard :keep="k" />
            </div>
        </div>
    </div>










</template>
<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { profileService } from '../services/ProfileService';
import { vaultsService } from '../services/vaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import CreateKeepModal from '../components/createKeepModal.vue';
import CreateVaultModal from '../components/createVaultModal.vue';
import { accountService } from '../services/AccountService';



export default {
    name: 'Profile',
    setup() {
        const route = useRoute();
        const router = useRouter();
        async function getProfileById() {
            try {
                await profileService.getProfileById(route.params.id);
            }
            catch (error) {
                logger.log(error);
            }
        }
        async function getProfileKeeps() {
            try {
                await profileService.getProfileKeeps(route.params.id);
            }
            catch (error) {
                logger.log(error);
            }
        }
        async function getProfileVaults() {
            try {
                await profileService.getProfileVaults(route.params.id);
            }
            catch (error) {
                logger.log(error);
            }
        }
        async function getAllVaults() {
            try {
                await accountService.getAllVaults(route.params.id);
            }
            catch (error) {
                logger.log(error);
            }
        }
        onMounted(() => {
            getProfileById();
            getProfileKeeps();
            getProfileVaults();
            getAllVaults();
            // goToVault();
        });
        return {
            accountKeeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.activeProfile),
            account: computed(() => AppState.account),
            myKeeps: computed(() => AppState.activeProfileKeeps),
            myVaults: computed(() => AppState.activeProfileVaults),
            accountVaults: computed(() => AppState.vaults),

            async goToVault(id) {
                router.push({ name: "Vault", params: { id } });
            },

            // async gotToVault(id) {
            //     try {
            //         router.push({ name: "Vault", params: { id } })
            //     }
            //     catch (error) {
            //         router.push({ name: 'Home' })
            //         Pop.error(error.message);
            //         logger.log(error);
            //     }
            // }


            // async goToVault(id) {
            //     if (account.id == profile.id) {
            //         router.push({ name: "Vault", params: { id } })
            //     } else {
            //         router.push({ name: "Home", params: { id } })
            //     }
            // }
        };
    },
    components: { CreateVaultModal, CreateKeepModal }
};
</script>
<style scoped lang="scss">
.modal-woah {

    border-radius: 25%;
    width: 100px
}

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