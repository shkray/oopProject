// src/components/Genres.js
import React, { useState, useEffect } from 'react';
import { getGenres } from '../services/apiService';

const Genres = () => {
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getGenres();
            setGenres(data);
        };
        fetchData();
    }, []);

    return (
        <div>
            <h2>Genres</h2>
            <ul>
                {genres.map(genre => (
                    <li key={genre.id}>
                        {genre.name} - {genre.description}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Genres;
