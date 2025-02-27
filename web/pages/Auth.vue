<script setup>
import { useDeviceStore } from '~/stores/useDeviceStore';
import AuthForm from '~/components/AuthForm.vue';
const deviceStore = useDeviceStore();
const formMode = ref("login");

const carouselSlides = [
  {
    image: new URL('@/assets/imgs/caroussel/caroussel_01.jpg', import.meta.url).href,
    title: 'Matrícula Simplificada',
    description: 'Gerencie seus alunos de maneira simples e rápida, para focar no que realmente importa: a educação.'
  },
  {
    image: new URL('@/assets/imgs/caroussel/caroussel_02.jpg', import.meta.url).href,
    title: 'Simples e Inteligente',
    description: 'Esqueça os inúmeros formulários, nosso trabalho é faciltar o seu.'
  },
  {
    image: new URL('@/assets/imgs/caroussel/caroussel_03.jpg', import.meta.url).href,
    title: 'Sempre Disponível',
    description: 'Tenha tudo o que precisa na palma da sua mão e 100% online.'
  }
]

const toggleForm = () => {
  formMode.value = formMode.value === "login" ? "register" : "login";
};
</script>

<template>
  <div class="login-container">
    <v-carousel v-if="!deviceStore.isMobile" class="carousel-section" cycle hide-delimiter-background show-arrows="hover"
      height="100vh">
      <v-carousel-item v-for="(slide, i) in carouselSlides" :key="i" :src="slide.image" cover position="top">
        <div class="carousel-gradient">
          <div class="caroussel-text">
            <h2 class="text-h2 font-weight-bold text-white">{{ slide.title }}</h2>
            <p class="text-subtitle-1 text-white">{{ slide.description }}</p>
          </div>
        </div>
      </v-carousel-item>
    </v-carousel>

    <div class="login-form-section">
      <v-container class="d-flex flex-column align-center justify-center fill-height">
        <section class="w-full d-flex flex-column ga-15">
          <v-img src="../assets/imgs/logoBlack.svg" height="48"></v-img>
          <section>
            <h1 class="text-h4 font-weight-bold mb-6">Boas vindas!</h1>
            <p class="text-subtitle-1 mb-6">É bom ter você por aqui novamente.</p>

            <AuthForm :mode="formMode" />

            <p class="text-center mt-5">
              {{ formMode === 'login' ? 'Ainda não possui conta? ' : '' }}
              <span class="text-mainRed cursor-pointer" @click="toggleForm">
                {{ formMode === 'login' ? 'Crie uma agora mesmo!' : 'Voltar para o login.' }}
              </span>
            </p>
          </section>
        </section>
      </v-container>
    </div>
  </div>
</template>



<style scoped>
.login-container {
  display: flex;
  height: 100vh;
}

.carousel-section {
  width: 50%;
}

.carousel-gradient {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(to top, #ff203ab2, #0000001a);
  display: flex;
  align-items: flex-end;
  padding: 2rem;
}

.caroussel-text {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  row-gap: 0.3rem;
  margin-bottom: 3rem;
  max-width: 80%;
}

.login-form-section {
  width: 50%;
}

.login-card {
  background-color: white;
}

@media (max-width: 767px) {
  .carousel-section {
    display: none;
  }

  .login-form-section {
    width: 100%;
  }
}
</style>