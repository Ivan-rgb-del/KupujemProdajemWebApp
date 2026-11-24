import React, { createContext, useState, useEffect } from "react";
import { createConnection, startConnection, getConnection } from "./SignalRConnection";

export const NotificationContext = createContext();

const NotificationProvider = ({ children }) => {
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        const connection = createConnection();
        startConnection();

        if (connection) {
            connection.on("ReceiveNotification", (message) => {
                setNotifications((prev) => [...prev, message]);
            });
        }

        return () => {
            if (connection) {
                connection.off("ReceiveNotification");
            }
        };
    }, []);

    return (
        <NotificationContext.Provider value={{ notifications, setNotifications }}>
            {children}
        </NotificationContext.Provider>
    );
};

export default NotificationProvider;
