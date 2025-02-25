<template>
    <div class="login-container">
      <!-- Carousel Section - Hidden on mobile -->
      <v-carousel
        v-if="!isMobile"
        class="carousel-section"
        cycle
        hide-delimiter-background
        show-arrows="hover"
        height="100vh"
      >
        <v-carousel-item
          v-for="(slide, i) in carouselSlides"
          :key="i"
          :src="slide.image"
          cover
        >
          <div class="carousel-gradient">
            <div class="carousel-text">
              <h2 class="text-h4 font-weight-bold white--text">{{ slide.title }}</h2>
              <p class="text-subtitle-1 white--text">{{ slide.description }}</p>
            </div>
          </div>
        </v-carousel-item>
      </v-carousel>
  
      <!-- Login Form Section -->
      <div class="login-form-section">
        <v-container class="d-flex align-center justify-center fill-height">
          <v-card class="login-card pa-8" elevation="0" max-width="400" width="100%">
            <h1 class="text-h4 font-weight-bold mb-6">Welcome Back!</h1>
            <p class="text-subtitle-1 mb-6">Please sign in to continue</p>
  
            <v-form @submit.prevent="handleLogin">
              <v-text-field
                v-model="email"
                label="Email"
                type="email"
                required
                variant="outlined"
                class="mb-4"
              ></v-text-field>
  
              <v-text-field
                v-model="password"
                label="Password"
                type="password"
                required
                variant="outlined"
                class="mb-6"
              ></v-text-field>
  
              <v-btn
                color="primary"
                block
                size="large"
                type="submit"
                :loading="loading"
              >
                Sign In
              </v-btn>
            </v-form>
          </v-card>
        </v-container>
      </div>
    </div>
  </template>
  
  <script setup>
  const router = useRouter()
  const email = ref('')
  const password = ref('')
  const loading = ref(false)
  const isMobile = ref(false)
  
  const carouselSlides = [
    {
      image: 'https://images.unsplash.com/photo-1497215728101-856f4ea42174',
      title: 'Welcome to Our Platform',
      description: 'Your journey starts here'
    },
    {
      image: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40',
      title: 'Manage Your Business',
      description: 'Streamline your operations'
    },
    {
      image: 'https://images.unsplash.com/photo-1552664730-d307ca884978',
      title: 'Grow Together',
      description: 'Join our community of professionals'
    }
  ]
  
  const handleLogin = async () => {
    loading.value = true
    try {
      // Simulate login delay
      await new Promise(resolve => setTimeout(resolve, 1000))
      // Navigate to dashboard
      await router.push('/dashboard')
    } catch (error) {
      console.error('Login failed:', error)
    } finally {
      loading.value = false
    }
  }
  
  onMounted(() => {
    const checkMobile = () => {
      isMobile.value = window.innerWidth < 768
    }
    
    checkMobile()
    window.addEventListener('resize', checkMobile)
    
    onUnmounted(() => {
      window.removeEventListener('resize', checkMobile)
    })
  })
  </script>
  
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
    background: linear-gradient(to bottom, rgba(0,0,0,0.1), rgba(0,0,0,0.7));
    display: flex;
    align-items: flex-end;
    padding: 2rem;
  }
  
  .login-form-section {
    width: 50%;
    background-color: #f5f5f5;
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