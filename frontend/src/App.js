import React from 'react';
import AdvertisementPage from './pages/AdvertisementPage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Components/Navbar';
import WelcomePage from './pages/WelcomePage';
import AdDetailPage from './pages/AdDetailPage';
import RegisterPage from './pages/RegisterPage';
import LoginPage from './pages/LoginPage';
import ResetPasswordPage from './pages/ResetPasswordPage';
import CreateAdPage from './pages/CreateAdPage';
import DashboardPage from './pages/DashboardPage';
import EditAdPage from './pages/EditAdPage';
import SavedAdsPage from './pages/SavedAdsPage';

function App() {
    return (
        <Router>
            <Navbar />
            <div className="container mx-auto">
                <Routes>
                    <Route path="/" element={<WelcomePage />} />
                    <Route path="/advertisements" element={<AdvertisementPage />} />
                    <Route path="/advertisements/:adId" element={<AdDetailPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/forgot-password" element={<ResetPasswordPage />} />
                    <Route path="/create" element={<CreateAdPage />} />
                    <Route path="/dashboard" element={<DashboardPage />} />
                    <Route path="/update/:adId" element={<EditAdPage />} />
                    <Route path="/savedAds" element={<SavedAdsPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
