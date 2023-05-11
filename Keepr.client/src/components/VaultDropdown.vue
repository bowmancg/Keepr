<template>
    <div class="dropdown">
        <button class="btn btn-primary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">Add to
            Vault</button>
        <ul class=" dropdown-menu scrollable-dropdown me-auto">
            <li v-for="v in vaults" :key="v.id">
                <a @click="createVaultKeep(keep.id, v.id)" class="dropdown-item">{{ v.name }}</a>
            </li>
        </ul>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { vaultsService } from '../services/VaultsService';
import { profilesService } from '../services/ProfilesService';
import Pop from '../utils/Pop';
import { Keep } from '../models/Keep';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup() {
        async function getProfileVaults() {
            try {
                const profileId = AppState.account.id
                if (profileId) {                    
                    await vaultsService.getProfileVaults(profileId)
                }
            } catch (error) {
                Pop.error(error)
            }
        }
        watchEffect(() => {
            getProfileVaults()
        })
        return {
            vaults: computed(() => AppState.myVaults),
            async createVaultKeep(keepId, vaultId) {
                try {
                    await vaultKeepsService.createVaultKeep(keepId, vaultId)
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.scrollable-dropdown {
    height: auto;
    max-height: 300px;
    overflow-x: hidden;
}
</style>