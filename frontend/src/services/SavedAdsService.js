import React from 'react';

export const savedAds = async (token) => {
    try {
        const response = await fetch('https://localhost:7084/api/favoriteapi/savedAds', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error(`Failed to fetch ads: ${response.statusText}`);
        }

        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching ad: ", error);
    }
};