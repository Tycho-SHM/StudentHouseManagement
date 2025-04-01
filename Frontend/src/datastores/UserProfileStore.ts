import { reactive } from 'vue';
import {UserProfile} from "@clerk/vue";

export const userProfileStore = reactive({
  userProfile: UserProfile
})
