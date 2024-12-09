import React from 'react';

export const resetPassword = async (userData) => {
    try {
        const response = await fetch('https://localhost:7084/api/accountapi/forgot-password', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userData)
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