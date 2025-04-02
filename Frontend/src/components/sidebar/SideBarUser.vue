<script setup lang="ts">
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from '@/components/ui/avatar'

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'

import {
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  useSidebar,
} from '@/components/ui/sidebar'

import {
  BadgeCheck,
  Bell,
  ChevronsUpDown,
  CreditCard,
  LogOut,
  Sparkles,
} from 'lucide-vue-next'

import { useClerk } from '@clerk/vue'

const clerk = useClerk();

import type {UserProfile} from "@/types/UserProfile.type.ts";

defineProps<{
  userProfile?: UserProfile
}>()


const { isMobile } = useSidebar()
</script>
<template>
  <SidebarMenu v-if="userProfile?.displayName != null">
    <SidebarMenuItem>
      <DropdownMenu>
        <DropdownMenuTrigger as-child>
          <SidebarMenuButton
            size="lg"
            class="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
          >
            <Avatar class="h-8 w-8 rounded-lg">
              <AvatarImage v-if="userProfile.imgUrl != null" :src="userProfile.imgUrl" :alt="userProfile.displayName" />
              <AvatarFallback class="rounded-lg">
                {{ userProfile.displayName.charAt(0) }}
              </AvatarFallback>
            </Avatar>
            <div class="grid flex-1 text-left text-sm leading-tight">
              <span class="truncate font-semibold">{{ userProfile.displayName }}</span>
              <span class="truncate text-xs">email@example.com</span>
            </div>
            <ChevronsUpDown class="ml-auto size-4" />
          </SidebarMenuButton>
        </DropdownMenuTrigger>
        <DropdownMenuContent
          class="w-[--reka-dropdown-menu-trigger-width] min-w-56 rounded-lg"
          :side="isMobile ? 'bottom' : 'right'"
          align="end"
          :side-offset="4"
        >
          <DropdownMenuLabel class="p-0 font-normal">
            <div class="flex items-center gap-2 px-1 py-1.5 text-left text-sm">
              <Avatar class="h-8 w-8 rounded-lg">
                <AvatarImage v-if="userProfile.imgUrl != null" :src="userProfile.imgUrl" :alt="userProfile.displayName" />
                <AvatarFallback class="rounded-lg">
                  {{ userProfile.displayName.charAt(0) }}
                </AvatarFallback>
              </Avatar>
              <div class="grid flex-1 text-left text-sm leading-tight">
                <span class="truncate font-semibold">{{ userProfile.displayName }}</span>
                <span class="truncate text-xs">email@example.com</span>
              </div>
            </div>
          </DropdownMenuLabel>
          <DropdownMenuSeparator />
          <DropdownMenuGroup>
            <DropdownMenuItem>
              <Sparkles />
              Upgrade to Pro
            </DropdownMenuItem>
          </DropdownMenuGroup>
          <DropdownMenuSeparator />
          <DropdownMenuGroup>
            <DropdownMenuItem>
              <BadgeCheck />
              Account
            </DropdownMenuItem>
            <DropdownMenuItem>
              <CreditCard />
              Billing
            </DropdownMenuItem>
            <DropdownMenuItem>
              <Bell />
              Notifications
            </DropdownMenuItem>
          </DropdownMenuGroup>
          <DropdownMenuSeparator />
          <DropdownMenuItem class="hover:cursor-pointer" @click="clerk!.signOut">
            <LogOut />
            Sign out
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </SidebarMenuItem>
  </SidebarMenu>
</template>
