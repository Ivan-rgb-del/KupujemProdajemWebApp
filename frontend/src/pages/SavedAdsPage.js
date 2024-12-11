import React, { useState, useEffect } from 'react';
import { savedAds } from '../services/SavedAdsService';
import { Link } from 'react-router-dom';

const SavedAdsPage = () => {
    const [ads, setAds] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const token = localStorage.getItem("token");

    useEffect(() => {
        const fetchSavedAds = async () => {
            try {
                const data = await savedAds(token);
                setAds(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchSavedAds();
    }, [token]);

    if (loading) return <p>Loading saved ads...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div className="bg-gray-100 min-h-screen py-8">
            <div className="container mx-auto px-4">
                <h1 className="text-4xl font-bold text-center text-gray-800 mb-8">
                    Saved Ads
                </h1>
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                    {ads.map((ad) => (
                        <div
                            key={ad.advertisementId}
                            className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-300"
                        >
                            <img
                                className="w-full h-48 object-cover"
                                src={ad.imageURL || "https://via.placeholder.com/150"}
                                alt={ad.title}
                            />
                            <div className="p-4">
                                <h2 className="text-lg font-semibold text-gray-800">
                                    {ad.title}
                                </h2>
                                <p className="text-gray-600">Price: ${ad.price.toFixed(2)}</p>
                                <p className="text-sm text-gray-500 mt-2">
                                    {ad.description.length > 50
                                        ? ad.description.substring(0, 50) + "..."
                                        : ad.description}
                                </p>

                                <div className="mt-4 flex items-center gap-2">
                                    <button className="bg-red-500 text-white px-3 py-1 rounded-lg text-sm">
                                        Remove
                                    </button>
                                    <Link
                                        to={`/advertisements/${ad.advertisementId}`}
                                        className="bg-blue-500 text-white px-3 py-1 rounded-lg text-sm">
                                        More
                                    </Link>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default SavedAdsPage;