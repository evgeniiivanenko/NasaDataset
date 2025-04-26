import React from "react";
import TableHeader from "./TableHeader";
import TableRow from "./TableRow";

export default function Table({ columns, data, keyFieldName, loading, error, sortColumn, sortDirection, onSort, renderRow }) {
    if (loading) return <div className="p-4">Загрузка...</div>;
    if (error) return <div className="p-4 text-red-500">Ошибка: {error}</div>;
    if (!data || data.length === 0)
        return <div className="p-4 text-gray-500">Нет данных</div>;

    return (
        <table className="w-full table-auto border-collapse">
            <TableHeader
                columns={columns}
                sortColumn={sortColumn}
                sortDirection={sortDirection}
                onSort={onSort}
            />
            <tbody>
                {data.map((item) =>
                    <TableRow
                        key={item[keyFieldName] || JSON.stringify(item)}
                        item={item}
                        columns={columns}
                    />
                )}
            </tbody>
        </table>
    );
}
