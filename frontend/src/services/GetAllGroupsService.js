import React from 'react';

export const getAllGroups = async () => {
    try {
        const response = await fetch('https://localhost:7084/api/advertisementapi/groups', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error("Failed to fetch groups!");
        }

        return await response.json();
    }
    catch (error) {
        throw new Error("Error fetching groups: ", error);
        throw error;
    }
};