import { z } from "zod"

export const loginSchema = z.object({
    email: z.string().email({ message: "Insira um email válido." }).min(3, { message: "O email deve ter ao menos 3 caracteres." }).max(320, { message: 'O email deve ter no máximo 320 caracteres.' }),
    password: z.string().min(3, { message: "A senha deve ter de 3 à 20 caracteres." }).max(20,{message:"A senha deve ter de 8 à 20 caracteres."}),
})

export type LoginData = z.infer<typeof loginSchema>
export type LoginErrors = Partial<Record<keyof LoginData, string>>

export const registerSchema = z.object({
    name: z.string().min(3, { message: "O nome deve ter ao menos 3 caracteres." }).max(100, { message: 'O nome deve ter no máximo 320 caracteres.' }),
    email: z.string().email({ message: "Insira um email válido." }).min(3, { message: "O email deve ter ao menos 3 caracteres." }).max(320, { message: 'O email deve ter no máximo 320 caracteres.' }),
    password: z.string().min(8, { message: "A senha deve ter de 8 à 20 caracteres." }).max(20,{message:"A senha deve ter de 8 à 20 caracteres."}),
})

export type RegisterData = z.infer<typeof registerSchema>
export type RegisterErrors = Partial<Record<keyof RegisterData, string>>