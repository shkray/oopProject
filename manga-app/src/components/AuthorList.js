import React, { useState, useEffect } from 'react';
import { getAuthors } from '../services/apiService';
import './AuthorList.css';
import './AuthorItem.css';

const AuthorItem = ({ author }) => {
    return (
      <div className="author-item">
        <h2>{author.name}</h2>
      </div>
    );
  };
  

const AuthorList = ({ isAdmin }) => {
  const [authors, setAuthors] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getAuthors();
      setAuthors(data);
    };
    fetchData();
  }, []);

    const handleAddClick = () => {
        // Логика для добавления нового отзыва
        alert('Добавить новый отзыв');
    };

  return (
      <div className="author-list">
          {isAdmin && <button className="add-button" onClick={handleAddClick}>Добавить автора</button>}
      {authors.map(author => (
        <AuthorItem key={author.id} author={author} />
      ))}
    </div>
  );
};

export default AuthorList;
