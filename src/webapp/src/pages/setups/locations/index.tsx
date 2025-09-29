import React, { useState, useEffect, useMemo } from "react";
import { FilterBar, type Filter } from "../../../components/filter-bar";

interface Location {
    id: number;
    name: string;
    floor?: string;
    room?: string;
    building?: Building;
    createdAt?: string;
}

interface Address {
    id: number;
    streetName: string;
    streetNumber: string;
    city: City;
}

interface Building {
    id: number;
    name: string;
    floor?: string;
    room?: string;
    address: Address;
}

interface City {
    id: number;
    name: string;
    state: State;
}

interface State {
    id: number;
    name: string;
    code: string;
    country: Country;
}

interface Country {
    id: number;
    name: string;
    code: string;
}

const mockLocations: Location[] = [
    {
        id: 1,
        name: "Recepcion",
        floor: "PB",
        room: "1",
        createdAt: "2025-09-01T10:30:00",
        building: {
            id: 1,
            name: "Edificio Central",
            floor: "PB",
            room: "1",
            address: {
                id: 1,
                streetName: "Av. Siempre Viva",
                streetNumber: "742",
                city: {
                    id: 1,
                    name: "Springfield",
                    state: {
                        id: 1,
                        name: "Illinois",
                        code: "IL",
                        country: { id: 1, name: "United States", code: "US" }
                    }
                }
            }
        }
    },
    {
        id: 2,
        name: "Consultorio",
        floor: "1",
        room: "2",
        createdAt: "2025-09-02T09:00:00",
        building: {
            id: 1,
            name: "Edificio Central",
            floor: "1",
            room: "2",
            address: {
                id: 1,
                streetName: "Av. Siempre Viva",
                streetNumber: "742",
                city: {
                    id: 1,
                    name: "Springfield",
                    state: {
                        id: 1,
                        name: "Illinois",
                        code: "IL",
                        country: { id: 1, name: "United States", code: "US" }
                    }
                }
            }
        }
    },
    {
        id: 3,
        name: "Sala de Espera",
        floor: "PB",
        room: "3",
        createdAt: "2025-09-03T14:15:00",
        building: {
            id: 1,
            name: "Edificio Central",
            floor: "PB",
            room: "3",
            address: {
                id: 1,
                streetName: "Av. Siempre Viva",
                streetNumber: "742",
                city: {
                    id: 1,
                    name: "Springfield",
                    state: {
                        id: 1,
                        name: "Illinois",
                        code: "IL",
                        country: { id: 1, name: "United States", code: "US" }
                    }
                }
            }
        }
    },
    { id: 4, name: "Remoto", createdAt: "2025-09-05T12:00:00" }
];

const LocationsPage: React.FC = () => {
    const [locations, setLocations] = useState<Location[]>([]);
    const [filters, setFilters] = useState<Filter[]>([]);

    useEffect(() => {
        setLocations(mockLocations);
    }, []);

    const filteredLocations = useMemo(() => {
        return locations.filter((l) =>
            filters.every((f) => {
                const val = Array.isArray(f.value) ? f.value : [f.value];

                const target =
                    f.field === "building"
                        ? l.building?.name ?? ""
                        : f.field === "address"
                            ? l.building
                                ? `${l.building.address.streetName} ${l.building.address.streetNumber}`
                                : ""
                            : f.field === "city"
                                ? l.building?.address.city.name ?? ""
                                : f.field === "state"
                                    ? l.building?.address.city.state.name ?? ""
                                    : f.field === "country"
                                        ? l.building?.address.city.state.country.name ?? ""
                                        : f.field === "id"
                                            ? String(l.id)
                                            : f.field === "createdAt"
                                                ? l.createdAt ?? ""
                                                : "";

                const targetNum = Number(target);
                const valNum = Number(val[0]);

                if (f.type === "text") {
                    return target.toLowerCase().includes(val[0].toLowerCase());
                }

                if (f.type === "number" || f.type === "date" || f.type === "datetime") {
                    switch (f.operator) {
                        case "equals":
                            return target === val[0] || targetNum === valNum;
                        case "greater than":
                            return targetNum > valNum;
                        case "lower than":
                            return targetNum < valNum;
                        case "between": {
                            const valNum2 = Number(val[1]);
                            return targetNum >= valNum && targetNum <= valNum2;
                        }
                        default:
                            return true;
                    }
                }

                return true;
            })
        );
    }, [locations, filters]);

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-6">Locations</h1>

            <FilterBar
                filterTypes={[
                    { field: "building", label: "Building", type: "text" },
                    { field: "address", label: "Address", type: "text" },
                    { field: "city", label: "City", type: "text" },
                    { field: "state", label: "State", type: "text" },
                    { field: "country", label: "Country", type: "text" },
                    { field: "id", label: "ID", type: "number" },
                    { field: "createdAt", label: "Created At", type: "datetime" }
                ]}
                onApply={setFilters} // ← now handled internally
            />

            <button
                className="mb-4 px-4 py-2 bg-blue-600 text-white rounded-lg shadow hover:bg-blue-700 transition"
                onClick={() => alert("Create Location (mock)")}
            >
                Create Location
            </button>

            <table className="min-w-full border border-gray-300 shadow-sm">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="border px-4 py-2 text-left">ID</th>
                        <th className="border px-4 py-2 text-left">Establishment</th>
                        <th className="border px-4 py-2 text-left">Address</th>
                        <th className="border px-4 py-2 text-left">City</th>
                        <th className="border px-4 py-2 text-left">Room</th>
                        <th className="border px-4 py-2 text-left">Created At</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredLocations.map((location) => (
                        <tr key={location.id} className="hover:bg-gray-50">
                            <td className="border px-4 py-2">{location.id}</td>
                            <td className="border px-4 py-2">
                                {location.building
                                    ? `${location.name} (${location.building.name})`
                                    : location.name}
                            </td>
                            <td className="border px-4 py-2">
                                {location.building
                                    ? `${location.building.address.streetName} ${location.building.address.streetNumber}`
                                    : "N/A"}
                            </td>
                            <td className="border px-4 py-2">
                                {location.building
                                    ? `${location.building.address.city.name}, ${location.building.address.city.state.name}, ${location.building.address.city.state.country.name}`
                                    : "N/A"}
                            </td>
                            <td className="border px-4 py-2">{`${location.floor ?? ""} ${location.room ?? ""}`}</td>
                            <td className="border px-4 py-2">{location.createdAt ?? "N/A"}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default LocationsPage;
