import React, { useEffect, useState } from "react";
import { Link } from 'react-router-dom';
import { Card, CardContent, CardHeader, CardTitle } from "../../../../components/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../../../components/table";

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
        cities: 10,
        enabled: true
    },
    {
        id: 2,
        name: "Cordoba",
        code: "CB",
        cities: 10,
        enabled: false
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
            <Card>
                <CardHeader>
                    <CardTitle>States</CardTitle>
                </CardHeader>
                <CardContent>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>ID</TableHead>
                                <TableHead>Name</TableHead>
                                <TableHead>Code</TableHead>
                                <TableHead>Cities</TableHead>
                                <TableHead>Enabled</TableHead>
                                <TableHead> Cities </TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {states.map((state) => (
                                <TableRow key={state.id}>
                                    <TableCell>{state.id}</TableCell>
                                    <TableCell>{state.name}</TableCell>
                                    <TableCell>{state.code}</TableCell>
                                    <TableCell>{state.cities}</TableCell>
                                    <TableCell>
                                        <input
                                            type="checkbox"
                                            checked={state.enabled}
                                            onChange={() => toggleEnabled(state.id)}
                                        />
                                    </TableCell>
                                    <TableCell>
                                        <Link to={`${state.id}/cities`} className="px-2 py-1 bg-blue-500 text-white rounded mr-2">
                                            Cities
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

export default StatesPage;
