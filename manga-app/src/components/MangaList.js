import React, { useState, useEffect } from 'react';
import { getMangas, postReview, getMangaReview, putReview } from '../services/apiService';
import './MangaList.css';
import './MangaItem.css';

const StarRating = ({ rating, onRatingChange }) => {
  const maxRating = 10;
  const stars = [];

  for (let i = 1; i <= maxRating; i++) {
    const starClassName = i <= rating ? 'filled-star' : 'empty-star';

    stars.push(
      <span
        key={i}
        className={starClassName}
        onClick={() => onRatingChange(i)}
      >
        ★
      </span>
    );
  }

  return <div className="star-rating">{stars}</div>;
};


const MangaItem = ({ manga }) => {
  const [showModal, setShowModal] = useState(false);
  const [review, setReview] = useState({
    mangaId: manga.id,
    text: '',
    rating: 0,
  });

  const handleOpenModal = () => {
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setReview({
      mangaId: manga.id,
      text: '',
      rating: 0,
    });
  };

  const handleSubmitReview = async () => {
    if (review.text.trim() !== '' && review.rating > 0) {
        const data = await getMangaReview(review.mangaId);
        console.log("полученные отзывы: " + data.toString());
        if (data.length === 0) {
          await postReview(review.mangaId, review.text, review.rating);
        } else {
          await putReview(data[0]["id"], review.mangaId, review.text, review.rating);
        }

      // Отправка отзыва на сервер
      console.log('Отзыв отправлен: ', review);
      handleCloseModal();
    } else {
      alert('Пожалуйста, заполните все поля корректно.');
    }
  };

  const handleInputChange = (e) => {
    setReview({
      ...review,
      [e.target.name]: e.target.value,
    });
  };

  const handleRatingChange = (newRating) => {
    setReview({
      ...review,
      rating: newRating,
    });
  };

  return (
    <div className="manga-item">
          <h2><a href={`${manga.url}`} >{manga.title}</a></h2>
      <p>Описание: {manga.description}</p>
      <p>Жанр: {manga.genre.name}</p>
      <p>Автор: {manga.author.name}</p>
      <button onClick={handleOpenModal}>Написать отзыв</button>

      {showModal && (
        <div className="modal">
          <h2>Написать отзыв</h2>
          <StarRating rating={review.rating} onRatingChange={handleRatingChange} />
          <textarea
            name="text"
            value={review.text}
            onChange={handleInputChange}
            placeholder="Введите текст отзыва..."
          />
          <div className="button-group">
            <button onClick={handleSubmitReview}>Отправить</button>
            <button onClick={handleCloseModal}>Закрыть</button>
          </div>
        </div>
      )}
    </div>
  );
};

/*
const MangaItem = ({ manga }) => {
  return (
    <div className="manga-item">
      <h2>{manga.title}</h2>
      <p>Описание: {manga.description}</p>
      <p>Жанр: {manga.genre.name}</p>
      <p>Автор: {manga.author.name}</p>
    </div>
  );
};
*/

const MangaList = ({ isAdmin }) => {
  const [mangas, setMangas] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getMangas();
      setMangas(data);
    };
    fetchData();
  }, []);

    const handleAddClick = () => {
        // Логика для добавления нового отзыва
        alert('Добавить новый отзыв');
    };

  return (
      <div className="manga-list">
          {isAdmin && <button className="add-button" onClick={handleAddClick}>Добавить мангу</button>}
      {mangas.map(manga => (
        <MangaItem key={manga.id} manga={manga} />
      ))}
    </div>
  );
};

export default MangaList;
