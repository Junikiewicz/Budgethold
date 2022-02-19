import { useNavigate } from "react-router-dom";
import {
  Box,
  TextField,
  Grid,
  Link,
  Typography,
  Alert,
} from "@mui/material";
import { useAppDispatch } from "../../../store/hooks";
import { login } from "../../../store/slices/authSlice";
import { useFormik } from "formik";
import { SignInRequest } from "./SignInRequest";
import { apiClient } from "../../../utils/apiClient";
import { signInRequestValidationSchema } from "./SignInRequestValidationSchema";
import { useState } from "react";
import LoadingButton from "@mui/lab/LoadingButton";

export default function SignInForm() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [error, setError] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const formik = useFormik<SignInRequest>({
    initialValues: {
      email: "",
      password: "",
    },
    validationSchema: signInRequestValidationSchema,
    onSubmit: (values) => {
      setError("");
      setIsLoading(true);
      apiClient
        .post("/auth/sign-in", values)
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
        Sign in
      </Typography>
      {error.length > 0 && (
        <Alert sx={{ mt: 2 }} severity="error">
          {error}
        </Alert>
      )}
      <Box
        component="form"
        onSubmit={formik.handleSubmit}
        noValidate
        sx={{ mt: 1 }}
      >
        <TextField
          margin="normal"
          required
          fullWidth
          id="email"
          label="Email Address"
          name="email"
          autoComplete="email"
          autoFocus
          value={formik.values.email}
          onChange={formik.handleChange}
          error={formik.touched.email && Boolean(formik.errors.email)}
          helperText={formik.touched.email && formik.errors.email}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          name="password"
          label="Password"
          type="password"
          id="password"
          autoComplete="current-password"
          value={formik.values.password}
          onChange={formik.handleChange}
          error={formik.touched.password && Boolean(formik.errors.password)}
          helperText={formik.touched.password && formik.errors.password}
        />
        <LoadingButton
          type="submit"
          fullWidth
          loading={isLoading}
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Sign In
        </LoadingButton>
        <Grid container justifyContent="flex-end">
          <Grid item>
            <Link href="/auth/sign-up" variant="body2">
              {"Don't have an account? Sign Up"}
            </Link>
          </Grid>
        </Grid>
      </Box>
    </>
  );
}
