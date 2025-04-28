import React, { useEffect, useState } from "react";
import DropdownFilter from "../../components/filters/DropdownFilter";
import Table from "../../components/table/Table";
import PageSwitcher from "../../components/PageSwitcher";
import Pagination from "../../components/Pagination";
import { API_ENDPOINTS } from "../../api/endpoints";
export default function MeteoriteTablePage() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [filters, setFilters] = useState({
    years: [],
    recclasses: [],
  });

  const [yearFrom, setYearFrom] = useState("");
  const [yearTo, setYearTo] = useState("");
  const [recclass, setRecclass] = useState("");
  const [searchQuery, setSearchQuery] = useState("");

  const [sortColumn, setSortColumn] = useState("id");
  const [sortDirection, setSortDirection] = useState("asc");

  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(1);

  const fetchData = async () => {
    try {
      const params = new URLSearchParams({
        SortBy: sortColumn,
        Direction: sortDirection,
      });

      if (yearFrom) params.append("YearFrom", yearFrom);
      if (yearTo) params.append("YearTo", yearTo);
      if (recclass) params.append("Recclass", recclass);
      if (searchQuery) params.append("SearchTerm", searchQuery);
      if (pageNumber) params.append("PageNumber", pageNumber);
      if (pageSize) params.append("PageSize", pageSize);

      const response = await fetch(
        `${API_ENDPOINTS.METEORITES.GET_WITH_PAGINATION}?${params}`
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

  const resetFilters = () => {
    setYearFrom("");
    setYearTo("");
    setRecclass("");
    setSearchQuery("");
    setSortColumn("id");
    setSortDirection("asc");
    setPageNumber(1);
  };

  useEffect(() => {
    fetch(API_ENDPOINTS.METEORITES.FILTERS)
      .then((res) => res.json())
      .then(setFilters)
      .catch(console.error);
  }, []);

  useEffect(() => {
    fetchData();
  }, [
    sortColumn,
    sortDirection,
    yearFrom,
    yearTo,
    recclass,
    searchQuery,
    pageNumber,
    pageSize,
  ]);

  const columns = [
    { label: "id", key: "id" },
    { label: "Название", key: "name" },
    { label: "Тип", key: "nametype" },
    { label: "Класс", key: "recclass" },
    { label: "Масса", key: "mass" },
    { label: "Падение", key: "fall" },
    { label: "Год", key: "year" },
    { label: "Широта", key: "reclat" },
    { label: "Долгота", key: "reclong" },
    { label: "Координаты", key: "geolocation" },
    { label: "Вычисляемая область", key: "region" },
  ];

  if (loading) return <div className="p-4">Загрузка...</div>;
  if (error) return <div className="p-4 text-red-500">Ошибка: {error}</div>;

  const formatData = data.map((item) => ({
    id: item.externalId,
    name: item.name,
    nametype: item.nametype,
    recclass: item.recclass,
    mass: item.mass,
    fall: item.fall,
    year: item.year,
    reclat: item.reclat,
    reclong: item.reclong,
    geolocation: JSON.stringify(item.geolocation),
    region: [item.computedRegionCbhkFwbd, item.computedRegionNnqa25f4]
      .filter(Boolean)
      .join(", "),
  }));

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
        <DropdownFilter
          label="Класс"
          options={filters.recclasses}
          value={recclass}
          onChange={setRecclass}
        />
        <div className="flex flex-col">
          <label className="mb-1 text-sm font-medium text-gray-700">
            Поиск
          </label>
          <input
            type="text"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            placeholder="Введите название"
            className="p-2 border rounded"
          />
        </div>
        <button
          onClick={resetFilters}
          className="h-10 mt-auto p-2 bg-gray-200 text-sm rounded hover:bg-gray-300 transition"
        >
          Сбросить фильтры
        </button>
      </div>

      <Table
        columns={columns}
        data={formatData}
        keyFieldName="id"
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
        pageSize={pageSize}
        onPageSizeChange={(newPageSize) => {
          setPageSize(newPageSize);
          setPageNumber(1);
        }}
      />
    </div>
  );
}
