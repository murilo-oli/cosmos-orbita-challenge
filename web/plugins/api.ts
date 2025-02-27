
import { $fetch, type FetchOptions } from 'ofetch';

import LoginModule from '~/repository/modules/login';
import StudentModule from '~/repository/modules/students';

interface IApiInstance {
  login: LoginModule;
  student: StudentModule
}

export default defineNuxtPlugin((nuxtApp) => {
  const config = useRuntimeConfig();

  const fetchOptions: FetchOptions = {
    baseURL: config.public.apiBaseUrl
  };

  const apiFecther = $fetch.create(fetchOptions);

  const modules: IApiInstance = {
    login: new LoginModule(apiFecther),
    student: new StudentModule(apiFecther)
  };

  return {
    provide: {
      api: modules
    }
  };
});