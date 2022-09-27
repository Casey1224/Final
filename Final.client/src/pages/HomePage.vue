<template>
  <div class="container-fluid">
    <div class="row">
      <div class="masonry">
        <div class="" v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home'
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

  .masonry {
    columns: 200px;
    column-gap: 1em;

    div {
      display: block;
      margin-bottom: 1em;
    }

  }
}
</style>
