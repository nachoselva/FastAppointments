import React from "react";
import Dashboard from "../../components/dashboard";
import { MenuItems } from "../../components/menu-items";

const HomePage: React.FC = () => {
    const dashboardItems = MenuItems.filter(item => item.path !== '/').map(item => ({
        name: item.name,
        path: item.path,
        icon: item.icon,
    }));

    return <Dashboard title="Home" items={dashboardItems} />;
};

export default HomePage;