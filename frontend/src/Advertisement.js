import React, { useEffect, useState } from 'react';

const Advertisement = () => {
    const [ads, setAds] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7084/api/advertisementapi')
            .then((response) => response.json())
            .then((data) => setAds(data))
            .catch((error) => console.error('Error fetching advertisements:', error));
    }, []);

    return (
        <div>
            <h1>Advertisements</h1>
            <ul>
                {ads.map((ad) => (
                    <li key={ad.id}>
                        <h2>{ad.title}</h2>
                        <p>Price: {ad.price}</p>
                        <p>{ad.description}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Advertisement;
