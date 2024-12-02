import React from 'react';

export const registerNewUser = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/register');
        if (!response.ok) {
            throw new Error("Failed to fetch ads!");
        }
        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching ads: ", error);
        throw error;
    }
};