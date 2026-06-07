
<template>
  <div class="layout">
    <!-- Menu lateral -->
    <aside class="sidebar">
      <h2>Estoque dentro</h2>
      <nav>
        <ul>
          <li><NuxtLink to="/">🏠 Dashboard</NuxtLink></li>
          <li><NuxtLink to="/produtos">📦 Produtos</NuxtLink></li>
          <li><NuxtLink to="/categorias">📂 Categorias</NuxtLink></li>
        </ul>
      </nav>
    </aside>

    <!-- Conteúdo principal -->
    <main class="content">
      <slot />
    </main>
 
  </div>
</template>

<style scoped>
.layout {
  display: flex;
  min-height: 100vh;
}

.sidebar {
  width: 220px;
  background: #2c3e50;
  color: #fff;
  padding: 1rem;
}

.sidebar h2 {
  margin-bottom: 1rem;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar li {
  margin: 0.5rem 0;
}

.sidebar a {
  color: #ecf0f1;
  text-decoration: none;
}

.sidebar a:hover {
  text-decoration: underline;
}

.content {
  flex: 1;
  padding: 2rem;
  background: #f5f6fa;
}
</style>
