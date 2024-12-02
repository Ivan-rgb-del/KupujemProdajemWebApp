import React, { useState, useEffect } from 'react';
import { registerNewUser } from '../services/RegisterService';

const RegisterPage = () => {
    const [formData, setFormData] = useState({
        emailAddress: "",
        password: "",
        confirmPassword: "",
        city: "",
        street: ""
    });
    const [message, setMessage] = useState("");
    const [error, setError] = useState("");

    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData(prevData => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await registerNewUser({
                emailAddress: formData.emailAddress,
                password: formData.password,
                confirmPassword: formData.confirmPassword,
                address: {
                    city: formData.city,
                    street: formData.street
                },
            });
            setMessage("User registered successfully!");
            setError("");
        } catch (err) {
            setError(err.message);
            setMessage("");
        }
    }

    return (
        <div className="max-w-md mx-auto p-6 bg-white shadow rounded mt-10">
            <h2 className="text-2xl font-bold mb-4 text-center">Register</h2>
            {message && <p className="text-green-500 text-center">{message}</p>}
            {error && <p className="text-red-500 text-center">{error}</p>}
            <form onSubmit={handleSubmit} className="space-y-4">
                <input
                    type="email"
                    name="emailAddress"
                    value={formData.emailAddress}
                    onChange={handleChange}
                    placeholder="Email Address"
                    className="w-full border rounded px-3 py-2"
                    required
                />
                <input
                    type="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    placeholder="Password"
                    className="w-full border rounded px-3 py-2"
                    required
                />
                <input
                    type="password"
                    name="confirmPassword"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    placeholder="Confirm Password"
                    className="w-full border rounded px-3 py-2"
                    required
                />
                <input
                    type="text"
                    name="city"
                    value={formData.city}
                    onChange={handleChange}
                    placeholder="City"
                    className="w-full border rounded px-3 py-2"
                    required
                />
                <input
                    type="text"
                    name="street"
                    value={formData.street}
                    onChange={handleChange}
                    placeholder="Street"
                    className="w-full border rounded px-3 py-2"
                    required
                />
                <button
                    type="submit"
                    className="w-full bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                >
                    Register
                </button>
            </form>
        </div>
    );
};

export default RegisterPage;