import React from 'react';

export const deleteAd = async (adId) => {
    try {
        const response = await fetch(`https://localhost:7084/api/advertisementapi/delete=${adId}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message);
        }

        const data = await response.json();
        return data;
    }
    catch (error) {
        console.error(error.message);
        throw error;
    }
};