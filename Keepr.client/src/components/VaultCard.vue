<template>
    <div class="container justify-content-center d-flex row">
            <button v-if="account.id == vault.creatorId" @click="deleteVault(vault.id)" class="btn btn-danger rounded-pill">Delete Vault</button>
            <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
                <div class="text-center rounded p-3 mt-4 mb-4">
                    <span v-if="vault.isPrivate != false" class="mdi mdi-lock"></span>
                    <img :src="vault?.img" :alt="vault.name" class="img-fluid rounded">
                    <p>{{ vault?.name }}</p>
                </div>
            </router-link>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props) {
        return {
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),
            async deleteVault(vaultId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this?')) {
                        await vaultsService.deleteVault(vaultId)
                    }
                } catch (error) {
                    Pop.error(error)
                }
            },
        }
    }
};
</script>


<style lang="scss" scoped></style>