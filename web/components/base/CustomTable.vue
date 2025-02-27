<script setup>
const props = defineProps({
  title: {
    type: String,
    required: true,
  },
  headers:{
    type: Array,
    required:true
  },
  data:{
    type: Array,
    required:true
  },
  addFunc:{
    type: Function,
    required:true
  },
  editFunc:{
    type: Function,
    required:true
  },
  deleteFunc:{
    type: Function,
    required:true
  },
});
</script>

<template>
    <v-card-title>{{title}}</v-card-title>
    <v-card-text>
        <v-btn prepend-icon="mdi-plus-circle-outline" variant="tonal" @click="addFunc">Adicionar</v-btn>
        <v-table v-if="headers.length > 0">
            <thead>
                <tr>
                    <th v-for="(header, headerIdx) in headers" :key="`header-${headerIdx}`" >{{ header.toUpperCase() }}</th>
                </tr>
            </thead>
            <tbody v-if="data.length > 0">
                <tr v-for="(item, itemIdx) in data" :key="`data-${itemIdx}`">
                    <td v-for="(itemHeader, itemHeaderIdx) in headers" :key="`itemHeader-${itemHeaderIdx}`">{{ item[itemHeader] }}</td>
                    <td>
                        <v-btn icon="mdi-pencil" size="small" class="mr-2" @click="editFunc(item)"></v-btn>
                        <v-btn icon="mdi-delete" size="small" color="error" @click="deleteFunc(item.id)"></v-btn>
                    </td>
                </tr>
            </tbody>
        </v-table>
    </v-card-text>
</template>