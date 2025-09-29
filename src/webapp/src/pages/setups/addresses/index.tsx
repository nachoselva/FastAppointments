import React, { useState, useEffect } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "../../../components/card";
import { Button } from "../../../components/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../../components/table";

interface Address {
    id: number;
    street: string;
    city: string;
    state: string;
    zip: string;
}

const mockAddresses: Address[] = [
    { id: 1, street: "123 Main St", city: "New York", state: "NY", zip: "10001" },
    { id: 2, street: "456 Oak Ave", city: "Los Angeles", state: "CA", zip: "90001" },
];

const AddressesPage: React.FC = () => {
    const [addresses, setAddresses] = useState<Address[]>([]);

    useEffect(() => {
        setAddresses(mockAddresses);
    }, []);

    const handleCreate = () => {
        alert("Create Address (mock)");
    };

    const handleEdit = (id: number) => {
        alert(`Edit Address ${id} (mock)`);
    };

    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Addresses</CardTitle>
                </CardHeader>
                <CardContent>
                    <Button
                        className="mb-4 bg-blue-600"
                        onClick={handleCreate}
                    >
                        Create Address
                    </Button>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Street</TableHead>
                                <TableHead>City</TableHead>
                                <TableHead>State</TableHead>
                                <TableHead>Zip</TableHead>
                                <TableHead>Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {addresses.map((address) => (
                                <TableRow key={address.id}>
                                    <TableCell>{address.id}</TableCell>
                                    <TableCell>{address.street}</TableCell>
                                    <TableCell>{address.city}</TableCell>
                                    <TableCell>{address.state}</TableCell>
                                    <TableCell>{address.zip}</TableCell>
                                    <TableCell>
                                        <Button
                                            className="bg-yellow-500"
                                            onClick={() => handleEdit(address.id)}
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

export default AddressesPage;
