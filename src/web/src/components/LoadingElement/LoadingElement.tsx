import {
  Box,
  CircularProgress,
  Container,
  CssBaseline,
  Typography,
} from "@mui/material";

function LoadingElement() {
  return (
    <Container component="main" maxWidth="xs" sx={{ mt: "15rem" }}>
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h5">
          Loading - please wait...
        </Typography>
        <CircularProgress size="10rem" sx={{ mt: "3rem" }} />;
      </Box>
    </Container>
  );
}

export default LoadingElement;
