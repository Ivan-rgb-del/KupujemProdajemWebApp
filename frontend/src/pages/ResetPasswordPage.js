import React, { useState } from 'react';
import { resetPassword } from '../services/ResetPasswordService';
import { useNavigate } from 'react-router-dom';

const ResetPasswordPage = () => {
    const [email, setEmail] = useState("");
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [message, setMessage] = useState("");
    const navigate = useNavigate();

    const handleResetPassword = async (e) => {
        e.preventDefault();

        try {
            const response = await resetPassword({ email, newPassword, confirmPassword });
            setMessage("Password reset successful!");
            navigate('/login');
        } catch (err) {
            setMessage(err.message);
        }
    };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-md bg-white rounded-lg shadow-md p-8">
                <h2 className="text-2xl font-bold text-center mb-6 text-gray-700">
                    Forgot Password
                </h2>
                <form onSubmit={handleResetPassword} className="space-y-4">
                    <div>
                        <label htmlFor="email" className="block text-sm font-medium text-gray-600">
                            Email:
                        </label>
                        <input
                            type="email"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            className="w-full px-4 py-2 mt-1 text-gray-700 bg-gray-50 border rounded-md focus:outline-none focus:ring focus:ring-blue-300"
                        />
                    </div>
                    <div>
                        <label htmlFor="newPassword" className="block text-sm font-medium text-gray-600">
                            New Password:
                        </label>
                        <input
                            type="password"
                            id="newPassword"
                            value={newPassword}
                            onChange={(e) => setNewPassword(e.target.value)}
                            required
                            className="w-full px-4 py-2 mt-1 text-gray-700 bg-gray-50 border rounded-md focus:outline-none focus:ring focus:ring-blue-300"
                        />
                    </div>
                    <div>
                        <label htmlFor="confirmPassword" className="block text-sm font-medium text-gray-600">
                            Confirm Password:
                        </label>
                        <input
                            type="password"
                            id="confirmPassword"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                            className="w-full px-4 py-2 mt-1 text-gray-700 bg-gray-50 border rounded-md focus:outline-none focus:ring focus:ring-blue-300"
                        />
                    </div>
                    <button
                        type="submit"
                        className={"w-full py-2 mt-4 text-white bg-blue-500 rounded-md focus:outline-none focus:ring focus:ring-blue-300 hover:bg-blue-600"}
                    >
                        Reset Password
                    </button>
                </form>
                {message && <p className="mt-4 text-sm text-green-600">{message}</p>}
            </div>
        </div>
    );
};

export default ResetPasswordPage;