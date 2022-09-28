<template>


    <div class="bg-light text-light border elevation-2 rounded selectable" @click="setActive()">
        <div class="p-1 text-light">
            <img class="img-fluid gfg " :src="keep.img" alt="">
            <h4 class=" text-shadow: 2px 2px #ff0000 first-txt text-light "><b>{{ keep.name }}</b></h4>
            <img :src="keep.creator.picture" class=" second-txt border border-circle" alt="">
        </div>
    </div>
    <KeepModal />




</template>
<script>
import { Modal } from 'bootstrap';
import { router } from '../router.js'
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import KeepModal from './KeepModal.vue';
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        return {
            async setActive() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.getOne(props.keep.id)
                } catch (error) {
                    logger.error(error)
                }
            }
        };
    },
    components: { KeepModal }
};
</script>
<style >
.gfg {

    position: relative;
}

.first-txt {
    position: absolute;
    bottom: 10px;
    left: 10px;
    text-shadow: 1px 1px #000000
}

.second-txt {
    position: absolute;
    bottom: 10px;
    left: 200px;
    border-radius: 50%;
    width: 30px
}
</style>