import React from "react";
import { useNavigate, useLocation } from "react-router-dom";

export default function PageSwitcher() {
  const navigate = useNavigate();
  const location = useLocation();

  const isGroupPage = location.pathname.includes("group");

  return (
    <div className="flex justify-end mb-4">
      <button
        onClick={() => navigate("/")}
              className={`px-4 py-2 bg-blue-500 text-white rounded transition ${!isGroupPage ? "opacity-50 cursor-not-allowed" : "hover:bg-blue-600"}`}
        disabled={!isGroupPage}
      >
        Все метеориты
      </button>
      <button
        onClick={() => navigate("/group")}
              className={`px-4 py-2 bg-blue-500 text-white rounded transition ${!isGroupPage ? "hover:bg-blue-600" : "opacity-50 cursor-not-allowed"}`}
        disabled={isGroupPage}
      >
        Агрегация по годам
      </button>
    </div>
  );
}
