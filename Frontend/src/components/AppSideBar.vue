<script setup lang="ts">

import { Home, Search, LogIn  } from "lucide-vue-next"

import { SignedIn, SignedOut, SignInButton, UserButton } from '@clerk/vue'

import { RouterLink } from 'vue-router'

import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup, SidebarGroupContent, SidebarGroupLabel,
  SidebarHeader, SidebarMenu, SidebarMenuButton, SidebarMenuItem,
} from '@/components/ui/sidebar'

const menuItems = [
  {
    title: "Home",
    url: "/",
    icon: Home
  },
  {
    title: "About",
    url: "/about",
    icon: Search
  },
  {
    title: "Sign in",
    url: "/account/sign-in",
    icon: LogIn
  }
]
</script>

<template>
  <Sidebar>
    <SidebarContent>
      <SidebarGroup>
        <SidebarGroupLabel>Menu</SidebarGroupLabel>
        <SidebarGroupContent>
          <SidebarMenu>
            <SidebarMenuItem v-for="menuItem in menuItems" :key="menuItem.title">
              <SidebarMenuButton asChild>
                <RouterLink :to="menuItem.url">
                  <component :is="menuItem.icon" />
                  <span>{{menuItem.title}}</span>
                </RouterLink>
              </SidebarMenuButton>
            </SidebarMenuItem>
            <SidebarMenuItem>
              <SignedOut>
                <SignInButton />
              </SignedOut>
              <SignedIn>
                <UserButton />
              </SignedIn>
            </SidebarMenuItem>
          </SidebarMenu>
        </SidebarGroupContent>
      </SidebarGroup>
    </SidebarContent>
  </Sidebar>
</template>
