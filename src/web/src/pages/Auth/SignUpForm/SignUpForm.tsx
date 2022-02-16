import { Box, Grid, TextField, Link, Typography, Alert } from "@mui/material";
import { useAppDispatch } from "../../../store/hooks";
import { useNavigate } from "react-router-dom";
import { login } from "../../../store/slices/authSlice";
import { apiClient } from "../../../utils/apiClient";
import { signUpFormModelValidationSchema } from "./SignUpFormModelValidationSchema";
import { useFormik } from "formik";
import { SignUpFormModel } from "./SignUpFormModel";
import { useState } from "react";
import { LoadingButton } from "@mui/lab";

export default function SignUpForm() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [error, setError] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const formik = useFormik<SignUpFormModel>({
    initialValues: {
      email: "",
      password: "",
      passwordConfirmation: "",
      name: "",
    },
    validationSchema: signUpFormModelValidationSchema,
    onSubmit: (values) => {
      setError("");
      setIsLoading(true);
      apiClient
        .post("/api/auth/sign-up", values)
        .then(() => {
          dispatch(login());
          navigate("/app");
        })
        .catch((error) => {
          if (error.response) {
            setError(error.response.data);
          } else {
            setError(error.message);
          }
        })
        .finally(() => {
          setIsLoading(false);
        });
    },
  });

  return (
    <>
      <Typography component="h1" variant="h5">
        Sign up
      </Typography>
      {error.length > 0 && (
        <Alert sx={{ mt: 2 }} severity="error">
          {error}
        </Alert>
      )}
      <Box
        component="form"
        noValidate
        onSubmit={formik.handleSubmit}
        sx={{ mt: 3 }}
      >
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              id="name"
              label="Name"
              name="name"
              autoComplete="name"
              value={formik.values.name}
              onChange={formik.handleChange}
              error={formik.touched.name && Boolean(formik.errors.name)}
              helperText={formik.touched.name && formik.errors.name}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              value={formik.values.email}
              onChange={formik.handleChange}
              error={formik.touched.email && Boolean(formik.errors.email)}
              helperText={formik.touched.email && formik.errors.email}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="new-password"
              value={formik.values.password}
              onChange={formik.handleChange}
              error={formik.touched.password && Boolean(formik.errors.password)}
              helperText={formik.touched.password && formik.errors.password}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              name="passwordConfirmation"
              label="Confirm password"
              type="password"
              id="password-confirmation"
              autoComplete="new-password"
              value={formik.values.passwordConfirmation}
              onChange={formik.handleChange}
              error={
                formik.touched.passwordConfirmation &&
                Boolean(formik.errors.passwordConfirmation)
              }
              helperText={
                formik.touched.passwordConfirmation &&
                formik.errors.passwordConfirmation
              }
            />
          </Grid>
        </Grid>
        <LoadingButton
          type="submit"
          loading={isLoading}
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Sign Up
        </LoadingButton>
        <Grid container justifyContent="flex-end">
          <Grid item>
            <Link href="/auth/sign-in" variant="body2">
              Already have an account? Sign in
            </Link>
          </Grid>
        </Grid>
      </Box>
    </>
  );
}
