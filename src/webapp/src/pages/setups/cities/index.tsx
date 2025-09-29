import React, { useState, useEffect } from "react";

interface City {
    id: number;
    name: string;
    addresses: Address[];
}
interface Address {
    id: number;
    streetName: string;
    streetNumber: string;
}

const mockCities: City[] = [
    {
        id: 1,
        name: "La Plata",
        addresses: [
            { id: 1, streetName: "Calle 1", streetNumber: "100" },
            { id: 2, streetName: "Calle 2", streetNumber: "200" }
        ]
    },
    {
        id: 2,
        name: "Cordoba",
        addresses: [
            { id: 3, streetName: "Av. Colon", streetNumber: "300" }
        ]
    }
];

const CitiesPage: React.FC = () => {
    const [cities, setCities] = useState<City[]>([]);

    useEffect(() => {
        setCities(mockCities);
    }, []);

    const handleCreateCity = () => {
        alert("Create City (mock)");
    };
    const handleEditCity = (id: number) => {
        alert(`Edit City ${id} (mock)`);
    };
    const handleCreateAddress = (cityId: number) => {
        alert(`Create Address for City ${cityId} (mock)`);
    };
    const handleEditAddress = (addressId: number) => {
        alert(`Edit Address ${addressId} (mock)`);
    };

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Cities & Addresses</h1>
            <button className="mb-4 px-4 py-2 bg-blue-600 text-white rounded" onClick={handleCreateCity}>
                Create City
            </button>
            <table className="min-w-full border mb-8">
                <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">Name</th>
                        <th className="border px-4 py-2">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {cities.map((city) => (
                        <React.Fragment key={city.id}>
                            <tr>
                                <td className="border px-4 py-2">{city.id}</td>
                                <td className="border px-4 py-2">{city.name}</td>
                                <td className="border px-4 py-2">
                                    <button className="px-2 py-1 bg-yellow-500 text-white rounded mr-2" onClick={() => handleEditCity(city.id)}>
                                        Edit
                                    </button>
                                    <button className="px-2 py-1 bg-green-500 text-white rounded" onClick={() => handleCreateAddress(city.id)}>
                                        Add Address
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td colSpan={3} className="border px-4 py-2 bg-gray-50">
                                    <div className="ml-4">
                                        <h2 className="text-lg font-semibold mb-2">Addresses</h2>
                                        <table className="min-w-full border">
                                            <thead>
                                                <tr>
                                                    <th className="border px-4 py-2">ID</th>
                                                    <th className="border px-4 py-2">Street Name</th>
                                                    <th className="border px-4 py-2">Street Number</th>
                                                    <th className="border px-4 py-2">Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {city.addresses.map((address) => (
                                                    <tr key={address.id}>
                                                        <td className="border px-4 py-2">{address.id}</td>
                                                        <td className="border px-4 py-2">{address.streetName}</td>
                                                        <td className="border px-4 py-2">{address.streetNumber}</td>
                                                        <td className="border px-4 py-2">
                                                            <button className="px-2 py-1 bg-yellow-500 text-white rounded" onClick={() => handleEditAddress(address.id)}>
                                                                Edit
                                                            </button>
                                                        </td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </React.Fragment>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default CitiesPage;
