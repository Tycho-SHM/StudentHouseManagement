<script lang="ts">
	import '../app.css';

    import { onMount } from 'svelte';
    import { goto } from "$app/navigation";
    import { initAuth, isLoading, isAuthenticated, user } from "$lib/stores/auth";
    import SignIn from "$lib/components/SignIn.svelte";
    import UserProfile from "$lib/components/UserProfile.svelte";
    import SignOut from "$lib/components/SignOut.svelte";
    import SignUp from "$lib/components/SignUp.svelte";
    import Button from "$lib/components/ui/button/Button.svelte";

    function getCookie(name: string): string | null {
        const cookies = document.cookie.split(';'); // Split cookies into an array
        for (let cookie of cookies) {
            // Trim whitespace and check if the cookie starts with the desired name
            cookie = cookie.trim();
            if (cookie.startsWith(`${name}=`)) {
                return cookie.substring(name.length + 1); // Return the value of the cookie
            }
        }
        return null; // Return null if the cookie is not found
    }

    onMount(async () => {
        await initAuth();

        if($isAuthenticated) {
            const response = await fetch(`${import.meta.env.VITE_API_BASE_URL}/profiles/UserProfile/GetOwnProfile`,
                {
                    method: "GET",
                    headers: {
                        Authorization: `Bearer ${getCookie("__session")}`,
                        "Content-Type": "application/json"
                    }
                });
            if (!response.ok) {
                if (response.status === 401) {
                    await goto("/sign-in");
                } else {
                    console.error("Failed to fetch user profile:", response.statusText);
                    return;
                }
            }
            const userProfile = await response.json();
            if(userProfile.displayName == null) {
                await goto("/onboarding/setup-userprofile");
            } else {
                await goto("/dashboard");
            }
        }
    });

    let { children } = $props();
</script>

<nav class="bg-gray-800 text-white py-2 px-4 flex mb-4 justify-evenly">
    <a href="/" class="px-2 font-bold">Tycho Student House management</a>
    {#if $isLoading}
        ...
    {:else}
        {#if $isAuthenticated}
            <span>Welcome {$user.firstName}</span>
            {#if $user.hasImage }
                <img src={$user.imageUrl} alt="User avatar" class="w-8 h-8 rounded-full ml-2" />
            {/if}
            <a href="/account-settings"><Button>Account Settings</Button></a>
            <SignOut />
        {:else}
            <a href="/sign-in"><Button>Sign in</Button></a>
        {/if}
    {/if}
</nav>

{@render children()}