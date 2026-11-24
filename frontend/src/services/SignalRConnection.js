import * as signalR from "@microsoft/signalr";

let connection = null;

export const createConnection = () => {
    if (!connection) {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7084/notificationHub")
            .withAutomaticReconnect()
            .build();
    }
    return connection;
};

export const startConnection = async () => {
    try {
        if (connection && connection.state === "Disconnected") {
            await connection.start();
            console.log("Connected to SignalR hub.");
        }
    } catch (err) {
        console.error("Error starting SignalR connection:", err);
    }
};

export const getConnection = () => connection;