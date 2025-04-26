import React from "react";

export default function TableHeader({
  columns,
  sortColumn,
  sortDirection,
  onSort,
}) {
  const handleSort = (key) => {
    if (key === sortColumn) {
      onSort(key, sortDirection === "asc" ? "desc" : "asc"); 
    } else {
      onSort(key, "asc"); 
    }
  };

  return (
    <thead>
      <tr className="bg-gray-100 text-left">
        {columns.map((col) => (
          <th
            key={col.key}
            className="p-2 cursor-pointer select-none"
            onClick={() => handleSort(col.key)}
          >
            <div className="flex items-center gap-1">
              {col.label}
              {sortColumn === col.key ? (
                <span className="text-xs">{sortDirection == "asc" ? "▲" : "▼"}</span>
              ) : (
                <span className="text-xs text-gray-400">↕</span>
              )}
            </div>
          </th>
        ))}
      </tr>
    </thead>
  );
}
