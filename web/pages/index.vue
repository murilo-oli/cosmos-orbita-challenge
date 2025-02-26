<script setup>
definePageMeta({
  middleware: ['auth']
});

import { ref, onMounted } from 'vue';
import { useDeviceStore } from '~/stores/useDeviceStore';
import { useAuthStore } from '~/stores/useAuthStore';
const { $api } = useNuxtApp();
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

const students = ref([]);

const updateProfile = () => {
  console.log('Profile updated:', profile.value)
}

const editUser = (user) => {
  console.log('Edit user:', user)
}

const deleteUser = (user) => {
  console.log('Delete user:', user)
}

const handleLogout = () => {
  const token = useCookie('token');
  token.value = "";

  authStore.clearUser();

  navigateTo('/auth', { replace: true })
}

onMounted(async () => {
  console.log("🌻 ~ onMounted ~ token:", token.value)
  const { data, error } = await $api.student.getPaginatedUser({
    headers: {
      'Authorization': `Bearer ${token.value}`,
      'Content-Type': 'application/json',
    }
  }, {
    IsActive: true,
    CurrentPage: 1,
    PageSize: 10
  });

  console.log(data.value)
  console.log(error.value)
});
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
      <section 
      class="d-flex align-start flex-column justify-space-between  h-screen">
        <section>
          <v-img src=""></v-img>
          <span>profile.name</span>
          <v-list>
            <v-list-item prepend-icon="mdi-account-group" title="Gestão de Alunos"
              @click="currentView = 'students'"></v-list-item>
            <v-list-item prepend-icon="mdi-account-edit" title="Editar Conta"
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
        <v-card v-if="currentView === 'profile'" class="mx-auto" max-width="600">
          <v-card-title>Editar Perfil</v-card-title>
          <v-card-text>
            <v-form @submit.prevent="updateProfile">
              <v-text-field v-model="profile.name" label="Nome" variant="outlined" class="mb-4"></v-text-field>
              <v-text-field v-model="profile.password" label="Nova senha" type="password" variant="outlined"
                class="mb-4"></v-text-field>
              <span></span>
              <v-btn color="primary" type="submit" block>
                Atualizar Dados
              </v-btn>
            </v-form>
          </v-card-text>
        </v-card>

        <v-card v-else class="mx-auto">
          <v-card-title>Gerir Alunos</v-card-title>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>RA</th>
                  <th>Nome</th>
                  <th>CPF</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="user in students" :key="user.ra">
                  <td>{{ user.ra }}</td>
                  <td>{{ user.name }}</td>
                  <td>{{ user.cpf }}</td>
                  <td>
                    <v-btn icon="mdi-pencil" size="small" class="mr-2" @click="editUser(user)"></v-btn>
                    <v-btn icon="mdi-delete" size="small" color="error" @click="deleteUser(user)"></v-btn>
                  </td>
                </tr>
              </tbody>
            </v-table>
          </v-card-text>
        </v-card>
      </v-container>
    </v-main>
  </v-app>
</template>
