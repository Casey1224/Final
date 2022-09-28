<template>
  <div class="container-fluid">

    <div class="masonry">
      <div class="" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>

  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {

    async function getKeeps() {

      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error)
      }
    }
    onMounted(async () => {
      getKeeps();
    })
    return {

      keeps: computed(() => AppState.keeps)
    }



  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }


}

// .masonry {
//   columns: 200px;
//   column-gap: 2em;
//   object-fit: contain;
//   -webkit-column-break-inside: avoid;

//   div {
//     display: block;
//     margin-bottom: 2em;
//     margin-top: 2em;
//   }

// }
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
