import { FaHome, FaCalendarAlt, FaMoneyBillWave, FaCog, FaUser, FaGlobe, FaAddressBook, FaBuilding, FaServicestack } from 'react-icons/fa';
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
            { name: 'Roles', path: '/setups/roles', icon: <FaUser /> },
            {
                name: 'Geography',
                path: '/setups/countries',
                icon: <FaGlobe />
            },
            {
                name: 'Addresses & Buildings',
                path: '/setups/addresses',
                icon: <FaAddressBook />
            },
            {
                name: 'Locations',
                path: '/setups/locations',
                icon: <FaBuilding />
            },
            {
                name: 'Service Provisions',
                path: '/setups/service-provisions',
                icon: <FaServicestack />
            }
        ]
    }
];
