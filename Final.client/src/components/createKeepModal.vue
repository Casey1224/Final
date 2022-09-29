<template>
    <slot name="button">

    </slot>
    <Modal id="create-keep">
        <template #body>
            <form @submit.prevent="createKeep">
                <div class="row d-flex justify-content-center m-5">
                    <div class="col-12 d-flex justify-content-between">
                        <h2>New Keep</h2>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="col-12 p-3">
                        <label for="">Name: </label>
                        <input v-model="editable.name" type="text" name="" id="" class="ms-2" />
                    </div>
                    <div class="col-12 p-3">
                        <label for="">Image Url: </label>
                        <input v-model="editable.img" type="text" name="" id="" class="ms-2" />
                    </div>
                    <div class="col-12 p-3">
                        <label for="">Description: </label>
                        <input v-model="editable.description" type="text" name="" id="" class="ms-2" />
                    </div>

                    <div class="col-12">
                        <button class="btn btn-primary" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        </template>
    </Modal>





</template>
<script>
import { Modal } from 'bootstrap';
import { popScopeId, ref, watchEffect } from 'vue';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

    props: { keepData: { type: Object, required: false, default: {} } },
    setup(props) {
        const editable = ref({})
        watchEffect(() => {
            logger.log('watch')
            editable.value = props.keepData
        })
        return {
            editable,
            // async createKeep() {
            //     try {
            //         await keepsService.createKeep(editable.value)
            //         Modal.getOrCreateInstance(document.getElementById('create-keep')).toggle()
            //     } catch (error) {
            //         logger.log(error)
            //         Pop.toast(error.message)
            //     }
            // }

            async createKeep() {
                try {


                    await keepsService.createKeep(editable.value);
                    Pop.toast("keep Created", "warning");

                }
                catch (error) {
                    logger.log(error)
                    Pop.toast(error.message)
                }
            },
            // async createKeep() {
            //     try {
            //         await keepsService.createKeep(editable.value);
            //         Pop.toast("keep created")
            //     } catch (error) {
            //         error.error(error)
            //     }
            // }

        };
    },
};
</script>
<style>

</style>