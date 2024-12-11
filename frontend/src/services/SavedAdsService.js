import React from 'react';

export const savedAds = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/favoriteapi/savedAds');
        if (!response.ok) {
            throw new Error("Failed to fetch ad!");
        }
        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching ad: ", error);
    }
};