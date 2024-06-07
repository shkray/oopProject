import React, { useState, useEffect } from 'react';
import { getReviews } from '../services/apiService';
import './ReviewList.css';
import './ReviewItem.css';

const ReviewItem = ({ review }) => {
    return (
        <div className="review-item">
            <h2>Оценка: {review.grade}</h2>
            <p>{review.description}</p>
        </div>
    );
};

const ReviewList = ({ isAdmin }) => {
    const [reviews, setReviews] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getReviews();
            setReviews(data);
        };
        fetchData();
    }, []);

    const handleAddClick = () => {
        // Логика для добавления нового отзыва
        alert('Добавить новый отзыв');
    };

    return (
        <div className="review-list">
            {reviews.map(review => (
                <ReviewItem key={review.id} review={review} />
            ))}
        </div>
    );
};

export default ReviewList;
