import axios, { AxiosInstance } from "axios";

let api: AxiosInstance;

export const initApi = () => {
  api = axios.create();
};

export const getApi = () => api;