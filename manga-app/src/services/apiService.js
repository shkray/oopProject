// src/services/apiService.js
import axios from 'axios';

const BASE_URL = 'https://localhost:7066/api';

export const auth = async () => {
    const authData = {
        username: "Admin",
        password: "secret_admin"
    };

    console.log(authData)

    const response = await axios.post(`${BASE_URL.replace("/api", "")}/Auth/login`, authData);
    return response.data;
}

export const getGenres = async () => {
    const response = await axios.get(`${BASE_URL}/Genres`);
    
    return response.data;
};

export const getMangaReview = async (mangaId) => {
    let reviews = []

    const response = await axios.get(`${BASE_URL}/Reviews`);
    //console.log("mangaid: " + mangaId + ", response: " + response.data.toString());
    for (let review of response.data) {
        const submangaid = review["mangaId"];
        console.log("submangaid: " + submangaid + ", mangaid: " + mangaId + ", review: " + review.toString());
        if (submangaid === mangaId) {
            reviews.push(review);
        }
    }

    return reviews;
};

export const putReview = async (reviewid, mangaid, description, grade) => {
    const reviewData = {
        mangaId: mangaid,
        description: description,
        grade: grade,
        id: reviewid
    };
  
    const response = await axios.put(`${BASE_URL}/Reviews/${reviewid}`, reviewData);
    return response.data
};

export const postReview = async ( mangaid, description, grade) => {
    const reviewData = {
        mangaId: mangaid,
        description: description,
        grade: grade,
    };
  
    const response = await axios.post(`${BASE_URL}/Reviews`, reviewData);
    return response.data
};

export const getAuthors = async () => {
    const authData = await auth();

    const config = {
        headers: {
            Authorization: `Bearer ${authData.token}`
        }
    };

    console.log(config);

    const response = await axios.get(`${BASE_URL}/Authors`, config);
    return response.data;
};

export const getMangas = async () => {
    const response = await axios.get(`${BASE_URL}/Mangas`);
    return response.data;
};

export const getReviews = async () => {
    const response = await axios.get(`${BASE_URL}/Reviews`);
    return response.data;
};
