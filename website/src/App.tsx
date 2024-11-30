import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from "./pages/Home";
import SignUp from "./pages/users/SignUp";
import SignIn from "./pages/users/SignIn";
import CreateTask from "./pages/tasks/CreateTask";
import SearchTask from "./pages/tasks/SearchTask";
import UpdateTask from "./pages/tasks/UpdateTask";
import ListLabel from "./pages/labels/ListLabel";
import CreateLabel from "./pages/labels/CreateLabel";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/signin" element={<SignIn />} />
          <Route path="/tasks/create" element={<CreateTask />} />
          <Route path="/tasks/list/:username" element={<SearchTask />} />
          <Route path="/tasks/update/:taskId" element={<UpdateTask />} />
          <Route path="/label/create" element={<CreateLabel />} />
          <Route path="/label/list/:username" element={<ListLabel />} />
        </Routes>
      </div>
  </Router>
  );
}

export default App;