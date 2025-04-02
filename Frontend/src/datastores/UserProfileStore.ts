import { ref } from 'vue';
import type { UserProfile } from "@/types/UserProfile.type.ts";

const userProfileStore = ref<UserProfile | null>(null);

export function useUserProfile() {
  return {
    userProfileStore
  };
}
