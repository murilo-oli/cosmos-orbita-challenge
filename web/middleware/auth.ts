import {useAuthStore} from '~/stores/useAuthStore';

export default function defineNuxtRouteMiddleware() {
    const authStore = useAuthStore();
    const cookieToken = useCookie("token");
    
    const authenticated = cookieToken.value || authStore.token != "";

    if (!authenticated) {
        return navigateTo('/auth', {replace:true})
    }
}