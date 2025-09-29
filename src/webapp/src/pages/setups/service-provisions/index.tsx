import React, { useState, useEffect } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "../../../components/card";
import { Button } from "../../../components/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../../components/table";

interface ServiceProvision {
    id: number;
    name: string;
    description: string;
}

const mockServiceProvisions: ServiceProvision[] = [
    { id: 1, name: "Consultation", description: "General consultation" },
    { id: 2, name: "Therapy", description: "Physical therapy session" },
];

const ServiceProvisionsPage: React.FC = () => {
    const [serviceProvisions, setServiceProvisions] = useState<ServiceProvision[]>([]);

    useEffect(() => {
        setServiceProvisions(mockServiceProvisions);
    }, []);

    const handleCreate = () => {
        alert("Create Service Provision (mock)");
    };

    const handleEdit = (id: number) => {
        alert(`Edit Service Provision ${id} (mock)`);
    };

    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Service Provisions</CardTitle>
                </CardHeader>
                <CardContent>
                    <Button
                        className="mb-4 bg-blue-600"
                        onClick={handleCreate}
                    >
                        Create Service Provision
                    </Button>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Name</TableHead>
                                <TableHead>Description</TableHead>
                                <TableHead>Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {serviceProvisions.map((sp) => (
                                <TableRow key={sp.id}>
                                    <TableCell>{sp.id}</TableCell>
                                    <TableCell>{sp.name}</TableCell>
                                    <TableCell>{sp.description}</TableCell>
                                    <TableCell>
                                        <Button
                                            className="bg-yellow-500"
                                            onClick={() => handleEdit(sp.id)}
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

export default ServiceProvisionsPage;
