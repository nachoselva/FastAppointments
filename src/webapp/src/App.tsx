import { useState } from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import { Sidebar } from './components/sidebar';
import './index.css';
import AppointmentsPage from './pages/appointments';
import HomePage from './pages/home';
import PaymentsPage from './pages/payments';
import ProfilePage from './pages/profile';
import SetupsPage from './pages/setups';

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
