import React, { useEffect, useState } from "react";
import DropdownFilter from "../../components/filters/DropdownFilter";
import Table from "../../components/table/Table";
import PageSwitcher from "../../components/PageSwitcher";
import Pagination from "../../components/Pagination";

export default function GroupTablePage() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [groupKey, setGroupKey] = useState("year");
  const [sortColumn, setSortColumn] = useState("key");
  const [sortDirection, setSortDirection] = useState("asc");

  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const [filters, setFilters] = useState({
    years: [],
  });

  const [yearFrom, setYearFrom] = useState("");
  const [yearTo, setYearTo] = useState("");

  const fetchData = async () => {
    try {
      const params = new URLSearchParams({
        Key: groupKey,
        SortBy: sortColumn,
        Direction: sortDirection,
      });

      if (yearFrom) params.append("YearFrom", yearFrom);
      if (yearTo) params.append("YearTo", yearTo);
      if (pageNumber) params.append("PageNumber", pageNumber);

      const response = await fetch(
        `https://localhost:7113/api/meteorites/groups?${params.toString()}`
      );
      if (!response.ok) throw new Error("Ошибка при загрузке данных");

      const json = await response.json();
      setData(json.items || []);
      setTotalPages(json.totalPages || 1);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [groupKey, sortColumn, sortDirection, pageNumber]);

  const resetFilters = () => {
    setSortColumn("key");
    setSortDirection("asc");
    setYearFrom("");
    setYearTo("");
    setPageNumber(1);
  };

  useEffect(() => {
    fetch("https://localhost:7113/api/meteorites/filters")
      .then((res) => res.json())
      .then(setFilters)
      .catch(console.error);
  }, []);

  const columns = [
    { label: groupKey === "year" ? "Год" : "Класс", key: "key" },
    { label: "Количество метеоритов", key: "count" },
    { label: "Суммарная масса", key: "totalMass" },
  ];

  return (
    <div className="p-4 space-y-4">
      <PageSwitcher />

      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
        <DropdownFilter
          label="Год от"
          options={filters.years}
          value={yearFrom}
          onChange={setYearFrom}
        />
        <DropdownFilter
          label="Год до"
          options={filters.years}
          value={yearTo}
          onChange={setYearTo}
        />

        <button
          onClick={resetFilters}
          className="h-10 mt-auto p-2 bg-gray-200 text-sm rounded hover:bg-gray-300 transition"
        >
          Сбросить фильтры
        </button>
      </div>

      <Table
        columns={columns}
        data={data}
        keyFieldName="key"
        loading={loading}
        error={error}
        sortColumn={sortColumn}
        sortDirection={sortDirection}
        onSort={(col) =>
          setSortColumn((prev) => {
            const newDir =
              sortColumn === col && sortDirection === "asc" ? "desc" : "asc";
            setSortDirection(newDir);
            return col;
          })
        }
      />
      <Pagination
        pageNumber={pageNumber}
        totalPages={totalPages}
        onPageChange={(newPage) => setPageNumber(newPage)}
      />
    </div>
  );
}
