import tsconfigPaths from 'vite-tsconfig-paths'

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },

  // CSS global
  vite: {
    plugins: [tsconfigPaths()],
     optimizeDeps: {
      include: [
        '@vue/devtools-core',
        '@vue/devtools-kit',
        'axios',
        'bootstrap' // força Vite a incluir o pacote
      ]
    }
  },
  css: [
    '../assets/styles.css', // ou './assets/styles.css'
    'bootstrap/dist/css/bootstrap.min.css'
  ],
  
  plugins: [
    '../plugins/bootstrap.client.ts',
    '../plugins/axios.client.ts'
  ],

  // Configurações de build
  build: {
    transpile: []
  },

  // Configurações de alias (atalhos de importação)
  alias: {
    '@components': '/<rootDir>/components',
    '@composables': '/<rootDir>/composables',
    '@assets': '/<rootDir>/assets'
  },

  // Módulos úteis
  modules: [
    '@pinia/nuxt',        // Gerenciamento de estado
    '@vueuse/nuxt'        // Coleção de composables prontos
  ],

  // Configuração de runtime (variáveis de ambiente)
  runtimeConfig: {
    public: {
      apiBase: process.env.API_BASE || 'http://localhost:5000/api'
    }
  }

})
