<template>
    <div class="text-center container-fluid">
        <div class="row justify-content-center">
            <div class="col-12 mb-3 pt-2">
                <img :src="profile?.coverImg" :alt="profile?.name" class="hero-img mx-1 img-fluid elevation-3">
            </div>
            <h1>Hello I am {{ profile?.name }}</h1>
            <img :src="profile?.picture" :alt="profile?.name" class="rounded img-profile">
        </div>
        <section class="row mt-2 justify-content-start">
            <h1 class="mb-3">Vaults</h1>
            <div class="col-12 masonry">
                <div v-for="v in vaults" :key="v.id" class="my-3 mx-4 py-2">
                    <VaultCard :vault="v" />
                </div>
            </div>
        </section>
        <section class="row justify-content-start">
            <h1>Keeps</h1>
            <div class="col-12 masonry">
                <div v-for="k in keeps" :key="k.id" class="my-3 mx-4 py-2">
                    <KeepCard :keep="k" />
                </div>
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import VaultCard from '../components/VaultCard.vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { profilesService } from '../services/ProfilesService';
import { useRoute } from 'vue-router';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute()
        async function getVaultsForProfile() {
            try {
                const profileId = route.params.profileId
                await profilesService.getVaultsForProfile(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getProfileById() {
            try {
                const profileId = route.params.profileId
                await profilesService.getProfileById(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getProfileKeeps() {
            try {
                const profileId = route.params.profileId
                await profilesService.getProfileKeeps(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        watchEffect(() => {
            getVaultsForProfile()
            getProfileKeeps()
            getProfileById()
        })
        return {
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.vaults),
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps),

        };
    },
    components: { VaultCard, KeepCard }
};
</script>


<style lang="scss" scoped>
.img-profile {
    max-width: 100px;
}

.hero-img {
    object-fit: cover;
    object-position: center;
    height: 39vh;
    border-style: solid;
    border-bottom: black;
    border-width: 2px;
    width: 50%;
}

body {
    margin: 0;
    padding: 1rem;
}

.masonry {
    columns: 300px;


    div {
        width: 25vh;
    }
}
</style>