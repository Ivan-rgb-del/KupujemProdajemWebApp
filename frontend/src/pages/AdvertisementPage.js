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
        <div className="p-8 bg-gray-100 min-h-screen">
            <h1 className="text-4xl font-bold text-center mb-6 text-gray-800">
                Advertisement Page
            </h1>
            <ul className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {ads.map((ad) => (
                    <li
                        key={ad.id}
                        className="bg-white rounded-lg shadow-md p-4 hover:shadow-lg transition-shadow"
                    >
                        <h2 className="text-2xl font-semibold text-gray-700 mb-2">
                            {ad.title}
                        </h2>
                        <p className="text-lg text-gray-500 mb-4">
                            Price: <span className="text-green-600 font-bold">{ad.price} $</span>
                        </p>
                        <p className="text-gray-600">{ad.description}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AdvertisementPage;