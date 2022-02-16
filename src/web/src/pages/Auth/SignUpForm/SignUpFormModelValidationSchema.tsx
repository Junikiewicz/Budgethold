import * as yup from "yup";

export const signUpFormModelValidationSchema = yup.object({
  name: yup.string().required("Name is required"),
  email: yup
    .string()
    .email("Enter a valid email")
    .required("Email is required"),
  password: yup
    .string()
    .min(6, "Password should be of minimum 6 characters length")
    .required("Password is required")
    .matches(/[a-z]/, "Password must contain one or more lowercase letters.")
    .matches(/[A-Z]/, "Password must contain one or more capital letters.")
    .matches(/\d/, "Password must contain one or more digits.")
    .matches(/[""!@$%^&*(){}:;<>,.?/+_=|'~\\-]/, "Password must contain one or more special characters."),
  passwordConfirmation: yup
    .string()
    .oneOf([yup.ref('password'), null], 'Passwords must match')
    .required("Password confirmation is required"),
});
