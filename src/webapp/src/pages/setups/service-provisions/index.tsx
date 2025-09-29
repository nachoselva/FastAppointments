import React, { useState, useEffect } from "react";

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
            <h1 className="text-2xl font-bold mb-4">Service Provisions</h1>
            <button
                className="mb-4 px-4 py-2 bg-blue-600 text-white rounded"
                onClick={handleCreate}
            >
                Create Service Provision
            </button>
            <table className="min-w-full border">
                <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">Name</th>
                        <th className="border px-4 py-2">Description</th>
                        <th className="border px-4 py-2">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {serviceProvisions.map((sp) => (
                        <tr key={sp.id}>
                            <td className="border px-4 py-2">{sp.id}</td>
                            <td className="border px-4 py-2">{sp.name}</td>
                            <td className="border px-4 py-2">{sp.description}</td>
                            <td className="border px-4 py-2">
                                <button
                                    className="px-2 py-1 bg-yellow-500 text-white rounded"
                                    onClick={() => handleEdit(sp.id)}
                                >
                                    Edit
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ServiceProvisionsPage;
