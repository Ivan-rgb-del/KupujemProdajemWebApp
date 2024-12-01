import React from 'react';

export const fetchAdDetail = async (adId) => {
    try {
        const response = await fetch(`https://localhost:7084/api/advertisementapi/adId=${adId}`);
        if (!response.ok) {
            throw new Error("Failed to fetch ad!");
        }
        return await response.json();
    }
    catch (error) {
        console.error("Error fetching ad: ", error);
        throw error;
    }
};