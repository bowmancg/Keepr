<template>
        <div class="elevation-3 mt-3 mb-3 text-center">
            <img class="img-fluid p-3 rounded" :src="vaultKeep.img" :alt="vaultKeep.name">
            <p>{{ vault?.name }}</p>
        </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { VaultKeep } from '../models/VaultKeep';
export default {
    props: {
        vaultKeep: { type: VaultKeep, required: true }
    },
    setup(props) {

        return {
            vault: computed(() => AppState.vaults)
        }
    }
};
</script>


<style lang="scss" scoped></style>