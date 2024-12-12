export const remove = async (adId) => {
    try {
        const token = localStorage.getItem("token");
        if (!token) {
            throw new Error("User is not authenticated.");
        }

        const response = await fetch(`https://localhost:7084/api/favoriteapi/remove/${adId}`, {
            method: "DELETE",
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const error = await response.json(); 
            throw new Error(error.message);
        }

        return true;
    } catch (error) {
        console.error(error.message);
        throw error;
    }
};