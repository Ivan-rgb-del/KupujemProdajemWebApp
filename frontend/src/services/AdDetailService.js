import React from 'react';

export const fetchAdDetail = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/advertisementapi/adId={adId}');
        if (!response.ok) {
            throw new Error("Failed to fetch ad!");
        }
        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching ad: ", error);
        throw error;
    }
};