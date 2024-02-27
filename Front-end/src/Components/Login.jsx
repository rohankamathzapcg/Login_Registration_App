import TextField from "@material-ui/core/TextField";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";

const Login = () => {
  return (
    <>
      <div className="Signup-Container">
        <div className="Signup-SubContainer">
          <div className="header">LogIn/SignIn</div>
          <div className="FormBody">
            <form className="formData">
              <TextField
                className="TextField"
                label="Username"
                variant="outlined"
                size="small"
              />
              <TextField
                className="TextField"
                label="Password"
                variant="outlined"
                size="small"
              />
              <RadioGroup
                className="roles"
                name="role"
                // value={value}
                // onChange={handleChange}
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
              Create an account
            </Button>
            <Button className="btn" variant="contained" color="primary">
              LogIn
            </Button>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
