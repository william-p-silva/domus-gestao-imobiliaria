

export type ResponseSuccess<T> = {
    success: boolean,
    data: T
}

export type ResponseError = {
    success: boolean,
    message: string
}