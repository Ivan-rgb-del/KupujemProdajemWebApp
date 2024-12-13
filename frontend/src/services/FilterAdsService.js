import React from 'react';

export const fetchAdDetail = async (city, categoryId, groupId, IsFixedPrice, IsReplacement, minPrice, maxPrice) => {
    try {
        const response = await fetch(`https://localhost:7084/api/advertisementapi/ad/city=${city}/categoryId=${categoryId}/groupId=${groupId}/IsFixedPrice=${IsFixedPrice}/IsReplacement=${IsReplacement}/minPrice=${minPrice}/maxPrice=${maxPrice}`);
        if (!response.ok) {
            throw new Error("Failed to fetch ads!");
        }
        return await response.json();
    }
    catch (error) {
        console.error("Error fetching ad: ", error);
        throw error;
    }
};