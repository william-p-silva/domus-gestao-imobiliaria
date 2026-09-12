import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import type { ResponseMensagens } from "../types/chatResponse";


type MensagemCallback = (mensagem: ResponseMensagens) => void;

class ChatHub {
    private readonly baseURL = "http://localhost:5038/domus";
    private readonly connection: HubConnection;
    private mensagemCallbacks: MensagemCallback[] = []

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(`${this.baseURL}/hubs/chat-imovel`)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

            this.connection.on("ReceberMensagem", (message: ResponseMensagens) => {
                this.mensagemCallbacks.forEach((cb) => cb(message));
            })

        this.connection.onreconnecting((error) => {
            console.warn("SignalR reconectando...", error)
        })
        
        this.connection.onreconnected(() => {
            console.info("SignalR reconectado.");
        });

        this.connection.onclose((error) => {
            console.warn("SignalR conexão fechada.", error);
        });
    }

    async start(): Promise<void> {
        if (this.connection.state === HubConnectionState.Disconnected) {
            await this.connection.start();
        }
    }

    async stop(): Promise<void> {
        if (this.connection.state !== HubConnectionState.Disconnected) {
            await this.connection.stop();
        }
    }

    async joinGroupChat(chatId: string): Promise<void> {  
        if (
            this.connection.state !== HubConnectionState.Connected
        ) {
            await this.start();
        }
    
        await this.connection.invoke(
            "JoinChatGroup",
            chatId
        );
    }

    async leaveGroupChat(chatId: string): Promise<void> {   
        await this.connection.invoke(
            "LeaveChatGroup",
            chatId
        );
    }

    onReceberMensagem(callback: MensagemCallback): void {
        this.mensagemCallbacks.push(callback);
    }

    offReceberMensagem(callback: MensagemCallback): void {
        this.mensagemCallbacks = this.mensagemCallbacks.filter((cb) => cb !== callback);
    }

    get state(): HubConnectionState {
        return this.connection.state;
    }
}

export default ChatHub;