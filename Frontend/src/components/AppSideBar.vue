<script setup lang="ts">

import { Home, Search, LogIn, ChevronUp  } from "lucide-vue-next"

import { SignedIn, SignedOut, SignInButton, UserButton } from '@clerk/vue'

import { RouterLink } from 'vue-router'

import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup, SidebarGroupContent, SidebarGroupLabel,
  SidebarHeader, SidebarMenu, SidebarMenuButton, SidebarMenuItem,
} from '@/components/ui/sidebar'

import SideBarUser from "@/components/sidebar/SideBarUser.vue";
import {Button} from "@/components/ui/button";

import { watch, ref, onMounted } from 'vue';

import { useUser, useAuth } from '@clerk/vue';

const { getToken } = useAuth()
const { isSignedIn, user, isLoaded } = useUser();

import { useRouter } from 'vue-router';
const router = useRouter();

import { userProfileStore } from "@/datastores/UserProfileStore.ts";

async function loadUserProfile(newVal: any) {
  if (newVal) {
    const response = await fetch(`${import.meta.env.VITE_API_BASE_URL}/profiles/UserProfile/GetOwnProfile`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${await getToken.value()}`,
          "Content-Type": "application/json"
        }
      });
    if (!response.ok) {
      console.error("Failed to fetch user profile:", response.statusText);
      return;
    }
    userProfileStore.userProfile = await response.json();
    if (userProfileStore.userProfile?.displayName == null) {
      await router.push('/onboarding/profile-setup');
      return;
    }

  } else {
    watch(isSignedIn, loadUserProfile);
  }
}

onMounted(() => {
  loadUserProfile(isSignedIn.value);
});

const menuItems = [
  {
    title: "Home",
    url: "/dashboard/home",
    icon: Home
  },
  {
    title: "About",
    url: "/dashboard/about",
    icon: Search
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
          </SidebarMenu>
        </SidebarGroupContent>
      </SidebarGroup>
    </SidebarContent>
    <SidebarFooter>
      <SignedIn>
        <SideBarUser v-bind:userProfile="userProfileStore.userProfile" />
      </SignedIn>
      <SignedOut>
        <RouterLink to="/account/sign-in">
          <Button>Sign in</Button>
        </RouterLink>
      </SignedOut>
    </SidebarFooter>
  </Sidebar>
</template>
