<template>
    <div class="row">

        <img :src="profile?.picture" class=" ml-3  modal-woah border border-circle" alt="">

        <div class="col-4">
            <h2>{{profile?.name}}</h2>

            <h6 v-if="account.id != profile.id">keeps:{{myKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Keeps:{{myKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Vaults:{{accountVaults.length}}</h6>
            <h6 v-if="account.id != profile.id">vaults:{{myVaults.length}}</h6>
        </div>
    </div>
    <div class="row">
        <div class="col-12 d-flex">
            <h4>Vaults</h4>
            <CreateVaultModal>
                <template #button>
                    <button class="btn btn-secondary" data-bs-target="#create-vault" data-bs-toggle="modal">Create Vault
                        +</button>
                </template>
            </CreateVaultModal>

        </div>
    </div>
    <p>VAULTS</p>
    <div class="row container " v-if="account.id == profile.id">
        <div class="col-md-3 fixed-height selectable rounded" :title="v.name" v-for="v in accountVaults" :key="v.id"
            @click="goToVault(v.id)">
            <h4><b>{{ v.name }}</b></h4>
        </div>
    </div>
    <div class="row container " v-else="account.id != profile.id">
        <div class="col-md-3 fixed-height selectable rounded" :title="v.name" v-for="v in myVaults" :key="v.id"
            @click="goToVault(v.id)">
            <h4><b>{{ v.name }}</b></h4>
        </div>
    </div>
    <!-- <div class="row">
        <div class="masonry " v-if="account.id == profile.id">
            <div class="" v-for="v in myVaults" :key="v.id">
                <VaultCard :vault="v" />
            </div>
        </div>
    </div> -->
    <div>
        <p>KEEPS</p>

        <div class="row">
            <div class="col-12 d-flex">
                <h4>Keeps</h4>
                <CreateKeepModal>
                    <template #button>
                        <button class="btn btn-secondary" data-bs-target="#create-keep" data-bs-toggle="modal">Create
                            Keep
                            +</button>
                    </template>
                </CreateKeepModal>


                <!-- <h4 v-if="profile.id == account.id" class="bg-green selectable" title="create keep"
                    data-bs-toggle="modal" data-bs-target="create-keep"> ＋</h4> -->
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
            }
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