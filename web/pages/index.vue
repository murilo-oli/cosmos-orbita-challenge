<template>
    <v-app>
      <!-- Mobile Menu -->
      <v-app-bar v-if="isMobile" color="primary">
        <v-app-bar-title>Dashboard</v-app-bar-title>
        <v-btn icon @click="drawer = !drawer">
          <v-icon>mdi-menu</v-icon>
        </v-btn>
      </v-app-bar>
  
      <!-- Navigation Drawer -->
      <v-navigation-drawer
        v-model="drawer"
        :permanent="!isMobile"
        :temporary="isMobile"
      >
        <v-list>
          <v-list-item
            prepend-icon="mdi-account-edit"
            title="Edit Profile"
            @click="currentView = 'profile'"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-account-group"
            title="Manage Users"
            @click="currentView = 'users'"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <!-- Main Content -->
      <v-main>
        <v-container fluid>
          <!-- Edit Profile Form -->
          <v-card v-if="currentView === 'profile'" class="mx-auto" max-width="600">
            <v-card-title>Edit Profile</v-card-title>
            <v-card-text>
              <v-form @submit.prevent="updateProfile">
                <v-text-field
                  v-model="profile.name"
                  label="Name"
                  variant="outlined"
                  class="mb-4"
                ></v-text-field>
                <v-text-field
                  v-model="profile.email"
                  label="Email"
                  type="email"
                  variant="outlined"
                  class="mb-4"
                ></v-text-field>
                <v-text-field
                  v-model="profile.password"
                  label="New Password"
                  type="password"
                  variant="outlined"
                  class="mb-4"
                ></v-text-field>
                <v-btn color="primary" type="submit" block>
                  Update Profile
                </v-btn>
              </v-form>
            </v-card-text>
          </v-card>
  
          <!-- Users List -->
          <v-card v-else class="mx-auto">
            <v-card-title>Manage Users</v-card-title>
            <v-card-text>
              <v-table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="user in users" :key="user.id">
                    <td>{{ user.name }}</td>
                    <td>{{ user.email }}</td>
                    <td>
                      <v-btn
                        icon="mdi-pencil"
                        size="small"
                        class="mr-2"
                        @click="editUser(user)"
                      ></v-btn>
                      <v-btn
                        icon="mdi-delete"
                        size="small"
                        color="error"
                        @click="deleteUser(user)"
                      ></v-btn>
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
  
  <script setup>
  const isMobile = ref(false)
  const drawer = ref(!isMobile.value)
  const currentView = ref('profile')
  
  const profile = ref({
    name: 'John Doe',
    email: 'john@example.com',
    password: ''
  })
  
  const users = ref([
    { id: 1, name: 'Jane Smith', email: 'jane@example.com' },
    { id: 2, name: 'Bob Johnson', email: 'bob@example.com' },
    { id: 3, name: 'Alice Brown', email: 'alice@example.com' }
  ])
  
  const updateProfile = () => {
    // Implement profile update logic
    console.log('Profile updated:', profile.value)
  }
  
  const editUser = (user) => {
    // Implement edit user logic
    console.log('Edit user:', user)
  }
  
  const deleteUser = (user) => {
    // Implement delete user logic
    console.log('Delete user:', user)
  }
  
  onMounted(() => {
    const checkMobile = () => {
      isMobile.value = window.innerWidth < 768
      drawer.value = !isMobile.value
    }
    
    checkMobile()
    window.addEventListener('resize', checkMobile)
    
    onUnmounted(() => {
      window.removeEventListener('resize', checkMobile)
    })
  })
  </script>