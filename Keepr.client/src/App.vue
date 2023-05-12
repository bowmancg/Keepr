<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
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
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeepForm from './components/CreateKeepForm.vue'
import CreateVaultForm from './components/CreateVaultForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeepForm, CreateVaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
