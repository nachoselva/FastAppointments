import React, { useState, useEffect, useMemo } from "react";
import {
    Card,
    CardContent,
    CardHeader,
    CardTitle
} from "../../../components/card";
import { Button } from "../../../components/button";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow
} from "../../../components/table";
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

    const handleCreate = () => {
        alert("Create Location (mock)");
    };

    const handleEdit = (id: number) => {
        alert(`Edit Location ${id} (mock)`);
    };

    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Locations</CardTitle>
                </CardHeader>
                <CardContent>
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

                    <Button
                        className="mb-4 bg-blue-600"
                        onClick={handleCreate}
                    >
                        Create Location
                    </Button>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Establishment</TableHead>
                                <TableHead>Address</TableHead>
                                <TableHead>City</TableHead>
                                <TableHead>Room</TableHead>
                                <TableHead>Created At</TableHead>
                                <TableHead>Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {filteredLocations.map((location) => (
                                <TableRow key={location.id}>
                                    <TableCell>{location.id}</TableCell>
                                    <TableCell>
                                        {location.building
                                            ? `${location.name} (${location.building.name})`
                                            : location.name}
                                    </TableCell>
                                    <TableCell>
                                        {location.building
                                            ? `${location.building.address.streetName} ${location.building.address.streetNumber}`
                                            : "N/A"}
                                    </TableCell>
                                    <TableCell>
                                        {location.building
                                            ? `${location.building.address.city.name}`
                                            : "N/A"}
                                    </TableCell>
                                    <TableCell>{`${location.floor ?? ""} ${location.room ?? ""}`}</TableCell>
                                    <TableCell>{location.createdAt ?? "N/A"}</TableCell>
                                    <TableCell>
                                        <Button
                                            className="bg-yellow-500"
                                            onClick={() => handleEdit(location.id)}
                                        >
                                            Edit
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </CardContent>
            </Card>
        </div>
    );
};

export default LocationsPage;
