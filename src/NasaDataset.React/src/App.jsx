import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import MeteoriteTablePage from "./features/meteorites/MeteoriteTablePage";
import GroupTablePage from "./features/groups/GroupTablePage";
function App() {
  return (
    <Router>
      <div className="p-4 space-y-4">
        <Routes>
          <Route path="/" element={<MeteoriteTablePage />} />
          <Route path="/group" element={<GroupTablePage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
