<template>
    <div class="elevation-3">
        <div>
            <img :src="keep?.img" :alt="keep.name" class="img-fluid">
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