import React from 'react';

export const createAd = async (adData) => {
    try {
        const response = await fetch('https://localhost:7084/api/advertisementapi/create', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(adData)
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "Creating new ad failed!");
        }

        const data = await response.json();
        return data;
    }
    catch (error) {
        throw new Error("Error creating new ad: ", error);
        throw error;
    }
};