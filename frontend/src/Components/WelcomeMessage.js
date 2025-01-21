import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

const WelcomeMessage = ({ token }) => {
    const [message, setMessage] = useState("");

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7084/notificationHub", {
                accessTokenFactory: () => token || localStorage.getItem("token"),
            })
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveMessage", (message) => setMessage(message));

        connection
            .start()
            .then(() => {
                console.log("SignalR connected!");
                connection.invoke("SendWelcomeMessage", localStorage.getItem("email"));
            })
            .catch((error) => console.error("SignalR Connection Error:", error));

        return () => {
            connection.stop();
        };
    }, [token]);

    return <h1>{message}</h1>;
};

export default WelcomeMessage;
