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

                        <div class="dropdown" v-if="user.id != null">
                            <button class="btn btn-outline dropdown-toggle" type="button" id="modalDropMenu"
                                data-bs-toggle="dropdown" aria-expanded="false">ADD TO VAULT</button>
                            <ul class="dropdown-menu" aria-labelledby="modalDropMenu">
                                <li v-for="av in accountVaults" :key="av.id">
                                    <a @click="addToVault(av.id, keep.id)" class="dropdown-item"
                                        href="#">{{av.name}}</a>
                                </li>
                            </ul>
                        </div>



                        <!-- <div class="" v-if=" user?.id == keep.creator?.id"
                            @click="removeKeep(keep.vaultKeepId, keep.id)">
                            <h6>remove Keep: 🗑</h6>
                        </div> -->
                        <!-- <div class="" v-if=" user?.id == keep.creator?.id
                        " @click="removeVaultKeep(keep?.id)">
                            <h6>Remove Vault Keep: 🐸</h6>
                        </div> -->
                        <div class="" v-if=" user?.id == keep.creator?.id"
                            @click="removeKeep(keep.vaultKeepId, keep.id)">
                            <h6>remove Keep: 🗑</h6>
                        </div>
                        <div class="" v-if=" user?.id == keep.creator?.id
                        " @click="removeKeep(keep.vaultKeepId, keep.id)">
                            <h6>Remove Vault Keep: 🐸</h6>
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
import { KeepAlive } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { accountService } from '../services/AccountService';
import { keepsService } from '../services/KeepsService';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { vaultsService } from '../services/vaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
    setup() {
        const router = useRouter()
        const route = useRoute()
        return {

            accountVaults: computed(() => AppState.vaults.filter(v => v.creator.id == AppState.account.id)),
            keep: computed(() => AppState.activeKeep),
            vaultKeep: computed(() => AppState.activeVaultKeeps),
            user: computed(() => AppState.account),
            route,
            goToProfile(id) {
                Modal.getOrCreateInstance(document.getElementById('keepModal')).toggle()
                router.push({ name: 'Profile', params: { id } })
            },

            async addToVault(vaultId, keepId) {
                try {
                    await vaultKeepsService.addToVaultKeep(vaultId, keepId)
                    Pop.toast("you added to a vault")
                } catch (error) {
                    logger.log(error)
                }
            },
            async removeKeep(id, keepId) {
                try {
                    if (route.name == "Vault") {
                        if (await Pop.confirm("You sure you want to delete this from this vault?")) {
                            const vaultKeepId = AppState.activeVaultKeeps.find(x => x.id === keepId).vaultKeepId
                            await keepsService.removeVaultKeep(vaultKeepId)
                            Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
                            await vaultsService.getVaultKeeps(AppState.activeVault.id)
                        }
                    }
                    if (route.name != "Vault") {
                        if (await Pop.confirm("You sure you want to delete this?")) {
                            await keepsService.removeKeep(id, keepId)
                            Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
                            await keepsService.getKeeps()
                        }
                    }
                } catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            },
            async getAllVaults() {
                try {
                    await accountService.getAllVaults(route.params.id);
                }
                catch (error) {
                    logger.log(error);
                }
            },
            // async removeKeep(id) {
            //     try {

            //         if (await Pop.confirm('are you sure you want to delete?')) {
            //             await keepsService.removeKeep(id)


            //         }
            //     } catch (error) {
            //         logger.log(error)
            //         Pop.toast(error.message)
            //     }
            // },
            // async removeVaultKeep(id) {
            //     try {
            //         console.log("Vault keep before remove")
            //         await vaultKeepsService.removeKeep(id)
            //     } catch (error) {
            //         logger.log(error)
            //         Pop.toast(error.message)
            //     }
            // }






        }
    },
    // async removeKeep(id, keepId){
    //     try {
    //         await Pop.confirm("are you sure you want to delete this? "){
    //       await keepsService.removeKeep(id, keepId)
    //       Modal.getOrCreateInstance(document.getElementById("active-keep")).toggle()
    //     } catch (error) {
    //         logger.log(error)
    //         Pop.toast(error.message)
    //     }
    // }},








}




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

