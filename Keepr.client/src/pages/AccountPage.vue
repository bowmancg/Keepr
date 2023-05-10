<template>
  <div class="text-center container-fluid">
    <div class="row">
      <div class="col-12 pt-2">
        <img :src="account?.coverImg" :alt="account.name" class="hero-img mx-1 img-fluid elevation-3">
      </div>
    </div>
    <h1>Welcome</h1>
    <img class="rounded img-profile" :src="account.picture" :alt="account.name" />
    <p>{{ account.name }}</p>
    <button data-bs-toggle="modal" data-bs-target="#editAccountForm" class="btn btn-success">Edit Account</button>
  </div>
  <section class="row justify-content-start">
    <div class="col-12">
      <div class="row justify-content-start">
        <div v-for="v in vaults" :key="v.id" class="col-md-3 my-3 mx-4 py-2">
          <VaultCard :vault="v.vault" />
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
import { vaultKeepsService } from '../services/VaultKeepsService';
import { useRoute } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
export default {
    setup() {
      const route = useRoute()
      // async function getVaultById() {
      //   try {
      //     const vaultId = route.params.vaultId
      //     await vaultsService.getVaultById(vaultId)
      //   } catch (error) {
      //     Pop.error(error)
      //   }
      // }
      

            watchEffect(() => {
              // getVaultById()
              route.params.vaultId
            })
        return {
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.vaults),
            
        };
    },
    components: { Modal, EditAccountForm, VaultCard }
}
</script>

<style scoped>
.img-profile {
  max-width: 100px;
  
}

.hero-img {
  object-fit: cover;
  object-position: center;
  height: 39vh;
  border-style: solid;
  border-bottom: black;
  border-width: 2px;
  width: 50%;
}
</style>
