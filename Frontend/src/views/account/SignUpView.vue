<script setup lang="ts">

import { SignUp } from '@clerk/vue';

</script>

<template>
  SignUp time!
  <SignUp fallback-redirect-url="/dashboard" />
</template>

<style scoped>

</style>
