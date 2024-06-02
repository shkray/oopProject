/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const MangaCard = ({ manga }) => (
    <div className="manga-card">
      <img src={manga.image} alt={manga.title} className="manga-image" />
      <div className="manga-info">
        <h2 className="manga-title">{manga.title}</h2>
        <p className="manga-author">{manga.author}</p>
        <p className="manga-description">{manga.description}</p>
      </div>
    </div>
  );

const MangaCatalog = () => {
  const [mangas, setMangas] = useState([]);

  useEffect(() => {
    const fetchMangas = async () => {
      const response = await axios.get('http://localhost:3000/mangas');
      setMangas(response.data.data);
    };

    fetchMangas();
  }, []);

  return (
    <div className="manga-catalog">
      {mangas.map((manga) => (
        <MangaCard key={manga.id} manga={manga} />
      ))}
    </div>
  );
};

export default MangaCatalog;
