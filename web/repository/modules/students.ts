
import type { FetchOptions } from 'ofetch';

import FetchFactory from '../factory';

interface CreateStudent {
  CPF: string,
  RA: string,
  Name: string,
  Email: string,
};

interface UpdateStudent {
  CPF: string,
  RA: string,
  Name: string,
  Email: string,
};

class StudentModule extends FetchFactory<object> {
  async getPaginatedUser(
    token: string,
    params?: Record<string, any>,
  ): Promise<any> {
    const fetchOptions: FetchOptions<'json'> = {
      method: "GET",
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      }, params
    };
    return this.call('GET', 'student/all', undefined, fetchOptions);
  }

  async createStudent(
    token: string,
    body?: CreateStudent
  ): Promise<any> {
    const fetchOptions: FetchOptions<'json'> = {
      method: "POST",
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      },
      body
    };
    return this.call('POST', 'student', body, fetchOptions);
  }

  async updateStudent(
    token: string,
    body?: UpdateStudent,
    id?:number
  ): Promise<any> {
    const fetchOptions: FetchOptions<'json'> = {
      method: "PUT",
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      },
      body
    };
    return this.call('PUT', `student/${id}`, body, fetchOptions);
  }

  async disableStudent(
    token: string,
    status?: Boolean,
    id?:number
  ): Promise<any> {
    const fetchOptions: FetchOptions<'json'> = {
      method: "PATCH",
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      },
    };
    return this.call('PATCH', `student/${id}/${status}`, undefined, fetchOptions);
  }


}

export default StudentModule;