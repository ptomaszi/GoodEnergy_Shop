import { getApi } from "../core/api";
import { ApiClientResponce } from "../core/parse-api-error";
import { Product } from "../models/product";
import { TotalSummary } from "../models/total-summary";

const api = getApi;

export const getProducts = async () => {
    const response = await api().get<void, ApiClientResponce<Product[]>>("/shop");

    return response.data;
}

export const getTotal = async (products: Array<Product>) => {
    const response = await api().post<void, ApiClientResponce<TotalSummary>>(`/shop/getTotals`, products);

    return response.data;
}