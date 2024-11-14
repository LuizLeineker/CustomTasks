import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from "./pages/Home";
import SignUp from "./pages/users/SignUp";
import SignIn from "./pages/users/SignIn";
import UserList from "./pages/users/UserList";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/signin" element={<SignIn />} />
          <Route path="/list" element={<UserList />} />
        </Routes>
      </div>
  </Router>
  );
}

export default App;