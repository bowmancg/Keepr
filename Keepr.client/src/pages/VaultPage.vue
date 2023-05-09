<template>
    <section class="row justify-content-center m-5">
        <div class="col-md-10 p-1 rounded">
            {{ vault?.img }}
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup(){
    return {
        vault: computed(() => AppState.activeVault)
    }
    }
};
</script>


<style lang="scss" scoped>

</style>