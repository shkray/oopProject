// src/App.js
import React from 'react';
import Genres from '../manga-app/src/components/Genres';
import Authors from '../manga-app/src/components/Authors';
import Mangas from '../manga-app/src/components/Mangas';
import Reviews from '../manga-app/src/components/Reviews';
import './App.css';

function App() {
    return (
        <div className="App">
            <h1>Manga App</h1>
            <Genres />
            <Authors />
            <Mangas />
            <Reviews />
        </div>
    );
}

export default App;
