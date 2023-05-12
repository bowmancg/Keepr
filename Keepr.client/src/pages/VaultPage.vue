<template>
    <section class="row justify-content-center m-5">
        <div class="col-md-10 text-center p-1 rounded">
            <img class="img-fluid vault-img elevation-3 rounded" :src="vault?.img" :alt="vault?.name">
        </div>
    </section>
    <section class="row justify-content-start">
        <div class="col-md-8 text-center">
            <p class="fs-4">{{ vault?.description }}</p>
            
        </div>
        <p class="fs-3 text-center">{{ vaultKeeps.length }} Keeps</p>
    </section>
    <section class="row justify-content-center">
        <div class="masonry" v-if="vaultKeeps && vaultKeeps.length > 0">
            <div data-bs-toggle="modal" data-bs-target="#keepModal" @click="selectVaultKeep(v)" v-for="v in vaultKeeps"
            :key="v.id" class="">
            <VaultKeepCard :vaultKeep="v" />
        </div>
    </div>
    </section>
    <KeepDetailsModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import VaultKeepCard from '../components/VaultKeepCard.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import KeepDetailsModal from '../components/KeepDetailsModal.vue';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter()
        async function getVaultById() {
            try {
                let vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                // Pop.error(error);
            }
        }
        async function getKeepsForVault() {
            try {
                const vaultId = route.params.vaultId
                await vaultKeepsService.getKeepsForVault(vaultId)
            } catch (error) {
                router.push({ name: 'Home'})
                // Pop.error(error)
            }
        }
        watchEffect(() => {
            getVaultById();
            getKeepsForVault()
        });
        return {
            vault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeeps),
            keep: computed(() => AppState.activeKeep),
            
            async selectVaultKeep(vaultKeep) {
                await vaultKeepsService.selectVaultKeep(vaultKeep.vaultKeepId)
                // AppState.activeVaultKeep = vaultKeep
                // AppState.activeKeep = AppState.keeps.find(k => k.id == vaultKeep.keepId)
                // keep.views++
            }
        };
    },
    components: { VaultKeepCard, KeepDetailsModal }
};
</script>


<style lang="scss" scoped>
.vault-img {
    height: 40vh;
    width: 50%;
}

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