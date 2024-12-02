import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <nav className="bg-blue-500 p-4 text-white">
            <div className="container mx-auto flex justify-between">
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
                    <Link to="/register" className="mr-4 hover:underline">
                        Register
                    </Link>
                    <Link to="/login" className="mr-4 hover:underline">
                        Login
                    </Link>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;