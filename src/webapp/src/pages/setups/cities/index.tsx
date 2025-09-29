import React, { useState, useEffect } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "../../../components/card";
import { Button } from "../../../components/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../../components/table";

interface City {
    id: number;
    name: string;
    state: string;
}

const mockCities: City[] = [
    { id: 1, name: "New York", state: "New York" },
    { id: 2, name: "Los Angeles", state: "California" },
];

const CitiesPage: React.FC = () => {
    const [cities, setCities] = useState<City[]>([]);

    useEffect(() => {
        setCities(mockCities);
    }, []);

    const handleCreate = () => {
        alert("Create City (mock)");
    };

    const handleEdit = (id: number) => {
        alert(`Edit City ${id} (mock)`);
    };

    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Cities</CardTitle>
                </CardHeader>
                <CardContent>
                    <Button
                        className="mb-4 bg-blue-600"
                        onClick={handleCreate}
                    >
                        Create City
                    </Button>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Name</TableHead>
                                <TableHead>State</TableHead>
                                <TableHead>Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {cities.map((city) => (
                                <TableRow key={city.id}>
                                    <TableCell>{city.id}</TableCell>
                                    <TableCell>{city.name}</TableCell>
                                    <TableCell>{city.state}</TableCell>
                                    <TableCell>
                                        <Button
                                            className="bg-yellow-500"
                                            onClick={() => handleEdit(city.id)}
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

export default CitiesPage;
