<template>
    <div class="card">
        <div class="mt-3 mb-3 rounded">
            <div>
                <img :src="keep?.img" :alt="keep.name" class="img-fluid p-3 rounded">
                <p class="text-center fs-5">{{ keep.name }}</p>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {
        return {
            keeps: computed(() => AppState.keeps)
        }
    }
};
</script>


<style lang="scss" scoped></style>