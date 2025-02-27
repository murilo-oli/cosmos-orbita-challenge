import { defineStore } from "pinia";
import { ref, onMounted, onUnmounted } from "vue";

export const useDeviceStore = defineStore("device", () => {
  const isMobile = ref(false);

  const checkMobile = () => {
    isMobile.value = window.innerWidth < 768;
  };

  onMounted(() => {
    checkMobile();
    window.addEventListener("resize", checkMobile);
  });

  onUnmounted(() => {
    window.removeEventListener("resize", checkMobile);
  });

  return { isMobile };
});
