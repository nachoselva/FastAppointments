import React, { useEffect, useState } from "react";
import { Link } from 'react-router-dom';
import { Card, CardContent, CardHeader, CardTitle } from "../../../components/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../../components/table";

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
        enabled: true
    },
    {
        id: 2,
        name: "Brazil",
        code: "BR",
        states: 50,
        enabled: false
    }
];


const CountriesPage: React.FC = () => {
    const [countries, setCountries] = useState<Country[]>([]);

    useEffect(() => {
        setCountries(mockCountries);
    }, []);
    const toggleEnabled = (id: number) => {
        setCountries((prev) =>
            prev.map((c) =>
                c.id === id ? { ...c, enabled: !c.enabled } : c
            )
        );
    };


    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Countries</CardTitle>
                </CardHeader>
                <CardContent>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Name</TableHead>
                                <TableHead>Code</TableHead>
                                <TableHead>States</TableHead>
                                <TableHead>Enabled</TableHead>
                                <TableHead>States</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {countries.map((country) => (
                                <TableRow key={country.id}>
                                    <TableCell>{country.id}</TableCell>
                                    <TableCell>{country.name}</TableCell>
                                    <TableCell>{country.code}</TableCell>
                                    <TableCell>{country.states}</TableCell>
                                    <TableCell>
                                        <input
                                            type="checkbox"
                                            checked={country.enabled}
                                            onChange={() => toggleEnabled(country.id)}
                                        />
                                    </TableCell>
                                    <TableCell>
                                        <Link to={`${country.id}/states`} className="px-2 py-1 bg-blue-500 text-white rounded mr-2">
                                            States
                                        </Link>
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

export default CountriesPage;
