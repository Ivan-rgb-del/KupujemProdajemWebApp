import React, { useState, useEffect } from "react";
import { createAd } from "../services/CreateAdService";
import { useNavigate } from 'react-router-dom';
import { getAllCategories } from "../services/GetAllCategoriesService";
import { getAllGroups } from "../services/GetAllGroupsService";

const CreateAdPage = () => {
    const navigate = useNavigate();
    const[categories, setCategories] = useState([]);
    const [groups, setGroups] = useState([]);

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

        console.log("Podaci koji se šalju na API:", adData);

        try {
            const response = await createAd(adData);
            console.log("Uspešan odgovor sa servera:", response);
            navigate(`/dashboard`);
        } catch (error) {
            console.error("Error creating ad:", error);
        }
    };

    useEffect(() => {
        const fetchCategoriesAndGroups = async () => {
            try {
                const categoriesData = await getAllCategories();
                setCategories(categoriesData);

                const groupsData = await getAllGroups();
                setGroups(groupsData);
            } catch (error) {
                console.error("Failed to fetch categories and groups", error);
            }
        }

        fetchCategoriesAndGroups();
    }, [])

    return (
        <form
            onSubmit={handleSubmit}
            className="max-w-3xl mx-auto p-6 bg-white shadow-md rounded-lg"
        >
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Title:</label>
                <input
                    type="text"
                    name="title"
                    value={formData.title}
                    onChange={handleChange}
                    required
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                />
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Price:</label>
                <input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    required
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                />
            </div>
            <div className="mb-4 flex items-center">
                <input
                    type="checkbox"
                    name="isFixedPrice"
                    checked={formData.isFixedPrice}
                    onChange={handleChange}
                    className="mr-2"
                />
                <label className="text-gray-700 font-bold">Fixed Price</label>
            </div>
            <div className="mb-4 flex items-center">
                <input
                    type="checkbox"
                    name="isReplacement"
                    checked={formData.isReplacement}
                    onChange={handleChange}
                    className="mr-2"
                />
                <label className="text-gray-700 font-bold">Replacement</label>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Description:</label>
                <textarea
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    required
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                ></textarea>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Image URL:</label>
                <input
                    type="text"
                    name="imageURL"
                    value={formData.imageURL}
                    onChange={handleChange}
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                />
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Condition:</label>
                <select
                    name="advertisementCondition"
                    value={formData.advertisementCondition}
                    onChange={handleChange}
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                >
                    <option value={1}>New</option>
                    <option value={2}>Used</option>
                    <option value={3}>Unused</option>
                    <option value={4}>Damaged</option>
                </select>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Delivery Type:</label>
                <select
                    name="deliveryType"
                    value={formData.deliveryType}
                    onChange={handleChange}
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                >
                    <option value={1}>Delivery</option>
                    <option value={2}>Personal Pickup</option>
                </select>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Category:</label>
                <select
                    name="advertisementCategoryId"
                    value={formData.advertisementCategoryId}
                    onChange={handleChange}
                    required
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                >
                    <option value="" disabled>Select a category</option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Group:</label>
                <select
                    name="advertisementGroupId"
                    value={formData.advertisementGroupId}
                    onChange={handleChange}
                    required
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                >
                    <option value="" disabled>Select a group</option>
                    {groups.map((group) => (
                        <option key={group.id} value={group.id}>
                            {group.name}
                        </option>
                    ))}
                </select>
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">City:</label>
                <input
                    type="text"
                    name="address.city"
                    value={formData.address.city}
                    onChange={handleChange}
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                />
            </div>
            <div className="mb-4">
                <label className="block text-gray-700 font-bold mb-2">Street:</label>
                <input
                    type="text"
                    name="address.street"
                    value={formData.address.street}
                    onChange={handleChange}
                    className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300"
                />
            </div>
            <button
                type="submit"
                className="w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 focus:outline-none focus:ring focus:ring-blue-300"
            >
                Create Ad
            </button>
        </form>
    );
};

export default CreateAdPage;
