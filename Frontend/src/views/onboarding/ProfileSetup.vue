<script setup lang="ts">

import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'

import {useAuth, useUser} from '@clerk/vue'
import {Skeleton} from "@/components/ui/skeleton";
import {Avatar, AvatarFallback, AvatarImage} from "@/components/ui/avatar";

import { useUserProfile } from "@/datastores/UserProfileStore.ts";
import type {UserProfile} from "@/types/UserProfile.type.ts";
const { userProfileStore } = useUserProfile();


import { useRouter } from 'vue-router';
import {ref} from "vue";
const router = useRouter();

const { user, isLoaded } = useUser()
const { getToken } = useAuth()

const nameInput = ref<string>('')

async function UpdateProfile() {
  const response = await fetch(`${import.meta.env.VITE_API_BASE_URL}/profiles/UserProfile`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${await getToken.value()}`,
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      id: userProfileStore?.value?.id,
      displayName: nameInput.value,
      imgUrl: user!.value!.imageUrl,
      userId: userProfileStore?.value?.userId,
    })
  });
  if (!response.ok) {
    console.error("Failed to update profile:", response.statusText);
    return;
  }

  userProfileStore.value = await response.json();

  await router.push('/dashboard');
}

</script>

<template>
  <Card class="w-[350px]">
    <div v-if="isLoaded">
      <CardHeader>
        <CardTitle>Setup your profile</CardTitle>
        <CardDescription>This is how you will be seen by other users.</CardDescription>
      </CardHeader>
      <CardContent>
        <form>
          <div class="grid items-center w-full gap-4">
            <div class="flex justify-center">
              <Avatar class="h-32 w-32 rounded-lg">
                <AvatarImage v-if="user?.hasImage" :src="user.imageUrl" :alt="user?.firstName ?? ''" />
                <AvatarFallback class="rounded-lg">
                  {{ user?.firstName?.charAt(0) }}
                </AvatarFallback>
              </Avatar>
            </div>
            <div class="flex flex-col space-y-1.5">
              <Label for="name">Name</Label>
              <Input id="userProfileDisplayName" placeholder="Your name" v-model="nameInput" :defaultValue="user?.firstName ?? ''"/>
            </div>
          </div>
        </form>
      </CardContent>
      <CardFooter class="flex justify-between px-6 pb-6">
        <Button variant="outline">
          Cancel
        </Button>
        <Button @click="UpdateProfile">Submit</Button>
      </CardFooter>
    </div>
    <div v-else>
      <Skeleton class="h-[125px] w-[250px] rounded-xl m-5" />
    </div>
  </Card>
</template>

<style scoped>

</style>
