
export class HttpService{
    private BaseURL = "http://localhost:5038/domus";

    public async postAsync<TResponse, TRequest>(endpoint: string, data: TRequest): Promise<TResponse> {
        const response = await fetch(`${this.BaseURL}/${endpoint}`,{
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data),
        });

        return response.json() as Promise<TResponse>;
    }
}