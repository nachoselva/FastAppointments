import React, { useEffect, useState } from "react";
import { Link } from 'react-router-dom';

interface Country {
    id: number;
    name: string;
    code: string;
    states: number;
    enabled: boolean;
}

const mockCountries: Country[] = [
    {
        id: 1,
        name: "Argentina",
        code: "AR",
        states: 23,
        enabled : true
    },
    {
        id: 2,
        name: "Brazil",
        code: "BR",
        states: 50,
        enabled : false
    }
];

const CountriesPage: React.FC = () => {
    const [countries, setCountries] = useState<Country[]>([]);

    useEffect(() => {
        setCountries(mockCountries);
    }, [mockCountries]);

    const toggleEnabled = (id: number) => {
        setCountries((prev) =>
            prev.map((c) =>
                c.id === id ? { ...c, enabled: !c.enabled } : c
            )
        );
    };

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Countries</h1>
            <table className="min-w-full border mb-8">
                <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">Name</th>
                        <th className="border px-4 py-2">Code</th>
                        <th className="border px-4 py-2">States</th>
                        <th className="border px-4 py-2">Enabled</th>
                        <th className="border px-4 py-2"></th>
                    </tr>
                </thead>
                <tbody>
                    {countries.map((country) => (
                        <React.Fragment key={country.id}>
                            <tr>
                                <td className="border px-4 py-2">{country.id}</td>
                                <td className="border px-4 py-2">{country.name}</td>
                                <td className="border px-4 py-2">{country.code}</td>
                                <td className="border px-4 py-2">{country.states}</td>
                                <td className="border px-4 py-2 text-center">
                                    <input
                                        type="checkbox"
                                        checked={country.enabled}
                                        onChange={() => toggleEnabled(country.id)}
                                    />
                                </td>
                                <td className="border px-4 py-2">
                                    <Link to={`${country.id}/states`} className="px-2 py-1 bg-blue-500 text-white rounded mr-2">
                                        States
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

export default CountriesPage;
