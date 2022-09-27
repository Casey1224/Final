<template>

    <router-link :to="{ name: 'KeepDetails', params: { keepId: keep.id } }">
        <div class="bg-light border elevation-2 rounded selectable">
            <div class="p-1">
                <img class="img-fluid" :src="keep.img" alt="">
                <h6 class="text-dark"><b>{{ keep.name }}</b></h6>
                <img :src="keep.creator.img" class="border border-circle" alt="">
            </div>
        </div>
    </router-link>




</template>
<script>
import { router } from '../router.js'
export default {
    props: { keep: { type: Object, required: true } },
    setup(props) {
        return {
            goTo() {
                router.push({ name: 'KeepsDetailsPage', params: { keepId: props.keep.id } })
            }



        };
    },
};
</script>
<style >

</style>