import React, { useState, useEffect } from "react";

interface Address {
    id: number;
    streetName: string;
    streetNumber: string;
    buildings: Building[];
}
interface Building {
    id: number;
    name: string;
}

const mockAddresses: Address[] = [
    {
        id: 1,
        streetName: "Calle 1",
        streetNumber: "100",
        buildings: [
            { id: 1, name: "Edificio A" },
            { id: 2, name: "Edificio B" }
        ]
    },
    {
        id: 2,
        streetName: "Av. Colon",
        streetNumber: "300",
        buildings: [
            { id: 3, name: "Edificio C" }
        ]
    }
];

const AddressesPage: React.FC = () => {
    const [addresses, setAddresses] = useState<Address[]>([]);

    useEffect(() => {
        setAddresses(mockAddresses);
    }, []);

    const handleCreateAddress = () => {
        alert("Create Address (mock)");
    };
    const handleEditAddress = (id: number) => {
        alert(`Edit Address ${id} (mock)`);
    };
    const handleCreateBuilding = (addressId: number) => {
        alert(`Create Building for Address ${addressId} (mock)`);
    };
    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Addresses & Buildings</h1>
            <button className="mb-4 px-4 py-2 bg-blue-600 text-white rounded" onClick={handleCreateAddress}>
                Create Address
            </button>
            <table className="min-w-full border mb-8">
                <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">Street Name</th>
                        <th className="border px-4 py-2">Street Number</th>
                        <th className="border px-4 py-2">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {addresses.map((address) => (
                        <React.Fragment key={address.id}>
                            <tr>
                                <td className="border px-4 py-2">{address.id}</td>
                                <td className="border px-4 py-2">{address.streetName}</td>
                                <td className="border px-4 py-2">{address.streetNumber}</td>
                                <td className="border px-4 py-2">
                                    <button className="px-2 py-1 bg-yellow-500 text-white rounded mr-2" onClick={() => handleEditAddress(address.id)}>
                                        Edit
                                    </button>
                                    <button className="px-2 py-1 bg-green-500 text-white rounded" onClick={() => handleCreateBuilding(address.id)}>
                                        Add Building
                                    </button>
                                </td>
                            </tr>
                        </React.Fragment>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AddressesPage;
