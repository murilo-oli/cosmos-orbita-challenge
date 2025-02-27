<script setup>
definePageMeta({
  middleware: ['auth']
});

import { ref } from 'vue';
import { useDeviceStore } from '~/stores/useDeviceStore';
import { useAuthStore } from '~/stores/useAuthStore';
import StudentsManager from '~/components/StudentsManager.vue';
const authStore = useAuthStore();
const deviceStore = useDeviceStore();
const drawer = ref(!deviceStore.isMobile)
const currentView = ref('students')
const token = useCookie("token");

const profile = ref({
  name: '',
  password: '',
  avatarPath: '',
});


function updateProfile() {
  console.log('Profile updated:', profile.value)
}

const handleLogout = () => {
  token.value = "";

  authStore.clearAuth();

  navigateTo('/auth', { replace: true });
}


</script>

<template>
  <v-app>
    <v-app-bar v-if="deviceStore.isMobile" color="primary">
      <v-app-bar-title>Dashboard</v-app-bar-title>
      <v-btn icon @click="drawer = !drawer">
        <v-icon>mdi-menu</v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer app v-model="drawer" :permanent="!deviceStore.isMobile" :temporary="deviceStore.isMobile">
      <section class="d-flex align-start flex-column justify-space-between  h-screen">
        <section>
          <div class="d-flex align-center flex-row mt-5 px-3 ga-6">
            <v-avatar color="success" size="32">
              <v-icon icon="mdi-account-circle"></v-icon>
            </v-avatar>
            <span class="font-weight-bold">{{ authStore.user.name }}</span>
          </div>
          <v-list>
            <v-list-item prepend-icon="mdi-account-group" title="Gestão de Alunos"
              @click="currentView = 'students'"></v-list-item>
            <v-list-item prepend-icon="mdi-account-edit" title="Gestão de Usuários"
              @click="currentView = 'profile'"></v-list-item>
            <v-list-item prepend-icon="mdi-logout-variant" title="Sair" @click="handleLogout"></v-list-item>
          </v-list>
        </section>
        <div class="align-self-center">
          <v-img src="~/assets/imgs/logoBlack.svg" height="32" style="height: 128px; width: 128px;"></v-img>
        </div>
      </section>
    </v-navigation-drawer>

    <v-main>
      <v-container fluid>
        <v-card v-if="currentView === 'profile'" class="mx-auto">

        </v-card>

        <v-card v-else class="mx-auto">
            <StudentsManager/>          
        </v-card>
      </v-container>
    </v-main>  
  </v-app>
</template>
