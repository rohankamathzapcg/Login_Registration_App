import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import Register from "./Components/Register";
import Login from "./Components/Login";
import HomePage from "./Components/HomePage";
import "react-toastify/dist/ReactToastify.css";

const App = () => {
  return (
    <>
      <Router>
        <Routes>
          <Route exact path="/" Component={Register} />
          <Route exact path="/login" Component={Login} />
          <Route exact path="/homepage" Component={HomePage} />
        </Routes>
      </Router>
    </>
  );
};

export default App;
