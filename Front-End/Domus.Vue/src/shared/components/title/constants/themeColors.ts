export const THEME_COLORS = {
    primaryDark: {
        background: "bg-primary-dark/20",
        text: "text-primary-dark",
        dot: "bg-primary-dark",
    },

    primary: {
        background: "bg-primary-soft/30",
        text: "text-primary",
        dot: "bg-primary",
    },

    primaryLight: {
        background: "bg-primary-soft/30",
        text: "text-primary-dark",
        dot: "bg-primary-light",
    },

    primarySoft: {
        background: "bg-primary-soft/30",
        text: "text-primary-dark",
        dot: "bg-primary",
    },

    accent: {
        background: "bg-accent/20",
        text: "text-primary-dark",
        dot: "bg-accent",
    },

    neutral: {
        background: "bg-background/20",
        text: "text-text-muted",
        dot: "bg-text-muted",
    },
} as const;

export type ThemeColor = keyof typeof THEME_COLORS;