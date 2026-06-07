import { defineNuxtPlugin } from 'nuxt/app';

export default defineNuxtPlugin(async () => {
  if (process.client) {
    const bootstrap = await import('bootstrap'); // importa só no navegador
    (window as any).bootstrap = bootstrap;
    console.log('Bootstrap carregado no cliente:', bootstrap);
  }
});