import { Card, CardContent, CardHeader, CardTitle } from "../../components/card";

const PaymentsPage: React.FC = () => {

    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>Payments</CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="flex items-center justify-center h-64">
                        <p className="text-gray-500">Payments functionality will be implemented here.</p>
                    </div>
                </CardContent>
            </Card>
        </div>
    );
};

export default PaymentsPage;