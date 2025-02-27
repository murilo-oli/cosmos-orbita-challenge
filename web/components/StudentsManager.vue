<script setup>
import { ref, onMounted } from 'vue';
import { createStudentSchema, editStudentSchema } from "~/schemas/StudentSchema";

import CustomTable from '~/components/base/CustomTable.vue';
import Toast from '~/components/base/Toast.vue';

const { $api } = useNuxtApp();
const token = useCookie("token");

const students = ref([]);
const openCreateModal = ref(false);
const openUpdateModal = ref(false);
const confirmDeleteModal = ref(false);
const openToast = ref(false);
const textToast = ref("");
const variantToast = ref("success");
const errors = ref({});

const createStudent = ref({
    name: "",
    email: "",
    cpf: "",
    ra: "",
});

const updateStudent = ref({
    name: "",
    email: ""
});

const selectedStudent = ref(null);

function openDeleteModal(studentId) {
    selectedStudent.value = studentId;
    confirmDeleteModal.value = true;
}

function openAddModal() {
    openCreateModal.value = true;
}

function openEditModal(student) {
    updateStudent.value.name = student.name;
    updateStudent.value.email = student.email;

    selectedStudent.value = student.id;
    openUpdateModal.value = true;
}

const validateForm = (schema, form) => {
    const result = schema.safeParse(form.value);
    if (!result.success) {
        errors.value = result.error.flatten().fieldErrors;
        return false;
    }
    errors.value = {};
    return true;
};

async function handleAddStudent() {
    if (!validateForm(createStudentSchema, createStudent)) return;
    openCreateModal.value = false;

    const response = await $api.student.createStudent(token.value, {
        ...createStudent.value
    });

    textToast.value = response.description;

    if (!response.success) {
        variantToast.value = "error";
    } else {
        variantToast.value = "success";

        await getPaginatedStudents();
    }

    openToast.value = true;

    setTimeout(() => {
        openToast.value = false;
    }, 1500);
}

async function handleUpdateStudent() {
    if (selectedStudent.value == null) return;
    openUpdateModal.value = false;
    const response = await $api.student.updateStudent(token.value, {
        ...updateStudent.value
    }, selectedStudent.value);

    textToast.value = response.description;

    if (!response.success) {
        variantToast.value = "error";
    } else {
        variantToast.value = "success";

        await getPaginatedStudents();
    }

    openToast.value = true;

    setTimeout(() => {
        openToast.value = false;
    }, 1500);
}

async function handleDeleteStudent() {
    if (selectedStudent.value == null) return;

    confirmDeleteModal.value = false;

    const response = await $api.student.disableStudent(token.value, false, selectedStudent.value);

    textToast.value = response.description;

    if (!response.success) {
        variantToast.value = "error";
    } else {
        variantToast.value = "success";

        await getPaginatedStudents();
    }

    openToast.value = true;

    setTimeout(() => {
        openToast.value = false;
    }, 1500);
}

async function getPaginatedStudents() {
    const response = await $api.student.getPaginatedUser(token.value, {
        IsActive: true,
        CurrentPage: 1,
        PageSize: 10
    });

    textToast.value = response.description;

    if (!response.success) {
        variantToast.value = "error";
    } else {
        variantToast.value = "success";
        students.value = [...response.data];
    }

    openToast.value = true;

    setTimeout(() => {
        openToast.value = false;
    }, 1500);
}

onMounted(async () => {
    await getPaginatedStudents();
});

</script>

<template>
    <section>
        <CustomTable title="Gerir Alunos" :headers="['ra', 'name', 'cpf']" :data="students" :addFunc="openAddModal"
            :editFunc="openEditModal" :deleteFunc="openDeleteModal" />

        <v-dialog v-model="openCreateModal" width="80%">
            <v-card prepend-icon="mdi-account" title="Cadastrar Usuário">
                <v-form @submit.prevent="handleAddStudent" class="px-4 py-4">
                    <v-text-field v-model="createStudent.name" label="Nome" required variant="outlined" class="mb-4"
                        :error-messages="errors.name"></v-text-field>

                    <v-text-field v-model="createStudent.email" label="Email" type="email" required variant="outlined"
                        class="mb-4" :error-messages="errors.email"></v-text-field>

                    <v-text-field v-model="createStudent.cpf" label="CPF" v-mask="'###.###.###-##'" required
                        variant="outlined" class="mb-6" :error-messages="errors.cpf"></v-text-field>

                    <v-text-field v-model="createStudent.ra" label="RA" required variant="outlined" class="mb-6"
                        :error-messages="errors.ra"></v-text-field>

                    <div class="d-flex flex-row align-center justify-end ga-3">
                        <v-btn text="Cancelar" @click="openCreateModal = false"></v-btn>
                        <v-btn text="Adicionar" color="mainBlue" type="submit"></v-btn>
                    </div>

                </v-form>
            </v-card>
        </v-dialog>

        <v-dialog v-model="openUpdateModal" width="80%">
            <v-card prepend-icon="mdi-account" title="Editar Usuário">
                <v-form @submit.prevent="handleUpdateStudent" class="px-4 py-4">
                    <v-text-field v-model="updateStudent.name" label="Nome" required variant="outlined" class="mb-4"
                        :error-messages="errors.name"></v-text-field>

                    <v-text-field v-model="updateStudent.email" label="Email" type="email" required variant="outlined"
                        class="mb-4" :error-messages="errors.email"></v-text-field>

                    <div class="d-flex flex-row align-center justify-end ga-3">
                        <v-btn text="Cancelar" @click="openUpdateModal = false"></v-btn>
                        <v-btn text="Atualizar" color="mainBlue" type="submit"></v-btn>
                    </div>

                </v-form>
            </v-card>
        </v-dialog>

        <v-dialog v-model="confirmDeleteModal" width="50%">
            <v-card prepend-icon="mdi-account" title="Remover Usuário" class="px-4 py-4">
                <p class="pa-5">Deseja realmente remover esse usuário?</p>
                <div class="d-flex flex-row align-center justify-end ga-3">
                    <v-btn text="Cancelar" @click="confirmDeleteModal = false"></v-btn>
                    <v-btn text="Remover" color="mainBlue" @click="handleDeleteStudent"></v-btn>
                </div>
            </v-card>
        </v-dialog>
        <Toast :text="textToast" :model-value="openToast" :variant="variantToast" />
    </section>
</template>