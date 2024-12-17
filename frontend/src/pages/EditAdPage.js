import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { fetchAdDetail } from "../services/AdDetailService";
import { editAd } from "../services/EditAdService ";
import { getAllCategories } from "../services/GetAllCategoriesService";
import { getAllGroups } from "../services/GetAllGroupsService";

const EditAdPage = () => {
    const { adId } = useParams();
    const [categories, setCategories] = useState([]);
    const [groups, setGroups] = useState([]);
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
        advertisementCondition: "",
        deliveryType: "",
        advertisementCategoryId: "",
        advertisementGroupId: "",
        addressId: { city: "", street: "" },
    });

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

        const data = {
            ...adData,
            price: parseFloat(adData.price),
            advertisementCategoryId: +adData.advertisementCategoryId,
            advertisementGroupId: +adData.advertisementGroupId,
            advertisementCondition: +adData.advertisementCondition,
            deliveryType: +adData.deliveryType,
        };

        try {
            const response = await editAd(data, adId);
            navigate(`/dashboard`);
        } catch (error) {
            setError("Failed to update ad. Please try again.");
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
                        <option value={2}>Personal Pickup</option>
                    </select>
                </div>
                <div>
                    <label>Category:</label>
                    <select
                        name="advertisementCategoryId"
                        value={adData.advertisementCategoryId}
                        onChange={handleChange}
                    >
                        <option value="" disabled>Select a category</option>
                        {categories.map((category) => (
                            <option key={category.id} value={category.id}>
                                {category.name}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Group:</label>
                    <select
                        name="advertisementGroupId"
                        value={adData.advertisementGroupId}
                        onChange={handleChange}
                    >
                        <option value="" disabled>Select a group</option>
                        {groups.map((group) => (
                            <option key={group.id} value={group.id}>
                                {group.name}
                            </option>
                        ))}
                    </select>
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
