import { FaHome, FaCalendarAlt, FaMoneyBillWave, FaCog, FaUser } from 'react-icons/fa';
import type { MenuItem } from '../types/menu-item';

export const MenuItems: MenuItem[] = [
    { name: 'Home', path: '/', icon: <FaHome /> },
    { name: 'Appointments', path: '/appointments', icon: <FaCalendarAlt /> },
    { name: 'Payments', path: '/payments', icon: <FaMoneyBillWave /> },
    {
        name: 'Setups',
        path: '/setups',
        icon: <FaCog />,
        children: [
            { name: 'Users', path: '/setups/users', icon: <FaUser /> },
            { name: 'Roles', path: '/setups/roles', icon: <FaUser /> }
        ]
    }
];
