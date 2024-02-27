import TextField from "@material-ui/core/TextField";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";
import { useState } from "react";
import axios from "axios";

const Register = () => {
  const [registration, setRegistration] = useState({
    username: "",
    pass: "",
    cpass: "",
    userFlag: false,
    passFlag: false,
    cpassFlag: false,
    roleValue: "User",
  });

  const checkValidity = () => {
    let newUserFlag = false;
    let newPassFlag = false;
    let newCpassFlag = false;

    if (registration.username === "") {
      newUserFlag = true;
    }
    if (registration.pass === "") {
      newPassFlag = true;
    }
    if (registration.cpass === "") {
      newCpassFlag = true;
    }

    setRegistration((prevState) => ({
      ...prevState,
      userFlag: newUserFlag,
      passFlag: newPassFlag,
      cpassFlag: newCpassFlag,
    }));
  };
  const handleSubmit = () => {
    checkValidity();
    if (
      registration.username !== "" &&
      registration.pass !== "" &&
      registration.cpass !== ""
    ) {
      axios
        .post("https://localhost:44376/api/Auth/Registration", registration)
        .then((result) => console.log(result))
        .catch((err) => console.log(err));
    } else {
      console.log("Not Acceptable");
    }
  };
  return (
    <>
      <div className="Signup-Container">
        <div className="Signup-SubContainer">
          <div className="header">Register/Signup</div>
          <div className="FormBody">
            <form className="formData">
              <TextField
                className="TextField"
                label="Username"
                variant="outlined"
                size="small"
                error={registration.userFlag}
                value={registration.username}
                onChange={(e) =>
                  setRegistration({ ...registration, username: e.target.value })
                }
              />
              <TextField
                className="TextField"
                label="Password"
                variant="outlined"
                size="small"
                type="password"
                error={registration.passFlag}
                value={registration.pass}
                onChange={(e) =>
                  setRegistration({ ...registration, pass: e.target.value })
                }
              />
              <TextField
                className="TextField"
                label="Confirm Password"
                variant="outlined"
                size="small"
                type="password"
                error={registration.cpassFlag}
                value={registration.cpass}
                onChange={(e) =>
                  setRegistration({ ...registration, cpass: e.target.value })
                }
              />
              <RadioGroup
                className="roles"
                name="role"
                value={registration.roleValue}
                onChange={(e) =>
                  setRegistration({
                    ...registration,
                    roleValue: e.target.value,
                  })
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
            <Button className="btn" color="primary">
              Login
            </Button>
            <Button
              className="btn"
              variant="contained"
              color="primary"
              onClick={handleSubmit}
            >
              Register
            </Button>
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;
