import { defineStore } from "pinia";
import { useCookie } from "#app";
import { ref, computed } from "vue";

export const useAuthStore = defineStore("auth", () => {
  const tokenCookie = useCookie("token");
  const token = ref<string | null>(tokenCookie.value || null);
  const user = ref<{ id: number; name: string; email: string; role: number; avatharPath: string } | null>(null);

  function setUserData(userData: any) {
    token.value = userData.token;
    tokenCookie.value = userData.token;
    user.value = userData.user;
  }

  function clearUser() {
    token.value = null;
    tokenCookie.value = null;
    user.value = null;
  }

  return {
    token: computed(() => token.value),
    user,
    setUserData,
    clearUser,
  };
});