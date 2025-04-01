<script setup lang="ts">

import { SignIn } from '@clerk/vue';

</script>

<template>
  signin time!
  <SignIn fallback-redirect-url="/dashboard" />
</template>

<style scoped>

</style>
