import React from 'react';
import WelcomeMessage from '../Components/WelcomeMessage';

const WelcomePage = () => {
    return (
        <div className="text-center mt-10">
            <h1 className="text-4xl font-bold">
                Welcome
                <WelcomeMessage />
                on KupujemProdajem!
            </h1>
            <p className="mt-4 text-lg text-gray-700">
                Find ads or post your own quickly and easily.
            </p>
        </div>
    );
};

export default WelcomePage;