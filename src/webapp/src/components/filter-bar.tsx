import React, { useState, useEffect } from "react";

export type FilterValue = string | [string, string];

export interface Filter {
    field: string;
    type: "text" | "number" | "date" | "datetime";
    operator: string;
    value: FilterValue;
}

export interface FilterType {
    field: string;
    label: string;
    type: Filter["type"];
}

export const FilterBar: React.FC<{
    filterTypes: FilterType[];
    onApply: (filters: Filter[]) => void;
}> = ({ filterTypes, onApply }) => {
    const [filters, setFilters] = useState<Filter[]>([]);
    const [selectedField, setSelectedField] = useState(filterTypes[0]?.field ?? "");
    const [operator, setOperator] = useState("contains");
    const [inputValue, setInputValue] = useState("");
    const [inputValue2, setInputValue2] = useState("");

    const currentFilterType = filterTypes.find((f) => f.field === selectedField);
    const textOperators = ["contains", "equals", "is not empty", "is empty"];
    const numberDateOperators = ["equals", "greater than", "lower than", "between", "is not empty", "is empty"];
    const operatorsForType = currentFilterType?.type === "text" ? textOperators : numberDateOperators;
    const showInput = operator !== "is not empty" && operator !== "is empty";

    useEffect(() => {
        if (currentFilterType?.type === "text") setOperator("contains");
        else setOperator("equals");
    }, [currentFilterType?.type, selectedField]);

    const handleAdd = () => {
        if (showInput && !inputValue) return;

        let value: FilterValue = inputValue;
        if (operator === "between") {
            if (!inputValue2) return;
            value = [inputValue, inputValue2];
        } else if (!showInput) {
            value = "";
        }

        const newFilters = [...filters, { field: selectedField, type: currentFilterType!.type, operator, value }];
        setFilters(newFilters);
        onApply(newFilters); // notify parent
        setInputValue("");
        setInputValue2("");
    };

    const handleRemove = (index: number) => {
        const newFilters = filters.filter((_, i) => i !== index);
        setFilters(newFilters);
        onApply(newFilters); // notify parent
    };

    return (
        <div className="w-full max-w-3xl mb-6">
            {/* Controls */}
            <div className="flex gap-2">
                <select
                    value={selectedField}
                    onChange={(e) => setSelectedField(e.target.value)}
                    className="px-3 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
                >
                    {filterTypes.map((ft) => (
                        <option key={ft.field} value={ft.field}>
                            {ft.label}
                        </option>
                    ))}
                </select>

                <select
                    value={operator}
                    onChange={(e) => setOperator(e.target.value)}
                    className="px-3 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
                >
                    {operatorsForType.map((op) => (
                        <option key={op} value={op}>
                            {op}
                        </option>
                    ))}
                </select>

                {showInput && (
                    <input
                        type={currentFilterType?.type === "datetime" ? "datetime-local" : currentFilterType?.type}
                        value={inputValue}
                        onChange={(e) => setInputValue(e.target.value)}
                        placeholder={`Value`}
                        className="px-3 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
                    />
                )}

                {operator === "between" && showInput && (
                    <input
                        type={currentFilterType?.type === "datetime" ? "datetime-local" : currentFilterType?.type}
                        value={inputValue2}
                        onChange={(e) => setInputValue2(e.target.value)}
                        placeholder={`and`}
                        className="px-3 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500"
                    />
                )}

                <button
                    onClick={handleAdd}
                    className="px-4 py-2 bg-blue-600 text-white rounded-lg shadow hover:bg-blue-700 transition"
                >
                    Add
                </button>
            </div>

            {/* Active Filters */}
            <div className="flex flex-wrap gap-2 mt-3">
                {filters.map((f, index) => (
                    <span
                        key={index}
                        className="flex items-center gap-2 px-3 py-1 bg-blue-100 text-blue-700 rounded-full text-sm"
                    >
                        {f.field} {f.operator} {Array.isArray(f.value) ? f.value.join(" and ") : f.value}
                        <button
                            onClick={() => handleRemove(index)}
                            className="text-blue-500 hover:text-blue-700"
                        >
                            ✕
                        </button>
                    </span>
                ))}
            </div>
        </div>
    );
};
