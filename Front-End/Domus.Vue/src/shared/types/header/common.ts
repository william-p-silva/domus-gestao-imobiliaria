


export interface LinksHeader {
    label: string,
    route: string
}

export interface LinksHeaderModal {
    label: string,
    route: string,
    profile: 'Locador' | 'Locatario' | 'Admin' | 'User'
}