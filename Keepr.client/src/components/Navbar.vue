<template>

    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <p class="fs-1"><span class="mdi mdi-image-marker"></span>Keepr</p>
        </div>
      </router-link>
      <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
      >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <div class="dropdown">
        <button  class="btn btn-primary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">Create</button>
        <ul class="dropdown-menu me-auto">
          <li>
            <a data-bs-toggle="modal" data-bs-target="#createKeepForm" class="dropdown-item">Add Keep</a>
          </li>
          <li><hr class="dropdown-divider"></li>
          <li>
            <a data-bs-toggle="modal" data-bs-target="#createVaultForm" class="dropdown-item">Add Vault</a>
          </li>
        </ul>
      </div>
      <!-- LOGIN COMPONENT HERE -->
    </div>
    <Login />
  </nav>
  <Modal id="createKeepForm">
    <template #header>
      <h5>Create Keep</h5>
    </template>
    <template #modalBody>
      <CreateKeepForm />
    </template>
  </Modal>
  <Modal id="createVaultForm">
    <template #header>
      <h5>Create Vault</h5>
    </template>
    <template #modalBody>
      <CreateVaultForm />
    </template>
  </Modal>
</template>

<script>
import CreateKeepForm from './CreateKeepForm.vue';
import CreateVaultForm from './CreateVaultForm.vue';
import Login from './Login.vue'
import Modal from './Modal.vue';
export default {
  setup() {
    return {

    }
  },
  components: { Login, Modal, CreateKeepForm, CreateVaultForm }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

</style>
