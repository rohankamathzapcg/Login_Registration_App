import TextField from "@material-ui/core/TextField";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import { useState } from "react";
import axios from "axios";

const Login = () => {
  const navigate = useNavigate();
  const [login, setLoginDetails] = useState({
    username: "",
    password: "",
    role: "User",
    userFlag: false,
    passFlag: false,
  });
  const checkValidity = () => {
    let newUserFlag = false;
    let newPassFlag = false;

    if (login.username === "") {
      newUserFlag = true;
    }
    if (login.password === "") {
      newPassFlag = true;
    }

    setLoginDetails((prevState) => ({
      ...prevState,
      userFlag: newUserFlag,
      passFlag: newPassFlag,
    }));
  };

  const handleSubmit = () => {
    checkValidity();
    if (login.username !== "" && login.password !== "") {
      let data = {
        userName: login.username,
        password: login.password,
        role: login.role,
      };
      axios
        .post("https://localhost:44376/api/Auth/Login", data)
        .then((result) => {
          if (result.data.isSuccess) {
            toast.success(result.data.message, {
              theme: "dark",
              autoClose: 1000,
            });
            setTimeout(() => {
              navigate("/homepage");
            }, 2000);
          } else {
            toast.error(result.data.message, {
              theme: "dark",
              autoClose: 1000,
            });
          }
        })
        .catch((err) => console.log(err));
    } else {
      toast.error("Enter all the Fields", {
        theme: "dark",
        autoClose: 1000,
      });
    }
  };

  return (
    <>
      <div className="Signup-Container">
        <div className="Signup-SubContainer">
          <ToastContainer />
          <div className="header">LogIn/SignIn</div>
          <div className="FormBody">
            <form className="formData">
              <TextField
                className="TextField"
                label="Username"
                variant="outlined"
                size="small"
                error={login.userFlag}
                value={login.username}
                onChange={(e) =>
                  setLoginDetails({ ...login, username: e.target.value })
                }
              />
              <TextField
                className="TextField"
                label="Password"
                type="password"
                variant="outlined"
                size="small"
                error={login.passFlag}
                value={login.password}
                onChange={(e) =>
                  setLoginDetails({ ...login, password: e.target.value })
                }
              />
              <RadioGroup
                className="roles"
                name="role"
                value={login.role}
                onChange={(e) =>
                  setLoginDetails({ ...login, role: e.target.value })
                }
              >
                <FormControlLabel
                  className="roleName"
                  value="Admin"
                  control={<Radio />}
                  label="Admin"
                />
                <FormControlLabel
                  className="roleName"
                  value="User"
                  control={<Radio />}
                  label="User"
                />
              </RadioGroup>
            </form>
          </div>
          <div className="buttons">
            <Button
              className="btn"
              color="primary"
              onClick={() => navigate("/")}
            >
              Create an account
            </Button>
            <Button
              className="btn"
              variant="contained"
              color="primary"
              onClick={handleSubmit}
            >
              LogIn
            </Button>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
