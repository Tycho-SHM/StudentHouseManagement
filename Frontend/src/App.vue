<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import {SidebarProvider, SidebarTrigger} from "@/components/ui/sidebar";
import AppSideBar from "@/components/AppSideBar.vue";
</script>

<template>
  <SidebarProvider>
    <AppSideBar />
    <main>
      <SidebarTrigger />
      <RouterView />
    </main>
  </SidebarProvider>
</template>

<style scoped>

</style>
