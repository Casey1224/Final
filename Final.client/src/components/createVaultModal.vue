<template>
    <slot name="button">

    </slot>

    <Modal id="create-vault">
        <template #body>
            <form @submit.prevent="createVault">
                <div class="row d-flex justify-content-center m-5">
                    <div class="col-12 d-flex justify-content-between">
                        <h2>New Vault</h2>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="col-12 p-3">
                        <label for="">Name: </label>
                        <input v-model="editable.name" type="text" name="" id="" class="ms-2" />
                    </div>
                    <div class="col-12 p-3">
                        <label for="">Description: </label>
                        <input v-model="editable.description" type="text" name="" id="" class="ms-2" />
                    </div>
                    <div class="col-12 p-3">
                        <input type="checkbox" name="" id="" v-model="editable.isPrivate" />

                        Private
                        <div class="row mt-2">
                            <sub>Private vaults can only be seen by you.</sub>
                        </div>
                    </div>


                    <div class="col-12 mt-3">
                        <button class="btn btn-primary" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        </template>
    </Modal>



</template>
<script>
import { Modal } from 'bootstrap';
import { ref, watchEffect } from 'vue';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/vaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

    setup() {
        const editable = ref({})
        watchEffect(() => {
            logger.log('watch')

        })
        // setup() {
        //     const editable = ref({})
        return {
            editable,
            // async createVault() {
            //     try {
            //         await vaultsService.createVault(editable.value)
            //         Modal.getOrCreateInstance(document.getElementById('create-vault')).hide()
            //     } catch (error) {
            //         logger.log(error)
            //         Pop.toast(error.message)
            //     }
            // },
            async createVault() {
                try {
                    await vaultsService.createVault(editable.value);

                    Pop.toast("vault Created", "warning");

                }
                catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            },
        };
    },
};
</script>
<style>

</style>