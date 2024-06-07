// src/components/Authors.js
import React, { useState, useEffect } from 'react';
import { getAuthors } from '../services/apiService';

const Authors = () => {
    const [authors, setAuthors] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getAuthors();
            setAuthors(data);
        };
        fetchData();
    }, []);

    return (
        <div>
            <h2>Authors</h2>
            <ul>
                <li>Авторы</li>
                {authors.map(author => (
                    <li key={author.id}>
                        {author.name}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Authors;
