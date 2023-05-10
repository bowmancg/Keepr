<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <div class="elevation-3 mt-4 mb-4">
            <img :src="vault?.img" :alt="vault.name" class="img-fluid">
            {{ vault?.name }}
        </div>
    </router-link>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props){
    return {
        vaults: computed(() => AppState.vaults)
    }
    }
};
</script>


<style lang="scss" scoped>

</style>