<template>
  <div class="mt-3 p-3 masonry-with-flex justify-content-center">

    <div class="col-md-3" data-bs-toggle="modal" data-bs-target="#keepModal" @click="selectKeep(k)" v-for="k in keeps"
      :key="k.id">
      <KeepCard :keep="k" />
    </div>
  </div>
  <KeepDetailsModal />
</template>

<script>
import { computed, reactive, onMounted } from 'vue';
import Modal from '../components/Modal.vue';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import KeepDetailsModal from '../components/KeepDetailsModal.vue';

export default {

  setup() {
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
      keep: computed(() => AppState.activeKeep),
      selectKeep(keep) {
        AppState.activeKeep = keep
      }
    };
  },
  components: { KeepCard, KeepDetailsModal, Modal }
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
    width: 40vh;
    background: #7ce548;
    color: white;
    margin: 0 1rem 1rem 0;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }

  // @for $i from 1 through 36 {
  //   div:nth-child(#{$i}) {
  //     $h: (random(400) + 100)+px;
  //     height: $h;
  //     line-height: $h;
  //   }
  // }
}
</style>
