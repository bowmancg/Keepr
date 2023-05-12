<template>
  <div class="text-center container-fluid">
    <div class="row">
      <div class="col-12 pt-2">
        <img :src="account?.coverImg" :alt="account.name" class="hero-img mx-1 mt-2 img-fluid elevation-3">
      </div>
    </div>
    <h1>Welcome</h1>
    <img class="profile-picture" :src="account.picture" :alt="account.name" />
    <p>{{ account.name }}</p>
    <button data-bs-toggle="modal" data-bs-target="#editAccountForm" class="btn btn-success">Edit Account</button>
  </div>
  <section class="row justify-content-start">
    <div class="col-12">
      <div class="row masonry-with-flex justify-content-center">
        <div v-for="v in vaults" :key="v.id" class="col-md-3 my-3 mx-4 py-2">
          <VaultCard :vault="v" />
        </div>
      </div>
    </div>
  </section>
  <Modal id="editAccountForm">

    <template #header>
      <h5>Edit Profile</h5>
    </template>

    <template #modalBody>
      <EditAccountForm />
    </template>

  </Modal>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import Modal from '../components/Modal.vue';
import EditAccountForm from '../components/EditAccountForm.vue';
import VaultCard from '../components/VaultCard.vue';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
export default {
  setup() {
    const route = useRoute()
    async function getProfileVaults() {
      try {
        await vaultsService.getProfileVaults()
      } catch (error) {
        Pop.error(error)
      }
    }


    watchEffect(() => {
      getProfileVaults()
    })
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { Modal, EditAccountForm, VaultCard }
}
</script>

<style lang="scss" scoped>

.hero-img {
  object-fit: cover;
  object-position: center;
  height: 39vh;
  border-style: solid;
  border-bottom: black;
  border-width: 2px;
  width: 50%;
}

body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-flex {
  columns: 300px;

  div {
    width: 30vh;
  }
}

.profile-picture {
    height: 10vh;
    width: 10vh;
    border-radius: 50%;
    box-shadow: 0 0 10px black;
}
</style>
