import React, { useEffect } from 'react';

function App() {
  useEffect(() => {
    fetch('http://localhost:5182/api/user/list')
    .then(response => response.json)
    .then(usersList => console.log(usersList))
  });
  return (
    <div className="App">
      <p>CustomTasks Website</p>
    </div>
  );
}

export default App;