import React, { useState } from "react";
import { createAd } from "../services/CreateAdService";
import { useNavigate } from 'react-router-dom';

const CreateAdPage = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        title: "",
        price: "",
        isFixedPrice: false,
        isReplacement: false,
        description: "",
        imageURL: "",
        createdOn: new Date().toISOString(),
        isActive: true,
        advertisementCondition: "",
        deliveryType: "",
        advertisementCategoryId: "",
        advertisementGroupId: "",
        address: { city: "", street: "" },
    });

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        if (name.includes("address.")) {
            const field = name.split(".")[1];
            setFormData((prev) => ({
                ...prev,
                address: { ...prev.address, [field]: value },
            }));
        } else if (type === "checkbox") {
            setFormData({ ...formData, [name]: checked });
        } else {
            setFormData({ ...formData, [name]: value });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

            const adData = {
                ...formData,
                price: parseFloat(formData.price),
                advertisementCategoryId: parseInt(formData.advertisementCategoryId, 10),
                advertisementGroupId: parseInt(formData.advertisementGroupId, 10),
                advertisementCondition: parseInt(formData.advertisementCondition, 10),
                deliveryType: parseInt(formData.deliveryType, 10),
                AppUserId: localStorage.getItem("userId")
            };

        try {
            const response = await createAd(adData);
            navigate(`/dashboard`);
        } catch (error) {
            console.error("Error creating ad:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Title:</label>
                <input
                    type="text"
                    name="title"
                    value={formData.title}
                    onChange={handleChange}
                    required
                />
            </div>
            <div>
                <label>Price:</label>
                <input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    required
                />
            </div>
            <div>
                <label>Fixed Price:</label>
                <input
                    type="checkbox"
                    name="isFixedPrice"
                    checked={formData.isFixedPrice}
                    onChange={handleChange}
                />
            </div>
            <div>
                <label>Replacement:</label>
                <input
                    type="checkbox"
                    name="isReplacement"
                    checked={formData.isReplacement}
                    onChange={handleChange}
                />
            </div>
            <div>
                <label>Description:</label>
                <textarea
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    required
                ></textarea>
            </div>
            <div>
                <label>Image URL:</label>
                <input
                    type="text"
                    name="imageURL"
                    value={formData.imageURL}
                    onChange={handleChange}
                />
            </div>
            <div>
                <label>Condition:</label>
                <select
                    name="advertisementCondition"
                    value={formData.advertisementCondition}
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
                    value={formData.deliveryType}
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
                    value={formData.advertisementCategoryId}
                    onChange={handleChange}
                    required
                />
            </div>
            <div>
                <label>Group ID:</label>
                <input
                    type="number"
                    name="advertisementGroupId"
                    value={formData.advertisementGroupId}
                    onChange={handleChange}
                    required
                />
            </div>
            <div>
                <label>City:</label>
                <input
                    type="text"
                    name="address.city"
                    value={formData.address.city}
                    onChange={handleChange}
                />
            </div>
            <div>
                <label>Street:</label>
                <input
                    type="text"
                    name="address.street"
                    value={formData.address.street}
                    onChange={handleChange}
                />
            </div>
            <button type="submit">Create Ad</button>
        </form>
    );
};

export default CreateAdPage;
