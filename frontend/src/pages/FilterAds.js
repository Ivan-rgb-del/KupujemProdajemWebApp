import React, { useState } from 'react';

const FilterAds = ({ onFilter }) => {
    const [filters, setFilters] = useState({
        city: '',
        categoryId: '',
        groupId: '',
        IsFixedPrice: false,
        IsReplacement: false,
        minPrice: '',
        maxPrice: '',
    });

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFilters((prevFilters) => ({
            ...prevFilters,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const handleFilterSubmit = (e) => {
        e.preventDefault();
        onFilter(filters);
    };

    return (
        <form onSubmit={handleFilterSubmit} className="mb-8 bg-white p-4 rounded shadow-md">
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                <input
                    type="text"
                    name="city"
                    placeholder="City"
                    value={filters.city}
                    onChange={handleInputChange}
                    className="p-2 border rounded"
                />
                <input
                    type="number"
                    name="categoryId"
                    placeholder="Category ID"
                    value={filters.categoryId}
                    onChange={handleInputChange}
                    className="p-2 border rounded"
                />
                <input
                    type="number"
                    name="groupId"
                    placeholder="Group ID"
                    value={filters.groupId}
                    onChange={handleInputChange}
                    className="p-2 border rounded"
                />
                <label className="flex items-center space-x-2">
                    <input
                        type="checkbox"
                        name="IsFixedPrice"
                        checked={filters.IsFixedPrice}
                        onChange={handleInputChange}
                    />
                    <span>Fixed Price</span>
                </label>
                <label className="flex items-center space-x-2">
                    <input
                        type="checkbox"
                        name="IsReplacement"
                        checked={filters.IsReplacement}
                        onChange={handleInputChange}
                    />
                    <span>Replacement</span>
                </label>
                <input
                    type="number"
                    name="minPrice"
                    placeholder="Min Price"
                    value={filters.minPrice}
                    onChange={handleInputChange}
                    className="p-2 border rounded"
                />
                <input
                    type="number"
                    name="maxPrice"
                    placeholder="Max Price"
                    value={filters.maxPrice}
                    onChange={handleInputChange}
                    className="p-2 border rounded"
                />
            </div>
            <button
                type="submit"
                className="mt-4 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
                Filter
            </button>
        </form>
    );
};

export default FilterAds;
