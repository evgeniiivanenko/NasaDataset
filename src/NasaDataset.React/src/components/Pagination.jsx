import React, { useEffect, useState } from "react";

export default function Pagination({
  pageNumber,
  totalPages,
  onPageChange,
  pageSize,
  onPageSizeChange,
}) {
  const [inputPage, setInputPage] = useState(pageNumber);
  useEffect(() => {
    setInputPage(pageNumber);
  }, [pageNumber]);

  const generatePagination = (pageNumber, totalPages) => {
    const pages = [];

    if (totalPages <= 7) {
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
    } else {
      pages.push(1);

      if (pageNumber > 3) {
        pages.push("...");
      }

      const startPage = Math.max(2, pageNumber - 1);
      const endPage = Math.min(totalPages - 1, pageNumber + 1);

      for (let i = startPage; i <= endPage; i++) {
        pages.push(i);
      }

      if (pageNumber < totalPages - 2) {
        pages.push("...");
      }

      pages.push(totalPages);
    }

    return pages;
  };

  return (
    <div className="flex flex-col md:flex-row justify-between items-center gap-4 mt-4">
      <div className="flex items-center gap-2 flex-wrap">
        <button
          onClick={() => onPageChange(pageNumber - 1)}
          disabled={pageNumber === 1}
          className="px-3 py-1 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
        >
          Назад
        </button>

        {generatePagination(pageNumber, totalPages).map((page, index) => (
          <button
            key={index}
            onClick={() => typeof page === "number" && onPageChange(page)}
            disabled={page === "..."}
            className={`px-3 py-1 rounded border 
              ${
                page === pageNumber
                  ? "bg-blue-500 text-white"
                  : "bg-white hover:bg-gray-100"
              }
              ${page === "..." ? "cursor-default" : ""}
            `}
          >
            {page}
          </button>
        ))}

        <button
          onClick={() => onPageChange(pageNumber + 1)}
          disabled={pageNumber === totalPages}
          className="px-3 py-1 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
        >
          Вперед
        </button>
      </div>

      <div className="flex items-center gap-2">
        <label className="text-sm">Страница:</label>
        <span className="text-sm">
          {inputPage} из {totalPages}
        </span>
      </div>

      <div className="flex items-center gap-2">
        <label className="text-sm">Показать по:</label>
        <select
          value={pageSize}
          onChange={(e) => onPageSizeChange(Number(e.target.value))}
          className="p-1 border rounded"
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={25}>25</option>
          <option value={50}>50</option>
          <option value={100}>100</option>
        </select>
      </div>
    </div>
  );
}
