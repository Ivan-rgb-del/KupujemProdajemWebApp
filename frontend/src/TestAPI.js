import React, { useEffect, useState } from 'react';

const TestAPI = () => {
    const [data, setData] = useState(null);

    useEffect(() => {
        fetch('/api/test')
            .then((response) => response.json())
            .then((data) => setData(data))
            .catch((error) => console.error('Error:', error));
    }, []);

    return (
        <div>
            <h1>Test API Response</h1>
            <pre>{JSON.stringify(data, null, 2)}</pre>
        </div>
    );
};

export default TestAPI;
