<script setup>
const { $api } = useNuxtApp();
import { ref, computed } from "vue";
import { loginSchema, registerSchema } from "~/schemas/AuthSchemas";
import Toast from "./base/Toast.vue";
import { useAuthStore } from '~/stores/useAuthStore';

const authStore = useAuthStore();

const props = defineProps({
  mode: {
    type: String,
    required: true,
    validator: (value) => ["login", "register"].includes(value),
  },
});

const formData = ref({
  name: "",
  email: "",
  password: "",
});

const loading = ref(false);
const errors = ref({});

const openToast = ref(false);
const textToast = ref("");
const variantToast = ref("success");

const schema = computed(() => (props.mode === "register" ? registerSchema : loginSchema));

const validateForm = () => {
  const result = schema.value.safeParse(formData.value);
  if (!result.success) {
    errors.value = result.error.flatten().fieldErrors;
    return false;
  }
  errors.value = {};
  return true;
};

const handleSubmit = async () => {
  if (!validateForm()) return;

  loading.value = true;
  const response = await $api.login.authUser({ Email: formData.value.email, Password: formData.value.password });

  textToast.value = response.description;

  if(response.success){
    const token = useCookie("token");
    authStore.setAuth(response.data.user, response.data.token);
    token.value = response.data.token;
    variantToast.value = "success";
    navigateTo('/', { replace: true })
  }else{
    variantToast.value = "error";
  }
  openToast.value = true;

  loading.value = false;
};
</script>

<template>
  <section class="fill-width w-100">
    <v-form @submit.prevent="handleSubmit">
      <v-text-field v-if="mode === 'register'" v-model="formData.name" label="Nome" required variant="outlined"
        class="mb-4" :error-messages="errors.name"></v-text-field>

      <v-text-field v-model="formData.email" label="Email" type="email" required variant="outlined" class="mb-4"
        :error-messages="errors.email"></v-text-field>

      <v-text-field v-model="formData.password" label="Senha" type="password" required variant="outlined" class="mb-6"
        :error-messages="errors.password"></v-text-field>

      <v-btn color="mainBlue" block size="large" type="submit" :loading="loading">
        {{ mode === "login" ? "Entrar" : "Registrar" }}
      </v-btn>
    </v-form>

    <Toast :text="textToast" :model-value="openToast" :variant="variantToast" />
  </section>
</template>
