

export type ResponseSuccess<T> = {
    success: boolean,
    data: T
}

export type ResponseError = {
    status: number,
    title: string,
    detail: string,
    instance: string,
}