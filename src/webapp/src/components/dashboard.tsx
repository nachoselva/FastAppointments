import { Link } from "react-router-dom";
import { Card, CardContent, CardHeader, CardTitle } from "../components/card";

export interface DashboardItem {
    name: string;
    path: string;
    icon: React.ReactNode;
}

interface DashboardProps {
    title: string;
    items: DashboardItem[];
}

const Dashboard: React.FC<DashboardProps> = ({ title, items }) => {
    return (
        <div className="p-4">
            <Card>
                <CardHeader>
                    <CardTitle>{title}</CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 p-4">
                        {items.map((item) => (
                            <Link
                                to={item.path}
                                key={item.name}
                                className="flex flex-col items-center justify-center p-6 bg-gray-100 dark:bg-gray-800 rounded-lg shadow-md hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors"
                            >
                                <div className="text-4xl text-blue-500">{item.icon}</div>
                                <span className="mt-2 text-lg font-semibold text-center text-gray-800 dark:text-gray-200">{item.name}</span>
                            </Link>
                        ))}
                    </div>
                </CardContent>
            </Card>
        </div>
    );
};

export default Dashboard;
