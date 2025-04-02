<script setup lang="ts">
import {Button} from "@/components/ui/button";

import { useRouter } from 'vue-router';
const router = useRouter();

</script>

<template>
  <main>
    This is the homeview
    <Button @click="router.push('/account/sign-in')">Sign in</Button>
  </main>
</template>
