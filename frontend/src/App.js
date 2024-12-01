import React from 'react';
import AdvertisementPage from './pages/AdvertisementPage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Components/Navbar';
import WelcomePage from './pages/WelcomePage';
import AdDetailPage from './pages/AdDetailPage';

function App() {
    return (
        <Router>
            <Navbar />
            <div className="container mx-auto">
                <Routes>
                    <Route path="/" element={<WelcomePage />} />
                    <Route path="/advertisements" element={<AdvertisementPage />} />
                    <Route path="/advertisements/:adId" element={<AdDetailPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
