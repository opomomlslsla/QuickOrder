export type Order = {
    id: string;
    senderCity: string;
    recipientCity: string;
    pickupDate: string;
    status: string;
    serialNumber: string;
}

export type OrderFullInfo = Order & {
    senderAddress: string;
    recipientAddress: string;
    weight: number;
};

export interface CreateOrderRequest {

    senderCity: string;

    senderAddress: string;

    recipientCity: string;

    recipientAddress: string;

    weight: number;

    pickupDate: string;
}