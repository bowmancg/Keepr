<template>
    <div id="keepModal" class="modal fade" tabindex="-1" aria-labelledby="keepModalTitle" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <!-- SECTION MODAL HEADER -->
                    <h1 class="fs-5" id="keepModalTitle">
                        {{ keep.name }}
                    </h1>
                </div>
                <!-- SECTION MODAL BODY -->
                <div class="modal-body">
                    {{ keep.description }}
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { useRoute } from 'vue-router';
export default {
    setup() {
        const route = useRoute()
        async function getKeepById() {
            try {
                let keepId = route.params.keepId
                await keepsService.getKeepById(keepId)
            } catch (error) {
                Pop.error(error)
            }
        }
        onMounted(() => {
            // getKeepById()
        })
        return {
            keep: computed(() => AppState.activeKeep)
        }
    }
};
</script>


<style lang="scss" scoped></style>