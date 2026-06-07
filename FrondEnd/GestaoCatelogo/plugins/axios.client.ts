
import axios from 'axios';
import { defineNuxtPlugin, useState } from '#app';
import type { ApiError } from '~/types/api-error';

export default defineNuxtPlugin((nuxtApp) => {

  const config = useRuntimeConfig()

  const api = axios.create({
    baseURL: config.public.apiBase,
    withCredentials: false // se não usar cookies
  })

   api.interceptors.response.use(
    (response) => response,
    (error) => {
      
      if (error.response) {
        const status = error.response.status;
        const message = error.response.data?.message || 'Erro inesperado';

        // Atualiza estado global de erro
        const errorState = useState<ApiError>('apiError', () => ({
          status: 0,
          message: '',
        }));

        errorState.value = { status, message };
      } else {
        const errorState = useState<ApiError>('apiError', () => ({
          status: 0,
          message: '',
        }));
        errorState.value = { status: 0, message: 'Erro de rede ou servidor indisponível' };
      }

      return Promise.reject(error);
    }
  );

  nuxtApp.provide('api', api);
});
