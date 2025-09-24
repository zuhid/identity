export interface User {
  email?: string;
  password?: string;
  confirmPassword?: string;
  phone?: string;
  tfaToken?: string;
  emailToken?: string;
  phoneToken?: string;
}
