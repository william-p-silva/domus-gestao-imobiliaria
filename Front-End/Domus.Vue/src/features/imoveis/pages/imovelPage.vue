<script setup lang="ts">
import { useRoute } from 'vue-router';


const route = useRoute();

const id = route.query.imovel_id;

</script>

<template>
    oi imovel {{ id }}
</template>