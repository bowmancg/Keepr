<template>
  <section class="justify-content-center p-3 masonry-with-flex">
    <div data-bs-toggle="modal" data-bs-target="#keepModal" v-for="k in keeps" :key="k.id">
    <KeepCard :keep="k" />
    </div>
  </section>

  <Modal id="keepModal">
    <template #header>
      <h4>{{ keep.name }}</h4>
    </template>
    <template #modalBody>
      <KeepDetailsModal />
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted } from 'vue';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import KeepDetailsModal from '../components/KeepDetailsModal.vue';
import { router } from '../router';

export default {
    setup(props) {
      async function getKeeps() {
        try {
          await keepsService.getKeeps()
        } catch (error) {
          Pop.error(error)
        }
      }

      onMounted(() => {
        getKeeps()
      })
        return {
          keeps: computed(() => AppState.keeps),
          keep: computed(() => AppState.keep)
        };
    },
    components: { KeepCard, KeepDetailsModal }
}
</script>

<style scoped lang="scss">
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-flex {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  max-height: 1000px;
  div {
    width: 150px;
    background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  } 
  @for $i from 1 through 36 { 
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>
