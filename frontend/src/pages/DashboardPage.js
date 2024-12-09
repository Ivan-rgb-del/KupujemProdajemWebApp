import React, { useState, useEffect } from 'react';
import { dashboard } from '../services/DashboardService';

const DashboardPage = () => {
    const [ads, setAds] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAds = async () => {
            try {
                const token = localStorage.getItem("jwt");
                if (!token) {
                    throw new Error("User is not authenticated!");
                }

                const userAds = await dashboard();
                setAds(userAds);
            } catch (error) {
                setError(error.message);
            }
        };

        fetchAds();
    }, []);

    return (
        <div>
            <h1>User Dashboard</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {ads.length === 0 ? (
                <p>No advertisements found.</p>
            ) : (
                <ul>
                    {ads.map((ad) => (
                        <li key={ad.id}>
                            <h2>{ad.title}</h2>
                            <p>Price: ${ad.price}</p>
                            <p>Description: {ad.description}</p>
                            <img src={ad.imageURL} alt={ad.title} style={{ width: '200px' }} />
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default DashboardPage;