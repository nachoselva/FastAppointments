import React, { useEffect, useState } from "react";
import { Link } from 'react-router-dom';

interface State {
    id: number;
    name: string;
    code: string;
    cities: number;
    enabled: boolean;
}

const mockStates: State[] = [
    {
        id: 1,
        name: "Buenos Aires",
        code: "BA",
        cities : 10,
        enabled : true
    },
    {
        id: 2,
        name: "Cordoba",
        code: "CB",
        cities: 10,
        enabled : false
    }
];

const StatesPage: React.FC = () => {
    const [states, setStates] = useState<State[]>([]);

    useEffect(() => {
        setStates(mockStates);
    }, [mockStates]);

    const toggleEnabled = (id: number) => {
        setStates((prev) =>
            prev.map((c) =>
                c.id === id ? { ...c, enabled: !c.enabled } : c
            )
        );
    };

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">States</h1>
            <table className="min-w-full border mb-8">
                <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">Name</th>
                        <th className="border px-4 py-2">Code</th>
                        <th className="border px-4 py-2">Cities</th>
                        <th className="border px-4 py-2">Enabled</th>
                        <th className="border px-4 py-2"></th>
                    </tr>
                </thead>
                <tbody>
                    {states.map((state) => (
                        <React.Fragment key={state.id}>
                            <tr>
                                <td className="border px-4 py-2">{state.id}</td>
                                <td className="border px-4 py-2">{state.name}</td>
                                <td className="border px-4 py-2">{state.code}</td>
                                <td className="border px-4 py-2">{state.cities}</td>
                                <td className="border px-4 py-2 text-center">
                                    <input
                                        type="checkbox"
                                        checked={state.enabled}
                                        onChange={() => toggleEnabled(state.id)}
                                    />
                                </td>
                                <td className="border px-4 py-2">
                                    <Link to={`${state.id}/cities`} className="px-2 py-1 bg-blue-500 text-white rounded mr-2">
                                        Cities
                                    </Link>
                                </td>
                            </tr>
                        </React.Fragment>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default StatesPage;
