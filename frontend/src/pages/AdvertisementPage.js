import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { fetchAdvertisements } from '../services/AdvertisementService';
import { Link } from 'react-router-dom';
import { addToFavorites } from '../services/AddToFavoritesService';
import FilterAds from './FilterAds';
import { fetchAdDetail } from '../services/FilterAdsService';

const AdvertisementPage = () => {
    const [ads, setAds] = useState([]);
    const navigate = useNavigate();

    const token = localStorage.getItem("token");

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

    const handleFilter = async (filters) => {
        try {
            const filteredAds = await fetchAdDetail(
                filters.city,
                parseInt(filters.categoryId, 10) || 0,
                parseInt(filters.groupId, 10) || 0,
                filters.IsFixedPrice,
                filters.IsReplacement,
                parseFloat(filters.minPrice) || 0,
                parseFloat(filters.maxPrice) || 0
            );
            setAds(filteredAds);
        } catch (error) {
            console.error("Failed to fetch filtered ads: ", error);
        }
    };

    const handleSave = async (adId) => {
        try {
            const response = await addToFavorites(adId);
            if (response) {
                navigate("/savedAds");
            }
        } catch (error) {
            console.error('Failed to add ad to favorites:', error);
        }
    }

    return (
        <div className="bg-gray-100 min-h-screen py-8">
            <div className="container mx-auto px-4">
                <h1 className="text-4xl font-bold text-center text-gray-800 mb-8">
                    Advertisements
                </h1>

                <FilterAds onFilter={handleFilter} />

                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                    {ads.map((ad) => (
                        <div
                            key={ad.id}
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
                                <div className="flex justify-between items-center mt-4">
                                    <div className="text-gray-500 text-sm">
                                        <span>Likes: {ad.likes || 0}</span> �{" "}
                                        <span>Viewers: {ad.viewers || 0}</span>
                                    </div>
                                </div>

                                <Link
                                    to={`/advertisements/${ad.id}`}
                                    className="mt-2 inline-block text-blue-500 hover:underline"
                                >
                                    More
                                </Link>

                                {token && (
                                    <div className="mt-4 flex items-center gap-2">
                                        <button
                                            onClick={() => handleSave(ad.id)}
                                            className="bg-blue-500 text-white px-3 py-1 rounded-lg text-sm hover:bg-blue-600 transition">
                                            Add to Favorites
                                        </button>
                                    </div>
                                )}

                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default AdvertisementPage;