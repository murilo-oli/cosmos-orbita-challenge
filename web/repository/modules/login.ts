import type { FetchOptions } from 'ofetch';

import FetchFactory from '../factory';

type ILogin = {
  email: string;
  password: string;
};

type AuthResponse = {
  token: string;
  user: { id: number; name: string; email: string; role: number; avatarPath: string };
};

class LoginModule extends FetchFactory<AuthResponse> {
  private RESOURCE = 'auth/login';

  async authUser(body: ILogin): Promise<AuthResponse> {
    const fetchOptions: FetchOptions<'json'> = {};
    return this.call('POST', `${this.RESOURCE}`, body, fetchOptions);
  }
}

export default LoginModule;
