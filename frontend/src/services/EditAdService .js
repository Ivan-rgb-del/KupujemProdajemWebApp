import React from 'react';

export const editAd = async (adData, adId) => {
    try {
        const response = await fetch(`https://localhost:7084/api/advertisementapi/update/${adId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(adData),
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error("API error:", errorData);
            throw new Error(errorData.message || "Failed to update ad");
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error in editAd:", error);
        throw error;
    }
};