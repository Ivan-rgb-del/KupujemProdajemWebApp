import React from 'react';

export const createAd = async (adData) => {
        const response = await fetch('https://localhost:7084/api/advertisementapi/create', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(adData)
        });

        console.log("API response status:", response.status);
        console.log("API response body:", await response.text());

        if (!response.ok) {
            let errorData;
            try {
                errorData = await response.json();
            } catch (error) {
                console.error("Error parsing response as JSON:", error);
                throw new Error("An unknown error occurred.");
            }

            console.error("API response error data:", errorData);
            throw new Error(errorData.title || "Failed to create ad");
        }

        const data = await response.json();
        return data;
};