import { AxiosError, AxiosResponse } from "axios";
import { TToast } from "./query-config-creator";

export interface ApiError {
    code: string;
    target: string;
    message: string;
}

export type ApiClientError<T = any> = AxiosError<T>

export type ApiClientResponce<T = any> = AxiosResponse<T>

export interface ApiErrorAggregate extends ApiError {
    details: ApiError[];
}

export function parseApiError(error: ApiClientError<ApiErrorAggregate>): string[] {
    if (!error?.response?.data) {
        return []
    }

    const errorRespData = error.response.data
    if (errorRespData?.details?.length) {
        const messages = errorRespData.details.filter(x => x.message);
        if (messages.length) {
            return messages.map(x => x.message)
        }
    }

    if (errorRespData.message) { return [errorRespData.message] }

    return []
}

export function handleApiError<ToastProps> (toast: TToast<ToastProps>, error: ApiClientError<ApiErrorAggregate>, fallbackMessage: string) {
    const parsedError = parseApiError(error);
    const message = parsedError.length ? parsedError.join("\n") : fallbackMessage;
   
    toast({type: "error", message} as unknown as ToastProps);
  };