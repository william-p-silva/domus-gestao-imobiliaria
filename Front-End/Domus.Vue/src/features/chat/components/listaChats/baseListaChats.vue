<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../../hooks/useChat';
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import HeaderListaChats from './headerListaChats.vue';
import CardChat from './cardChat.vue';


const {
    isLoading,
    error,
    chatsUser,
    setUserChats,
} = useChat()

const formatter = FormatterDate()


onMounted(async () => {
    await setUserChats();
});

</script>


<template>
    <aside
        class="
            w-88
            shrink-0
            h-full
            min-h-0
            flex
            flex-col
            border-r
            border-primary/10
            overflow-hidden
            pt-4
        "
    >
        <HeaderListaChats />

        <div class="flex-1 min-h-0 overflow-y-auto py-6">
            <CardChat
                v-for="chat in chatsUser"
                :key="chat.chat_ID"
                :chat="chat"
            />
        </div>
    </aside>
</template>