import { defineStore } from 'pinia';
import type { ApiError } from '~/types/api-error';

export const useErrorStore = defineStore('error', {
  state: () => ({
    status: 0 as number,
    message: '' as string,
  }),
  actions: {
    setError({ status, message }: ApiError) {
      this.status = status;
      this.message = message;
    },
    clearError() {
      this.status = 0;
      this.message = '';
    },
  },
});