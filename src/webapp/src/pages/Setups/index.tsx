import Dashboard from "../../components/dashboard";
import { MenuItems } from "../../components/menu-items";

const SetupsPage: React.FC = () => {
    const setupsMenu = MenuItems.find(item => item.name === 'Setups');
    const dashboardItems = setupsMenu?.children?.map(item => ({
        name: item.name,
        path: item.path,
        icon: item.icon,
    })) || [];

    return <Dashboard title="Setups" items={dashboardItems} />;
};

export default SetupsPage;