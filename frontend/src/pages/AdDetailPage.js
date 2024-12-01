import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { fetchAdDetail } from '../services/AdDetailService';

const AdDetailPage = () => {
    const { adId } = useParams();
    const [ad, setAd] = useState();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const adDetail = await fetchAdDetail(adId);
                setAd(adDetail);
            } catch (error) {
                console.error(error);
            }
        }

        fetchData();
    }, []);

    if (!ad) {
        return <p>Loading...</p>
    }

    return (
        <div className="max-w-xl mx-auto mt-10 p-4 border rounded shadow-lg">
            <h1 className="text-2xl font-bold mb-4">{ad.title}</h1>
            <p className="text-lg">Price: {ad.price} EUR</p>
            <p className="mt-2">{ad.description}</p>
            <p className="text-sm text-gray-500 mt-4">Likes: {ad.likes} | Viewers: {ad.viewers}</p>
        </div>
    );
}

export default AdDetailPage;