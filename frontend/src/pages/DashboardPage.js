import React, { useState, useEffect } from 'react';
import { dashboard } from '../services/DashboardService';
import { deleteAd } from '../services/DeleteAdService';

const DashboardPage = () => {
    const [ads, setAds] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAds = async () => {
            try {
                const userAds = await dashboard();
                setAds(userAds);
            } catch (error) {
                setError(error.message);
            }
        };

        fetchAds();
    }, []);

    const handleDelete = async (adId) => {
        try {
            await deleteAd(adId);
            setAds((prevAds) => prevAds.filter((ad) => ad.id !== adId));
            alert("Oglas je uspešno obrisan!");
        } catch (error) {
            console.error(error.message);
        }
    }

    return (
        <div className="bg-gray-100 min-h-screen py-8">
            <div className="container mx-auto px-4">
                <h1 className="text-4xl font-bold text-center text-gray-800 mb-8">
                    Moji Oglasi
                </h1>
                {ads.length === 0 ? (
                    <p className="text-center text-gray-500 text-lg">Nemate kreirane oglase.</p>
                ) : (
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                        {ads.map((ad) => (
                            <div
                                key={ad.id}
                                className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-300"
                            >
                                <img
                                    className="w-full h-48 object-cover"
                                    src={ad.imageURL || "https://via.placeholder.com/300"}
                                    alt={ad.title}
                                />
                                <div className="p-4">
                                    <h2 className="text-lg font-semibold text-gray-800 truncate">
                                        {ad.title}
                                    </h2>
                                    <p className="text-gray-600 font-bold mt-1">
                                        Price: {ad.price} €
                                    </p>

                                    <p className="text-sm text-gray-500 mt-2">
                                        {ad.description.length > 50
                                            ? ad.description.substring(0, 50) + "..."
                                            : ad.description}
                                    </p>

                                    <div className="flex justify-between items-center mt-4 text-gray-500 text-sm">
                                        <span>Lajkovi: {ad.likes || 0}</span>
                                        <span>Pregledi: {ad.viewers || 0}</span>
                                    </div>

                                    <div className="mt-4 flex items-center gap-2">
                                        <button className="bg-blue-500 text-white px-3 py-1 rounded-lg text-sm hover:bg-blue-600 transition">
                                            Edit
                                        </button>

                                        <button
                                            className="bg-red-500 text-white px-3 py-1 rounded-lg text-sm hover:bg-red-600 transition"
                                            onClick={() => handleDelete(ad.id)}
                                        >
                                            Delete
                                        </button>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default DashboardPage;