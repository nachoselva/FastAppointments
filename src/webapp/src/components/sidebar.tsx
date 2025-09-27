import { FaSignOutAlt, FaUser } from 'react-icons/fa';
import { Link, useLocation } from 'react-router-dom';
import type { MenuItem } from '../types/menu-item';
import { MenuItems } from './menu-items';

interface SidebarProps {
    sidebarOpen: boolean;
    setSidebarOpen: (open: boolean) => void;
}
interface SidebarLinkProps {
    item: MenuItem;
    isActive?: boolean | undefined;
    iconLeft?: boolean | undefined;
    setSidebarOpen: (open: boolean) => void;
}

const SidebarLink = ({ item, isActive, iconLeft, setSidebarOpen }: SidebarLinkProps) => {
    return (
        <Link
            to={item.path}
            className={`flex items-center justify-between gap-2 p-2 rounded hover:bg-gray-200 ${iconLeft ? 'justify-between' : ''} ${isActive ? 'bg-gray-100 font-semibold' : ''}`}
            onClick={() => setSidebarOpen(false)}>
            {
                iconLeft ?
                    <>
                        {item.icon}
                        {item.name}
                    </> :
                    <>
                        {item.name}
                        {item.icon}
                    </>
            }
        </Link>);
};

export const Sidebar = ({ sidebarOpen, setSidebarOpen }: SidebarProps) => {
    const location = useLocation();

    return (
        <div className={`fixed inset-y-0 left-0 z-30 w-64 bg-white border-r border-gray-200 flex flex-col transform transition-transform duration-300 ease-in-out 
            ${sidebarOpen ? 'translate-x-0' : '-translate-x-full'} md:translate-x-0 md:static md:flex-shrink-0`}>
            <div className="flex justify-end p-2 md:hidden">
                <button
                    onClick={() => setSidebarOpen(false)}
                    className="p-2 rounded hover:bg-gray-200">
                    ✕
                </button>
            </div>

            <div className="p-4 flex-1">
                <h2 className="text-xl font-bold mb-6">My App</h2>
                <ul>
                    {MenuItems.map((parentItem) => {
                        const isParentActive = location.pathname === parentItem.path || parentItem.children?.some((child) => location.pathname === child.path);

                        return (
                            <li key={parentItem.name} className="mb-1 relative group">
                                <SidebarLink
                                    item={parentItem}
                                    isActive={isParentActive}
                                    setSidebarOpen={setSidebarOpen} />

                                {!isParentActive && parentItem.children && (
                                    <ul className="absolute left-full top-0 hidden group-hover:block bg-white border border-gray-200 shadow-lg rounded min-w-[160px] z-40">
                                        {parentItem.children.map((child) => (
                                            <li key={parentItem.name} className="mb-1">
                                                <SidebarLink
                                                    key={child.name}
                                                    item={child}
                                                    setSidebarOpen={setSidebarOpen}
                                                />
                                            </li>
                                        ))}
                                    </ul>
                                )}

                                {isParentActive && parentItem.children && (
                                    <ul className="ml-6 mt-1">
                                        {parentItem.children.map((childItem) => {
                                            const isChildActive = location.pathname === childItem.path;
                                            return (
                                                <li key={parentItem.name} className="mb-1">
                                                    <SidebarLink
                                                        key={childItem.name}
                                                        item={childItem}
                                                        isActive={isChildActive}
                                                        setSidebarOpen={setSidebarOpen} />
                                                </li>
                                            );
                                        })}
                                    </ul>
                                )}
                            </li>
                        );
                    })}
                </ul>
            </div>

            <div className="p-4 border-t border-gray-200">
                <ul>
                    <SidebarLink
                        key={'Profile'}
                        item={{ name: 'Profile', path: 'profile', icon: <FaUser /> }}
                        isActive={location.pathname === '/profile'}
                        setSidebarOpen={setSidebarOpen} />
                    <li>
                        <button
                            onClick={() => alert('Logout clicked')}
                            className="flex w-full items-center justify-between gap-2 p-2 rounded hover:bg-gray-200 text-left"
                        >
                            <span>Logout</span>
                            <FaSignOutAlt />
                        </button>
                    </li>
                </ul>
            </div>

        </div>
    );
};