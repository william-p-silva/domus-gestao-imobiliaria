import type { ResponseError, ResponseSuccess } from "@/shared/types/responseApi";


export class HttpService{
    private BaseURL: string = import.meta.env.VITE_API_URL_BASE;
    
    private async HandleResponse<TResponse>(response: Response): Promise<TResponse> {
        if(!response.ok){
            const errorResponse = await response.json() as ResponseError;
            const errorMessage: string = `HTTP Error! Status: ${response.status} (${response.statusText}). \n${errorResponse.message}`;
            throw new Error(errorMessage);
        }

        const jsonResponse = await response.json() as ResponseSuccess<TResponse>;
        
        return jsonResponse.data;
    }

    public async PostAsync<TResponse, TRequest>(
        endpoint: string, data: TRequest): Promise<TResponse> {

        const response = await fetch(`${this.BaseURL}/${endpoint}`,{
            method: "POST",
            credentials: "include",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data),
        });

        return await this.HandleResponse<TResponse>(response);
    }

    public async GetAsync<TResponse>(endpoint: string): Promise<TResponse>{
        const response = await fetch(`${this.BaseURL}/${endpoint}`, {
            method: "GET",
            credentials: "include",
            headers: {"Content-Type": "application/json"}
        });

        return await this.HandleResponse<TResponse>(response);
    }

    public async PutAsync<TResponse, TResquest>(endpoint: string, data: TResquest): Promise<TResponse>{
        const response = await fetch(`${this.BaseURL}/${endpoint}`, {
            method: "PUT",
            credentials: "include",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(data)
        });

        return await this.HandleResponse<TResponse>(response);
    }

    public async DeleteAsync<TResponse, TRequest>(endpoint: string, data: TRequest): Promise<TResponse>{
        const response = await fetch(`${this.BaseURL}/${endpoint}`, {
            method: "DELETE",
            credentials: "include",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(data)
        });

        return await this.HandleResponse<TResponse>(response);
    }
}