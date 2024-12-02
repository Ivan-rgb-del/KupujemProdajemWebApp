import React from 'react';

export const registerNewUser = async (userData) => {
    try {
        const response = await fetch('https://localhost:7084/api/accountapi/register', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userData)
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "Registration failed!");
        }

        const data = await response.json();
        return data;
    }
    catch (error) {
        console.error("Error registering user:", error.message);
        throw error;
    }
};