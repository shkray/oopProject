// src/components/Reviews.js
import React, { useState, useEffect } from 'react';
import { getReviews } from '../services/apiService';

const Reviews = () => {
    const [reviews, setReviews] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getReviews();
            setReviews(data);
        };
        fetchData();
    }, []);

    return (
        <div>
            <h2>Reviews</h2>
            <ul>
                {reviews.map(review => (
                    <li key={review.id}>
                        Grade: {review.grade} - {review.description}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Reviews;
