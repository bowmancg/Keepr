<template>
  <div class="text-center container-fluid">
    <h1>Welcome</h1>
    <img class="rounded" :src="account.picture" :alt="account.name" />
    <p>{{ account.name }}</p>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
