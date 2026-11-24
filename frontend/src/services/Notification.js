import React, { useContext } from "react";
import { NotificationContext } from "./NotificationContext";

const Notifications = () => {
    const { notifications } = useContext(NotificationContext);

    return (
        <div className="fixed bottom-0 right-0 m-4 z-50">
            {notifications.map((notification, index) => (
                <div
                    key={index}
                    className="bg-blue-500 text-white p-3 rounded shadow-lg mb-2"
                >
                    {notification}
                </div>
            ))}
        </div>
    );
};

export default Notifications;
