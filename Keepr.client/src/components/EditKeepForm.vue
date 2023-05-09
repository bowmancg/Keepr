<template>
    <form @submit.prevent="editKeep()">
        <div class="mb-3">
            <label for="name" class="form-label">Keep Name</label>
            <input type="text" maxlength="30" placeholder="Keep Name" required v-model="editable.name" id="name"
                class="form-control">
        </div>
        <div class="mb-3">
            <label for="img" class="form-label">Image URL</label>
            <input type="url" id="img" required placeholder="Image URL" v-model="editable.img" class="form-control">
        </div>
        <div class="mb-3">
            <label for="tags" class="form-label">Tags...</label>
            <input type="text" maxlength="1000" placeholder="Tags..." required v-model="editable.tags" id="tags"
                class="form-control">
        </div>
        <div class="mb-3">
            <label for="description" class="form-label">Description</label>
            <input type="text" placeholder="Keep Description" required v-model="editable.description" id="description"
                maxlength="1000" class="form-control">
        </div>
        <button data-bs-dismiss="modal" type="submit" class="btn btn-success">Submit</button>
    </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { keepsService } from '../services/KeepsService';
export default {
    setup(){
        const editable = ref({})
        watchEffect(() => {
            editable.value = { ...AppState.activeKeep }
        })
    return {
        editable,
        async editKeep() {
                try {
                    const keepData = editable.value
                    await keepsService.editKeep(keepData)
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