
import type { FetchOptions } from 'ofetch';
import type { AsyncDataOptions } from '#app';

import FetchFactory from '../factory';

class StudentModule extends FetchFactory<object> {
  private RESOURCE = 'student';

  async getPaginatedUser(
    asyncDataOptions?: AsyncDataOptions<object>,
    params?: Record<string, any>
  ) {

    return useAsyncData(
      () => {
        const fetchOptions: FetchOptions<'json'> = {
            params,
        };
        return this.call(
          'GET',
          `${this.RESOURCE}/all`,
          undefined,
          fetchOptions
        )
      },
      asyncDataOptions
    )
  }
}

export default StudentModule;