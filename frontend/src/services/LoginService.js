import React from 'react';

export const loginUser = async (userData) => {
    try {
        const response = await fetch('https://localhost:7084/api/accountapi/login', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(userData)
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "Login failed!");
        }

        const data = await response.json();
        return data;
    }
    catch (error) {
        console.error("Error login user:", error.message);
        throw error;
    }
};