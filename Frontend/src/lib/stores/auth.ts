import {type Writable, writable} from 'svelte/store';
import { getClerk, isSignedIn, getUserData } from '$lib/clerk';

export const user: Writable<any> = writable(null);
export const isAuthenticated = writable(false);
export const isLoading = writable(true);

export async function initAuth() {
    isLoading.set(true);

    try {
        const clerk = await getClerk();

        // Set initial values
        isAuthenticated.set(await isSignedIn());
        user.set(await getUserData());

        // Set up listeners for auth state changes
        clerk.addListener((event: any) => {
            if (event.type === 'user') {
                isAuthenticated.set(!!clerk.user);
                user.set(clerk.user);
            }
        });
    } catch (error) {
        console.error('Failed to initialize auth:', error);
    } finally {
        isLoading.set(false);
    }
}
