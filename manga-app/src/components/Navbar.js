import React from 'react';
import './Navbar.css';

const Navbar = ({ setActiveTab, isAdmin }) => {
    return (
        <nav>
            <ul>
                <li onClick={() => setActiveTab('manga')}>Манга</li>
                <li onClick={() => setActiveTab('authors')}>Авторы</li>
                <li onClick={() => setActiveTab('genres')}>Жанры</li>
                <li onClick={() => setActiveTab('reviews')}>Отзывы</li>
            </ul>
        </nav>
    );
};

export default Navbar;
