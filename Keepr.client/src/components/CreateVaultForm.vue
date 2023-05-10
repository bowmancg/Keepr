<template>
    <form @submit.prevent="createVault()">
        <div class="mb-3">
            <label for="name" class="form-label">Title</label>
            <input type="text" maxlength="30" placeholder="Title" required v-model="editable.name" id="name" class="form-control">
        </div>
        <div class="mb-3">
            <label for="img" class="form-label">Image URL</label>
            <input type="url" id="img" required placeholder="Image URL" v-model="editable.img" class="form-control">
        </div>
        <div class="mb-3">
            <label for="description" class="form-label">Description</label>
            <input type="text" maxlength="1000" id="description" required placeholder="Description" v-model="editable.description" class="form-control">
        </div>
        <div class="mb-3">
            <label for="isPrivate" class="form-label">Make Vault Private?</label>
            <input type="checkbox" class="form-check-input">
        </div>
        <button data-bs-dismiss="modal" type="submit" class="btn btn-success">Submit</button>
    </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { useRoute, useRouter } from 'vue-router';
export default {
    // props: {
    //     vault: {
    //         default: {}
    //     }
    // },
    setup(){
        const editable = ref({})
        
        const route = useRoute()
        const router = useRouter()
    return {
        route,
        editable,
        async createVault() {
            try {
                const vaultData = editable.value
                const vault = await vaultsService.createVault(vaultData)
                AppState.activeVault = null
                await router.push({ name: 'Vault', params: { vaultId: vault.id } })
            } catch (error) {
                Pop.error(error)
            }
        }
    }
    }
};
</script>


<style lang="scss" scoped>

</style>