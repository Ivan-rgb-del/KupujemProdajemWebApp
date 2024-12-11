export const addToFavorites = async (adId) => {
    try {
        const token = localStorage.getItem("token");
        if (!token) {
            throw new Error("User is not authenticated.");
        }

        const response = await fetch(`https://localhost:7084/api/favoriteapi/saveAd/${adId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const error = await response.text(); 
            throw new Error(error);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Failed to add to favorites:", error.message);
        throw error;
    }
};