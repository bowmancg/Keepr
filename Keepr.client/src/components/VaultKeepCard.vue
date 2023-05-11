<template>
    <div class="elevation-3 mt-3">
        <img class="img-fluid" :src="vaultKeep.img" :alt="vaultKeep.name">

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { VaultKeep } from '../models/VaultKeep';
export default {
    props: {
        vaultKeep: { type: VaultKeep, required: true }
    },
    setup(props) {

        return {
            
        }
    }
};
</script>


<style lang="scss" scoped></style>