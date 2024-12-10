import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { fetchAdDetail } from "../services/AdDetailService";
import { editAd } from "../services/EditAdService ";

const EditAdPage = () => {
    const { adId } = useParams();
    const navigate = useNavigate();

    const [adData, setAdData] = useState({
        title: "",
        price: "",
        isFixedPrice: false,
        isReplacement: false,
        description: "",
        image: "",
        url: "",
        isActive: true,
        advertisementCondition: 1,
        deliveryType: 1,
        advertisementCategoryId: 1,
        advertisementGroupId: 1,
        addressId: { city: "", street: "" },
    });

    const advertisementConditionMap = {
        New: 1,
        Used: 2,
        Unused: 3,
        Damaged: 4
    };

    const deliveryTypeMap = {
        Delivery: 1,
        Pickup: 2,
    };

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchAdDetail(adId);
                const userId = localStorage.getItem("userId");
                setAdData({
                    ...data,
                    appUserId: userId,
                });
                setLoading(false);
            } catch (error) {
                setError("Failed to load ad details.");
                setLoading(false);
            }
        };

        fetchData();
    }, [adId]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setAdData((prev) => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const data = await editAd(adData, adId);
            navigate(`/dashboard`);
        } catch (error) {
            setError("Failed to update ad. Please try again.");
        }
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div className="edit-ad-page">
            <h1>Edit Ad</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Title:</label>
                    <input
                        type="text"
                        name="title"
                        value={adData.title}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Price:</label>
                    <input
                        type="number"
                        name="price"
                        value={adData.price}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Fixed Price:</label>
                    <input
                        type="checkbox"
                        name="isFixedPrice"
                        checked={adData.isFixedPrice}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Replacement:</label>
                    <input
                        type="checkbox"
                        name="isReplacement"
                        checked={adData.isReplacement}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Description:</label>
                    <textarea
                        name="description"
                        value={adData.description}
                        onChange={handleChange}
                    ></textarea>
                </div>
                <div>
                    <label>Image URL:</label>
                    <input
                        type="text"
                        name="image"
                        value={adData.url}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Condition:</label>
                    <select
                        name="advertisementCondition"
                        value={adData.advertisementCondition}
                        onChange={handleChange}
                    >
                        <option value={1}>New</option>
                        <option value={2}>Used</option>
                        <option value={3}>Unused</option>
                        <option value={4}>Damaged</option>
                    </select>
                </div>
                <div>
                    <label>Delivery Type:</label>
                    <select
                        name="deliveryType"
                        value={adData.deliveryType}
                        onChange={handleChange}
                    >
                        <option value={1}>Delivery</option>
                        <option value={2}>Pickup</option>
                    </select>
                </div>
                <div>
                    <label>Category ID:</label>
                    <input
                        type="number"
                        name="advertisementCategoryId"
                        value={adData.advertisementCategoryId}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Group ID:</label>
                    <input
                        type="number"
                        name="advertisementGroupId"
                        value={adData.advertisementGroupId}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>City:</label>
                    <input
                        type="text"
                        name="address.city"
                        value={adData.address.city}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Street:</label>
                    <input
                        type="text"
                        name="address.street"
                        value={adData.address.street}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit">Update Ad</button>
            </form>
        </div>
    );
};

export default EditAdPage;
