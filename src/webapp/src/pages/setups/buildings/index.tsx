import React, { useState, useEffect } from "react";

interface Building {
    id: number;
    name: string;
    locations: Location[];
}
interface Location {
    id: number;
    name: string;
    floor?: string;
    room?: string;
}

const mockBuildings: Building[] = [
    {
        id: 1,
        name: "Edificio A",
        locations: [
            { id: 1, name: "Recepcion", floor: "PB", room: "1" },
            { id: 2, name: "Consultorio", floor: "1", room: "2" }
        ]
    },
    {
        id: 2,
        name: "Edificio B",
        locations: [
            { id: 3, name: "Sala de Espera", floor: "PB", room: "3" }
        ]
    }
];

const BuildingsPage: React.FC = () => {
    const [buildings, setBuildings] = useState<Building[]>([]);

    useEffect(() => {
        setBuildings(mockBuildings);
    }, [index]);

    const handleCreateBuilding = () => {
        alert("Create Building (mock)");
    };
    const handleEditBuilding = (id: number) => {
        alert(`Edit Building ${id} (mock)`);
    };
    const handleCreateLocation = (buildingId: number) => {
        alert(`Create Location for Building ${buildingId} (mock)`);
    };
    const handleEditLocation = (locationId: number) => {
        alert(`Edit Location ${locationId} (mock)`);
    };

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Buildings & Locations</h1>
            <button className="mb-4 px-4 py-2 bg-blue-600 text-white rounded" onClick={handleCreateBuilding}>
                Create Building
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
                    {buildings.map((building) => (
                        <React.Fragment key={building.id}>
                            <tr>
                                <td className="border px-4 py-2">{building.id}</td>
                                <td className="border px-4 py-2">{building.name}</td>
                                <td className="border px-4 py-2">
                                    <button className="px-2 py-1 bg-yellow-500 text-white rounded mr-2" onClick={() => handleEditBuilding(building.id)}>
                                        Edit
                                    </button>
                                    <button className="px-2 py-1 bg-green-500 text-white rounded" onClick={() => handleCreateLocation(building.id)}>
                                        Add Location
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td colSpan={3} className="border px-4 py-2 bg-gray-50">
                                    <div className="ml-4">
                                        <h2 className="text-lg font-semibold mb-2">Locations</h2>
                                        <table className="min-w-full border">
                                            <thead>
                                                <tr>
                                                    <th className="border px-4 py-2">ID</th>
                                                    <th className="border px-4 py-2">Name</th>
                                                    <th className="border px-4 py-2">Floor</th>
                                                    <th className="border px-4 py-2">Room</th>
                                                    <th className="border px-4 py-2">Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {building.locations.map((location) => (
                                                    <tr key={location.id}>
                                                        <td className="border px-4 py-2">{location.id}</td>
                                                        <td className="border px-4 py-2">{location.name}</td>
                                                        <td className="border px-4 py-2">{location.floor}</td>
                                                        <td className="border px-4 py-2">{location.room}</td>
                                                        <td className="border px-4 py-2">
                                                            <button className="px-2 py-1 bg-yellow-500 text-white rounded" onClick={() => handleEditLocation(location.id)}>
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

export default BuildingsPage;
