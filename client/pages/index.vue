<template>
  <v-container>
    <v-card>
      <v-card-title>Categorias</v-card-title>
      <v-card-text>
        <v-tabs
          v-model="categoriaSelecionada"
          class="rounded"
          bg-color="grey-lighten-4"
          center-active
          grow
        >
          <v-tab
            v-for="categoria in categorias"
            :key="categoria.id"
            :value="categoria.id"
            @click="fetchProdutos(categoria.id)"
          >
            {{ categoria.nome }}
          </v-tab>
        </v-tabs>

        <v-table>
          <thead>
            <tr class="font-weight-bold">
              <th class="text-left">Nome</th>
              <th class="text-left">Descrição</th>
              <th class="text-left">Estoque</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="produto in produtos" :key="produto.id">
              <td>{{ produto.nome }}</td>
              <td>{{ produto.descricao }}</td>
              <td>{{ produto.estoque }}</td>
              <td class="text-center">
                <v-btn
                  class="mr-3"
                  density="comfortable"
                  icon="mdi-pencil"
                  variant="elevated"
                ></v-btn>
                <v-btn
                  density="comfortable"
                  icon="mdi-delete"
                  color="error"
                  variant="tonal"
                ></v-btn>
              </td>
            </tr>
          </tbody>
        </v-table>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

// Reactive data
const categorias = ref([]);
const produtos = ref([]);
const categoriaSelecionada = ref(null);

// Fetch categories from API when the component is mounted
const fetchCategorias = async () => {
  try {
    const response = await axios.get("https://localhost:7118/api/Categorias");
    categorias.value = response.data;
    // Set the first category as selected by default
    if (categorias.value.length > 0) {
      categoriaSelecionada.value = categorias.value[0].id;
      fetchProdutos(categorias.value[0].id);
    }
  } catch (error) {
    console.error("Error fetching categories:", error);
  }
};

// Fetch products for a specific category
const fetchProdutos = async (categoriaId) => {
  try {
    const response = await axios.get(
      `https://localhost:7118/api/Categorias/${categoriaId}/produtos`
    );
    produtos.value = response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
  }
};

onMounted(fetchCategorias);
</script>

<style scoped>
/* Add custom styles if needed */
</style>