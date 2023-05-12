<template>
    <div id="keepModal" class="modal fade" tabindex="-1" aria-labelledby="keepModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header align-self-center">
                    <!-- SECTION MODAL HEADER -->
                    <h1 class="fs-3" id="keepModalTitle">
                        {{ keep.name }}
                    </h1>
                </div>
                <!-- SECTION MODAL BODY -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <img :src="keep?.img" :alt="keep.name" class="img-fluid rounded">
                        </div>
                        <div class="col-md-6">
                            <p class="p-2">{{ keep.description }}</p>
                            <button data-bs-toggle="modal" data-bs-target="#editKeepForm"
                                v-if="keep?.creatorId == account?.id" class="btn btn-success">Edit Keep</button>
                            <button @click="deleteKeep(keep.id)" v-if="page != 'Vault' && keep?.creatorId == account?.id"
                                class="btn btn-danger mb-3 mt-3">Delete Keep</button>
                            <VaultDropdown v-if="account?.id && keep" :keep="keep" />
                        </div>
                    </div>
                    <button v-if="page == 'Vault' && vaultKeep?.creatorId == account?.id" @click="deleteVaultKeep(vaultKeep.id)" 
                        class="btn btn-danger mb-3 mt-3">Remove Keep</button>
                    <p><span class="mdi mdi-eye"></span> {{ keep?.views }}</p>
                    <p><span class="mdi mdi-archive"></span> {{ keep?.kept }}</p>
                </div>
                    {{ keep.creator?.name }}
                    <div >
                        <img @click="goToProfile(keep.creator?.id)" :src="keep.creator?.picture" :alt="keep.creator?.name" class="img-fluid m-3 profile-picture">
                    </div>
                <div>
                </div>
            </div>
        </div>
    </div>
    <ModalComponent id="editKeepForm">
        <template #header>
            <h5>Edit Keep</h5>
        </template>
        <template #modalBody>
            <EditKeepForm />
        </template>
    </ModalComponent>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { useRoute, useRouter } from 'vue-router';
import ModalComponent from './Modal.vue';
import EditKeepForm from './EditKeepForm.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import VaultDropdown from './VaultDropdown.vue';
import { Profile } from '../models/Account';
import { logger } from '../utils/Logger';
import { Modal } from 'bootstrap';
export default {
    
    setup() {
        const editable = ref({});
        const route = useRoute();
        const router = useRouter()
        const page = route.name

        onMounted(() => {
            console.log(route.name, 'ROUTE')
        });
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profiles),
            vaultKeep: computed(() => AppState.activeVaultKeep),
            editable,
            page,
            async deleteKeep(keepId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this?')) {
                        await keepsService.deleteKeep(keepId)
                    }
                } catch (error) {
                    Pop.error(error)
                }
            },
            async createVaultKeep(keepId) {
                try {

                    await vaultKeepsService.createVaultKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            },
            async goToProfile(profileId) {
                logger.log(profileId)
                //todo close modal somehow
                Modal.getOrCreateInstance('#editKeepForm').hide()
                router.push({ name: 'Profile', params: { profileId: profileId } })
            },
            async deleteVaultKeep(vaultKeepId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this?')) {
                        await vaultKeepsService.deleteVaultKeep(vaultKeepId)
                    }
                } catch (error) {
                    Pop.error(error)
                }
            }
        };
    },
    components: { ModalComponent, EditKeepForm, VaultDropdown }
};
</script>


<style lang="scss" scoped>
.profile-picture {
    height: 10vh;
    width: 10vh;
    border-radius: 50%;
    box-shadow: 0 0 10px black;
}
</style>