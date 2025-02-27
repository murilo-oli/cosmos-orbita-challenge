import { z } from "zod"

export const createStudentSchema = z.object({
    name: z.string().min(3, { message: "Mínimo de 3 caracteres." }).max(100, { message: "Máximo de 100 caracteres." }),
    cpf: z.string().min(14, { message: "O CPF deve ter 11 caracteres." }).max(14, { message: "O CPF deve ter 11 caracteres." }),
    ra: z.string(),
    email: z.string().email({ message: "Insira um email válido." }).min(3, { message: "Mínimo de 3 caracteres." }).max(320,{ message: "Máximo de 320 caracteres." }),
})

export type CreateStudentData = z.infer<typeof createStudentSchema>
export type CreateStudentErrors = Partial<Record<keyof CreateStudentData, string>>

export const editStudentSchema = z.object({
    name: z.string().min(3, { message: "Mínimo de 3 caracteres." }).max(100, { message: "Máximo de 100 caracteres." }),
    email: z.string().email({ message: "Insira um email válido." }).min(3, { message: "Mínimo de 3 caracteres." }).max(320,{ message: "Máximo de 320 caracteres." }),
})

export type UpdateStudentData = z.infer<typeof editStudentSchema>
export type UpdateStudentErrors = Partial<Record<keyof UpdateStudentData, string>>