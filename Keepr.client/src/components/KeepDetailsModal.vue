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
                            <img :src="keep.img" :alt="keep.name" class="img-fluid rounded">
                        </div>
                        <div class="col-md-6">
                            <p class="p-2">{{ keep.description }}</p>
                            <button data-bs-toggle="modal" data-bs-target="#editKeepForm"
                                v-if="keep?.creatorId == account?.id" class="btn btn-success">Edit Keep</button>
                            <button @click="deleteKeep(keep.id)" v-if="keep?.creatorId == account?.id" class="btn btn-danger">Delete Keep</button>
                            <VaultDropdown v-if="account?.id" :keep="keep"/>
                        </div>
                    </div>
                    <p>Views: {{ keep.views }}</p>
                    <p>Number Kept: {{ keep.kept }}</p>
                </div>
            </div>
        </div>
    </div>
    <Modal id="editKeepForm">
        <template #header>
            <h5>Edit Keep</h5>
        </template>
        <template #modalBody>
            <EditKeepForm />
        </template>
    </Modal>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { useRoute } from 'vue-router';
import Modal from './Modal.vue';
import EditKeepForm from './EditKeepForm.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import VaultDropdown from './VaultDropdown.vue';
export default {
    setup() {
        const editable = ref({});
        const route = useRoute();
        // async function getKeepById() {
        //     try {
        //         let keepId = route.params.keepId;
        //         await keepsService.getKeepById(keepId);
        //     }
        //     catch (error) {
        //         Pop.error(error);
        //     }
        // }
        onMounted(() => {
            // getKeepById()
        });
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            editable,
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
            }
        };
    },
    components: { Modal, EditKeepForm, VaultDropdown }
};
</script>


<style lang="scss" scoped></style>