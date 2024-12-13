import React from 'react';

export const getAllCategories = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/advertisementapi/categories', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error("Failed to fetch categories!");
        }

        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching categories: ", error);
        throw error;
    }
};