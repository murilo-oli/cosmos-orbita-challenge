
import type { FetchOptions } from 'ofetch';
import type { AsyncDataOptions } from '#app';

import FetchFactory from '../factory';

type ILogin = {
  email: string,
  password: string
};

type IAuthReturn = {
  Token: string,
  User: {
    Id: number,
    Name: string,
    Email: string,
    Role: number
  }
}

class LoginModule extends FetchFactory<object> {
  private RESOURCE = 'auth/login';

  async authUser(
    asyncDataOptions?: AsyncDataOptions<object>,
    body?: ILogin
  ) {

    return useAsyncData(
      () => {
        const fetchOptions: FetchOptions<'json'> = {};
        return this.call(
          'POST',
          `${this.RESOURCE}`,
          body,
          fetchOptions
        )
      },
      asyncDataOptions
    )
  }
}

export default LoginModule;