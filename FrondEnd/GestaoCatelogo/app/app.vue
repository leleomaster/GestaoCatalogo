<script setup lang="ts">
import { onMounted, watch } from 'vue';
import { useState } from 'nuxt/app';

const apiError = useState<{ status: number; message: string }>('apiError', () => ({
  status: 0,
  message: '',
}));

onMounted(() => {

  watch(apiError, (err) => {
    try {
      if (process.client && err.message) {
        const modalEl = document.getElementById('errorModal');

        if (modalEl) {
          const modal = new (window as any).bootstrap.Modal(modalEl);
          modal.show();
        } else {
          console.warn('Bootstrap Modal não disponível ou elemento não encontrado');
        }
      }
    } catch (e) {
      console.error('Erro no watcher do apiError:', e);
    }
  });
});
</script>

<template>
  <NuxtLayout>
    <NuxtPage />
  </NuxtLayout>

  <!-- Modal de Erro -->
  <div class="modal fade" id="errorModal" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-danger text-white">
          <h5 class="modal-title">Erro</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>
        <div class="modal-body">
          <p>{{ apiError.message }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
