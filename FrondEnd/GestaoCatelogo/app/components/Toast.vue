<script setup>
import { ref } from 'vue'

const mensagens = ref([])

function addMensagem(texto, tipo = 'info') {
  const id = Date.now()
  mensagens.value.push({ id, texto, tipo })
  setTimeout(() => {
    mensagens.value = mensagens.value.filter(m => m.id !== id)
  }, 4000) // desaparece após 4s
}

defineExpose({ addMensagem })
</script>

<template>
  <div class="toast-container">
    <div 
      v-for="m in mensagens" 
      :key="m.id" 
      class="toast" 
      :class="m.tipo"
    >
      {{ m.texto }}
    </div>
  </div>
</template>

<style scoped>
.toast-container {
  position: fixed;
  top: 1rem;
  right: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  z-index: 9999;
}

.toast {
  padding: 0.75rem 1rem;
  border-radius: 4px;
  color: #fff;
  font-weight: 500;
  box-shadow: 0 2px 6px rgba(0,0,0,0.2);
}

.toast.info { background: #3498db; }
.toast.success { background: #2ecc71; }
.toast.error { background: #e74c3c; }
</style>
