import axios from "axios";

import type {
    Order,
    OrderFullInfo,
    CreateOrderRequest
} from "../types/order";


const API_URL = "/api/orders";


export async function getOrders() {
    const response =
        await axios.get<Order[]>(API_URL);

    return response.data;
}

export async function getOrderById(id: string) {
    const response =
        await axios.get<OrderFullInfo>(
            `${API_URL}/${id}`
        );

    return response.data;
}

export async function createOrder(
    data: CreateOrderRequest
) {
    const response =
        await axios.post<OrderFullInfo>(
            API_URL,
            data
        );

    return response.data;
}