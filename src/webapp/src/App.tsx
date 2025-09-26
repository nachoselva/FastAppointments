import type { ReactNode } from 'react';
import { useState } from 'react';
import { FaCalendarAlt, FaCog, FaHome, FaMoneyBillWave, FaSignOutAlt, FaUser } from 'react-icons/fa';
import { Link, Route, BrowserRouter as Router, Routes, useLocation } from 'react-router-dom';
import './index.css';
import AppointmentsPage from './pages/Appointments';
import HomePage from './pages/Home';
import PaymentsPage from './pages/Payments';
import ProfilePage from './pages/Profile';
import SetupsPage from './pages/Setups';

interface MenuItem {
    name: string;
    path: string;
    icon: ReactNode;
}

const menuItems: MenuItem[] = [
    { name: 'Home', path: '/', icon: <FaHome /> },
    { name: 'Appointments', path: '/appointments', icon: <FaCalendarAlt /> },
    { name: 'Payments', path: '/payments', icon: <FaMoneyBillWave /> },
    { name: 'Setups', path: '/setups', icon: <FaCog /> },
];

interface SidebarProps {
    sidebarOpen: boolean;
    setSidebarOpen: (open: boolean) => void;
}

const Sidebar = ({ sidebarOpen, setSidebarOpen }: SidebarProps) => {
    const location = useLocation();

    return (
        <div
            className={`
        fixed inset-y-0 left-0 z-30 w-64 bg-white border-r border-gray-200 
        flex flex-col
        transform transition-transform duration-300 ease-in-out
        ${sidebarOpen ? 'translate-x-0' : '-translate-x-full'}
        md:translate-x-0 md:static md:flex-shrink-0
      `}
        >
            <div className="flex justify-end p-2 md:hidden">
                <button
                    onClick={() => setSidebarOpen(false)}
                    className="p-2 rounded hover:bg-gray-200"
                >
                    ✕
                </button>
            </div>

            <div className="p-4 flex-1">
                <h2 className="text-xl font-bold mb-6">My App</h2>
                <ul>
                    {menuItems.map((item) => (
                        <li key={item.name} className="mb-2">
                            <Link
                                to={item.path}
                                className={`flex items-center gap-2 p-2 rounded hover:bg-gray-200 ${location.pathname === item.path ? 'bg-gray-200 font-semibold' : ''
                                    }`}
                                onClick={() => setSidebarOpen(false)}
                            >
                                {item.icon}
                                {item.name}
                            </Link>
                        </li>
                    ))}
                </ul>
            </div>

            <div className="p-4 border-t border-gray-200">
                <ul>
                    <li className="mb-2">
                        <Link
                            to="/profile"
                            className={`flex items-center justify-between gap-2 p-2 rounded hover:bg-gray-200 ${location.pathname === '/profile' ? 'bg-gray-200 font-semibold' : ''
                                }`}
                            onClick={() => setSidebarOpen(false)}
                        >
                            <span>Profile</span>
                            <FaUser />
                        </Link>
                    </li>
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

const AppContent = () => (
    <div className="flex-1 flex flex-col">
        <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/appointments" element={<AppointmentsPage />} />
            <Route path="/payments" element={<PaymentsPage />} />
            <Route path="/setups" element={<SetupsPage />} />
            <Route path="/profile" element={<ProfilePage />} />
        </Routes>
    </div>
);

const App: React.FC = () => {
    const [sidebarOpen, setSidebarOpen] = useState(false);

    return (
        <Router>
            <div className="flex h-screen">

                <Sidebar sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />

                {sidebarOpen && (
                    <div
                        className="fixed inset-0 bg-black opacity-25 z-20 md:hidden"
                        onClick={() => setSidebarOpen(false)}
                    ></div>
                )}

                <div className="flex-1 flex flex-col">
                    <div className="bg-white p-4 border-b border-gray-200 flex justify-between items-center md:hidden">
                        <button
                            className="p-2 bg-gray-200 rounded"
                            onClick={() => setSidebarOpen(!sidebarOpen)}
                        >
                            ☰
                        </button>
                    </div>

                    <AppContent />
                </div>
            </div>
        </Router>
    );
};

export default App;
