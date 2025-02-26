<script setup>
const props = defineProps({
    text: {
        type: String,
        required: true,
    },
    modelValue: {
        type: Boolean,
        required: true
    },
    variant: {
        type: String,
        required: true,
        validator: (value) => ["error", "success"].includes(value),
    }
});

const emit = defineEmits(["update:modelValue"]);
</script>
<template>
    <v-snackbar :model-value="modelValue" @update:model-value="emit('update:modelValue', $event)" :timeout="1500"
        :color="variant">
        {{ text }}
    </v-snackbar>
</template>
