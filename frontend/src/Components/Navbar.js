import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

const Navbar = () => {
    const isLoggedIn = localStorage.getItem("token");
    const navigate = useNavigate();

    const handleLogout = (e) => {
        e.preventDefault();

        localStorage.removeItem("token");
        localStorage.removeItem("email");
        localStorage.removeItem("userId");

        navigate('/');
    }

    return (
        <nav className="bg-blue-500 p-4 text-white">
            <div className="flex justify-between">
                <Link to="/" className="text-xl font-bold">
                    KupujemProdajem
                </Link>

                <div>
                    <Link to="/" className="mr-4 hover:underline">
                        Home
                    </Link>
                    <Link to="/advertisements" className="mr-4 hover:underline">
                        Advertisements
                    </Link>

                    {isLoggedIn ? (
                        <>
                            <Link to="/create" className="mr-4 hover:underline">
                                Create new Ad
                            </Link>
                            <button onClick={handleLogout} className="mr-4 hover:underline">
                                Logout
                            </button>
                            <Link to="/dashboard" className="hover:underline">
                                Dashboard
                            </Link>
                        </>
                    ) : (
                        <>
                            <Link to="/register" className="mr-4 hover:underline">
                                Register
                            </Link>
                            <Link to="/login" className="hover:underline">
                                Login
                            </Link>
                        </>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Navbar;