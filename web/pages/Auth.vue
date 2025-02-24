<script setup lang="ts">
const CAROUSSEL_IMGS = [
    new URL('@/assets/imgs/caroussel_01.jpg', import.meta.url).href,
    new URL('@/assets/imgs/caroussel_02.jpg', import.meta.url).href,
];
</script>

<template>
    <section class="d-flex flex-row min-h-screen">
            <v-carousel cycle hide-delimiter-background show-arrows="hover">
                <v-carousel-item v-for="(img, idx) in CAROUSSEL_IMGS" :key="'img' + idx" :absolute="false" height="100vh" max-width="50%">
                    <v-img 
                        :gradient="`to top, rgba(255,32,59,0.6), rgba(0,0,0,0.3)`" 
                        :src="img"
                        cover>
                    </v-img>
                </v-carousel-item>
            </v-carousel>

        <!-- Seção lateral com 25% da largura -->
        <section>
            <p>Bem-vindo ao sistema</p>
            <v-img height="48" src="../assets/iconBlack.png"></v-img>
        </section>
    </section>
</template>
