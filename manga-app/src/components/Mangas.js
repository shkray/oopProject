// src/components/Mangas.js
import React, { useState, useEffect } from 'react';
import { getMangas } from '../services/apiService';

const Mangas = () => {
    const [mangas, setMangas] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getMangas();
            setMangas(data);
        };
        fetchData();
    }, []);

    return (
        <div>
            <h2>Mangas</h2>
            <ul>
                {mangas.map(manga => (
                    <li key={manga.id}>
                        {manga.title} - {manga.status} - {manga.description} - Rating: {manga.rating}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Mangas;
