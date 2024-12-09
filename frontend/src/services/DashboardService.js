import React from 'react';

export const dashboard = async () => {
    const token = localStorage.getItem('token');

    if (!token) {
        console.error("User is not authenticated!");
        return;
    }

    try {
        const response = await fetch('https://localhost:7084/api/dashboardapi/dashboard', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Failed to fetch user ads.');
        }

        const data = await response.json();
        return data;
    }
    catch (error) {
        throw new Error("Error fetching ads: ", error);
        throw error;
    }
};