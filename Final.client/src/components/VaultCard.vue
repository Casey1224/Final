<template>


    <div class="bg-light text-light border elevation-2 rounded selectable">
        <div class="col-2 text-dark ">

            <h4><b>{{ vault.name }}</b></h4>

        </div>
    </div>



</template>
<script>
export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {};
    },
};
</script>
<style >
.gfg {

    position: relative;
}

.first-txt {
    position: absolute;
    /* bottom: 10px;
    left: 10px; */
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