export default function TableRow({ item, keyFieldName = "id", columns = [] }) {
    return (
        <tr className="border-b">
            {columns.map((col) => (
                <td key={col.key} className="p-2">
                    {typeof col.render === "function"
                        ? col.render(item[col.key], item)
                        : item[col.key]}
                </td>
            ))}
        </tr>
    );
}
