import React from 'react';

export const dashboard = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/dashboardapi/dashboard', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem("token")}`,
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Failed to fetch user ads.');
        }

        const data = await response.json();
        return data.advertisements;
    }
    catch (error) {
        throw new Error("Error fetching ads: ", error);
        throw error;
    }
};