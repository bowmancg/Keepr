<template>
    <div class="elevation-3 mt-3">
        <img :src="vaultKeep.img" :alt="vaultKeep.name">

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { VaultKeep } from '../models/VaultKeep';
export default {
    props: { type: VaultKeep, required: true },
    setup(props){
        
    return {
        vaultKeeps: computed(() => AppState.vaultKeeps)
    }
    }
};
</script>


<style lang="scss" scoped>

</style>