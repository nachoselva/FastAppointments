import type { MenuItem } from "../../types/menu-item";

interface SetupsPageProps {
    subMenu?: MenuItem[];
}

const SetupsPage: React.FC<SetupsPageProps> = ({ subMenu }) => {
    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold">Setups</h1>

            {subMenu && (
                <div className="mt-4">
                    <h2 className="text-xl font-semibold mb-2">Available Sections</h2>
                    <ul className="list-disc pl-6">
                        {subMenu.map((item) => (
                            <li key={item.path}>{item.name}</li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default SetupsPage;