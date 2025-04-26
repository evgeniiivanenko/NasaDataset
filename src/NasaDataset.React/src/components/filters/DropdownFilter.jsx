import React from "react";

export default function DropdownFilter({ label, options, value, onChange }) {
    return (
        <div className="flex flex-col">
            <label className="mb-1 text-sm font-medium text-gray-700">{label}</label>
            <select
                value={value}
                onChange={(e) => onChange(e.target.value)}
                className="border border-gray-300 rounded-md p-2 text-sm"
            >
                <option value="">Все</option>
                {options.map((opt) => (
                    <option key={opt} value={opt}>
                        {opt}
                    </option>
                ))}
            </select>
        </div>
    );
}
