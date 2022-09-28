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
                        <div>

                            <router-link v-if="keep" :to="{name: 'Keep Details', params: {id:keep?.id}}">
                                <button type="button" data-bs-dismiss="modal" class="btn btn-primary">See
                                    Details</button>
                            </router-link>
                        </div>

                        <div>
                            <h6>🗑</h6>
                        </div>
                        <div>
                            <img :src="keep.creator.picture" class=" modal-prof border border-circle"
                                alt="">{{keep.creator.name}}
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
import { AppState } from '../AppState';

export default {
    setup() {
        return {

            keep: computed(() => AppState.activeKeep)
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