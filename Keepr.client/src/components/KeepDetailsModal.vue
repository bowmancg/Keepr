<template>
    <div class="">
        {{ keep.description }}

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup(props){
    return {
        keep: computed(() => AppState.keep)
    }
    }
};
</script>


<style lang="scss" scoped>

</style>