<template>
  <div class="mt-3 p-3 masonry">

    <div class="card mb-3" data-bs-toggle="modal" data-bs-target="#keepModal" @click="selectKeep(k)" v-for="k in keeps"
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
import { useRoute } from 'vue-router';

export default {

  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        Pop.error(error)
      }
    }
    
    const route = useRoute()
    onMounted(() => {
      getKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps),
      async selectKeep(keep) {
        //TODO - GET KEEP BY ID
        const keepId = keep.id
        await keepsService.getKeepById(keepId)
        AppState.activeKeep = keep
        keep.views++
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

.masonry {
  columns: 300px;
  

  div {
    width: 25vh;
  }
}
</style>
