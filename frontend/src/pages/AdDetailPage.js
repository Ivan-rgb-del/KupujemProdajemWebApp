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

    const getConditionLabel = (condition) => {
        switch (condition) {
            case 0:
                return "New";
            case 1:
                return "Used";
            case 2:
                return "Unused";
            case 3:
                return "Damaged";
            default:
                return "Unknown";
        }
    };

    if (!ad) {
        return <p>Loading...</p>
    }

    return (
        <div className="max-w-3xl mx-auto mt-10 p-6 bg-white border rounded-lg shadow-lg">
            <div className="flex items-center justify-between mb-6">
                <h1 className="text-3xl font-bold text-gray-800">{ad.title}</h1>
                <div className="text-right">
                    <p className="text-2xl font-semibold text-blue-600">
                        {ad.price} EUR
                    </p>
                    <p className={`text-sm ${ad.isFixedPrice ? 'text-green-500' : 'text-gray-500'}`}>
                        {ad.isFixedPrice ? 'Fixed Price' : 'Negotiable'}
                    </p>
                </div>
            </div>

            {ad.imageURL && (
                <div className="mb-6">
                    <img
                        src={ad.imageURL}
                        alt={ad.title}
                        className="w-full h-64 object-cover rounded-lg"
                    />
                </div>
            )}

            <p className="text-gray-700 text-lg mb-6">{ad.description}</p>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-6 mb-6">
                <div>
                    <p className="text-gray-500">Condition:</p>
                    <p className="text-gray-800 font-medium">
                        {getConditionLabel(ad.advertisementCondition)}
                    </p>
                </div>
                <div>
                    <p className="text-gray-500">Delivery Type:</p>
                    <p className="text-gray-800 font-medium">{ad.deliveryType ? 'Personal pickup' : 'Delivery'}</p>
                </div>
                <div>
                    <p className="text-gray-500">Category ID:</p>
                    <p className="text-gray-800 font-medium">{ad.advertisementCategoryId ?? 'N/A'}</p>
                </div>
                <div>
                    <p className="text-gray-500">Group ID:</p>
                    <p className="text-gray-800 font-medium">{ad.advertisementGroupId ?? 'N/A'}</p>
                </div>
            </div>

            <div className="mb-6">
                <p className="text-gray-500">Address:</p>
                <p className="text-gray-800 font-medium">
                    {ad.address?.street}, {ad.address?.city}
                </p>
            </div>

            <div className="flex items-center space-x-6 mb-6">
                <p className={`text-sm font-medium ${ad.isReplacement ? 'text-blue-500' : 'text-gray-500'}`}>
                    {ad.isReplacement ? 'Replacement Available' : 'No Replacement'}
                </p>
                <p className={`text-sm font-medium ${ad.isActive ? 'text-green-500' : 'text-red-500'}`}>
                    {ad.isActive ? 'Active' : 'Inactive'}
                </p>
            </div>

            <div className="flex items-center justify-between bg-gray-100 p-4 rounded-lg">
                <p className="text-gray-700">
                    <span className="font-semibold">{ad.likes ?? 0}</span> Likes
                </p>
                <p className="text-gray-700">
                    <span className="font-semibold">{ad.viewers ?? 0}</span> Viewers
                </p>
            </div>
        </div>
    );
}

export default AdDetailPage;