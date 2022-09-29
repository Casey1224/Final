<template>
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header justify-text-between ">
                    <div class="col-10 justify-content-between">

                        <span class="first-yes" v-for="k in keep?.views" :key="k">views : {{keep.views}}</span>
                        kept : {{keep?.kept}}
                    </div>


                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                </div>
                <div class="modal-body ">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid rounded keep-img" :src="keep?.img" alt="">
                        </div>
                        <div class="col-6">
                            <div class="text-center">
                                <div class="pt-2">
                                    <h1 class="border-bottom ">{{keep?.name}}</h1>
                                </div>
                                <div class="pt-2">
                                    {{keep?.description}}

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">

                    <div class="col-12 d-flex justify-content-between">

                        <div class="dropdown" v-if="user.id != undefined">
                            <button class="btn btn-outline dropdown-toggle" type="button" id="modalDropMenu"
                                data-bs-toggle="dropdown" aria-expanded="false">ADD TO VAULT</button>
                            <ul class="dropdown-menu" aria-labelledby="modalDropMenu">
                                <li v-for="mv in myVaults" :key="mv.id" @click="addToVault(mv.id)">
                                    <a class="dropdown-item" href="#">{{mv.name}}</a>
                                </li>
                            </ul>
                        </div>



                        <div class="" v-if="user?.id == keep.creator?.id"
                            @click="removeKeep(keep.vaultKeepId, keep.id)">
                            <h6>🗑</h6>
                        </div>
                        <div>
                            <img :src="keep.creator?.picture" class=" selectable modal-prof border border-circle" alt=""
                                @click="goToProfile(keep.creator?.id)">{{keep.creator?.name}}
                        </div>
                        <div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>






</template>
<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { useRouter, useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/vaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
    setup() {
        const router = useRouter()
        const route = useRoute()
        return {
            myVaults: computed(() => AppState.vaults),
            keep: computed(() => AppState.activeKeep),
            user: computed(() => AppState.account),
            route,
            goToProfile(id) {
                Modal.getOrCreateInstance(document.getElementById('keepModal')).hide()
                router.push({ name: 'Profile', params: { id } })
            },
            async addToVault(id) {
                try {

                    Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
                    router.push({ name: 'Profile', params: { id } })
                    await vaultsService.addToVault(body)
                    AppState.activeKeep.kept++
                    Pop.toast("added keep to your vault")
                } catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }



            },
            async removeKeep(id, keepId) {
                try {
                    if (route.name == "Vault") {
                        if (await Pop.confirm("this cannot be undone, are you sure?")) {
                            await keepsService.removeVaultKeep(AppState.activeKeepVault.vaultKeepId)
                            Modal.getOrCreateInstance(document.getElementById('keepModal')).hide()
                            await vaultsService.getVaultKeeps(AppState.activeVault.id)
                        }
                    }

                    if (route.name != 'Vault') {
                        if (await Pop.confirm("you sure you want to delete that?")) {
                            await keepsService.removeKeep(id, keepId)
                            Modal.getOrCreateInstance(document.getElementById('keepModal')).hide()
                            await keepsService.getAll()
                        }
                    }
                } catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            }
        };
    },
};
</script>
<style >
.keep-img {
    height: 300px;
    width: 300px;
    object-fit: cover;
}

.first-yes {
    position: absolute;
    left: 100px;

}

.modal-prof {

    border-radius: 25%;
    width: 50px
}
</style>