import { ApiClientError, ApiErrorAggregate, parseApiError } from "./parse-api-error";

export type TToast<P> = (props: P) => React.ReactText;

export function queryConfigCreator<T>(toast: TToast<T>) {
    return {
        defaultOptions: {
            queries: { refetchOnWindowFocus: false },
            mutations: {
                throwOnError: true,
                onError: (error: unknown) => {
                    toast({ type: "error", message: parseApiError(error as ApiClientError<ApiErrorAggregate>).join('\n') } as unknown as T)
                }
            },
        },
    };
}
