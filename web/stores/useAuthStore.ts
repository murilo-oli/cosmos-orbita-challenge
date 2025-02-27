import { defineStore } from "pinia";

interface User {
  id: number; name: string; email: string; role: number; avatharPath: string
}

export const useAuthStore = defineStore("user", {
  state: () => {
    return {
      user: {} as User,
      token: "" as String
    }
  },
  actions: {
    setAuth(user: User, token: string) {
      this.user = user;
      this.token = token;
    },
    clearAuth() {
      this.user = {} as User;
      this.token = "";
    },
  }
});