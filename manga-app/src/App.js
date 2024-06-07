import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Navbar from './components/Navbar';
import MangaList from './components/MangaList';
import AuthorList from './components/AuthorList';
import GenreList from './components/GenreList';
import ReviewList from './components/ReviewList';
import Admin from './components/Admin';
import './App.css';

const loginButton = {
    position: 'absolute',
    top: '10px',
    left: '10px',
    padding: '10px 20px',
    backgroundColor: 'rgba(204, 0, 0, 0.9)',
    color: '#fff',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer'
};

function App() {
    const [activeTab, setActiveTab] = useState('manga');

    const renderContent = (isAdmin = false) => {
        switch (activeTab) {
            case 'manga':
                return <MangaList isAdmin={isAdmin} />;
            case 'authors':
                return <AuthorList isAdmin={isAdmin} />;
            case 'genres':
                return <GenreList isAdmin={isAdmin} />;
            case 'reviews':
                return <ReviewList isAdmin={isAdmin} />;
            default:
                return <MangaList isAdmin={isAdmin} />;
        }
    };

    return (
        <Router>
            <div className="App">
                <header>
                    <button style={loginButton} onClick={() => window.location.href = '/admin'}>Admin</button>
                    <h1>Manga365</h1>
                </header>
                <Navbar setActiveTab={setActiveTab} isAdmin={false} />
                <main>
                    <Routes>
                        <Route path="/admin/*" element={
                            <>
                                <Admin activeTab={activeTab} />
                                {renderContent(true)}
                            </>
                        } />
                        <Route path="/" element={renderContent(false)} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;
