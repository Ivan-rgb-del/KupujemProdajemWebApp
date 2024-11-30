import React, { useState, useEffect } from 'react';
import { fetchAdvertisements } from '../services/AdvertisementService';

const AdvertisementPage = () => {
    const [ads, setAds] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const ads = await fetchAdvertisements();
                setAds(ads);
            } catch (error) {
                console.error(error);
            }
        }

        fetchData();
    }, []);

    return (
        <div>
            <h1>Advertisement Page</h1>
            <ul>
                {ads.map((ad) => (
                    <li key={ad.id}>
                        <h2>{ad.title}</h2>
                        <p>Price: {ad.price}</p>
                        <p>{ad.description}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AdvertisementPage;