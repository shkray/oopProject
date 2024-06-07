import React, { useState, useEffect } from 'react';
import { getGenres } from '../services/apiService';
import './GenreList.css';
import './GenreItem.css';

const GenreItem = ({ genre }) => {
  return (
    <div className="genre-item">
      <h2>{genre.name}</h2>
      <p>Описание: {genre.description}</p>
    </div>
  );
};

const GenreList = ({ isAdmin }) => {
  const [genres, setGenres] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getGenres();
      setGenres(data);
    };
    fetchData();
  }, []);

    const handleAddClick = () => {
        // Логика для добавления нового отзыва
        alert('Добавить новый отзыв');
    };

  return (
      <div className="genre-list">
          {isAdmin && <button className="add-button" onClick={handleAddClick}>Добавить жанр</button>}
      {genres.map(genre => (
        <GenreItem key={genre.id} genre={genre} />
      ))}
    </div>
  );
};

export default GenreList;
