<template>
  <div class="text-center container-fluid">
    <div class="row">

    </div>
    <h1>Welcome</h1>
    <img class="rounded" :src="account.picture" :alt="account.name" />
    <p>{{ account.name }}</p>
  </div>
  <Modal id="editAccountModal">

    <template #header>
      <h5 class="text-dark">Edit Profile</h5>
    </template>

    <template #modalBody>
      <EditAccountForm />
    </template>

  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import Modal from '../components/Modal.vue';
import EditAccountForm from '../components/EditAccountForm.vue';
export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.vaults)
        };
    },
    components: { Modal, EditAccountForm }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
