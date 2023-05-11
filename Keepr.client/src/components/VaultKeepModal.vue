<template>
    <div class="modal fade" id="vaultKeepModal" tabindex="-1" aria-labelledby="vaultKeepModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header align-self-center">
                    <!-- SECTION MODAL HEADER -->
                    <h1 class="fs-3" id="keepModalTitle">
                        {{ keep?.name }}
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
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { vaultKeepsService } from '../services/VaultKeepsService';
export default {
    setup(){
    return {
        keep: computed(() => AppState.activeVaultKeep),
        
    }
    }
};
</script>


<style lang="scss" scoped>

</style>