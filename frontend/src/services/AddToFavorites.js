import React from 'react';

const addToFavorites = async (adId) => {
    const token = localStorage.getItem("token");

    if (!token) {
        throw new Error("User is not authenticated.");
    }

    try {
        const response = await fetch(`https://localhost:7084/api/favoriteapi/saveAd/${adId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const error = await response.text();
            throw new Error(error);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error saving ad to favorites:", error.messages);
        throw error;
    }
};