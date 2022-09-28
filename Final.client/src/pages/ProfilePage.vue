<template>
    <div class="row">

        <img :src="profile?.picture" class=" ml-3  modal-woah border border-circle" alt="">

        <div class="col-4">
            <h2>{{profile?.name}}</h2>

            <h6 v-if="account.id != profile.id">keeps:{{accountKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Keeps:{{myKeeps.length}}</h6>
            <h6 v-if="account.id == profile.id">Vaults:{{myVaults.length}}</h6>
            <h6 v-if="account.id != profile.id">vaults:{{accountVaults.length}}</h6>
        </div>
    </div>

    <div class="row">
        <div class="masonry">
            <div class="" v-for="v in myVaults" :key="v.id">
                <VaultCard :vault="v" />
            </div>
        </div>
    </div>
    <div>
        <p>VAULTS</p>
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
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

    setup() {
        const route = useRoute()
        const router = useRouter()

        async function getProfileById() {
            try {
                await profileService.getProfileById(route.params.id);
            } catch (error) {
                logger.log(error)
            }
        }
        async function getProfileKeeps() {
            try {
                await profileService.getProfileKeeps(route.params.id)
            } catch (error) {
                logger.log(error)
            }
        }
        async function getProfileVaults() {
            try {
                await profileService.getProfileVaults(route.params.id)
            } catch (error) {
                logger.log(error)
            }
        }



        onMounted(() => {
            getProfileById();
            getProfileKeeps();
            getProfileVaults();
        })
            ;

        return {
            accountKeeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.activeProfile),
            account: computed(() => AppState.account),
            myKeeps: computed(() => AppState.activeProfileKeeps),
            myVaults: computed(() => AppState.activeProfileVaults),
            accountVaults: computed(() => AppState.vaults),





        };
    },
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