<script lang="ts">
	import '../app.css';

    import { onMount } from 'svelte';
    import { initAuth, isLoading, isAuthenticated, user } from "$lib/stores/auth";
    import SignIn from "$lib/components/SignIn.svelte";
    import UserProfile from "$lib/components/UserProfile.svelte";
    import SignOut from "$lib/components/SignOut.svelte";
    import SignUp from "$lib/components/SignUp.svelte";
    import Button from "$lib/components/ui/button/Button.svelte";

    onMount(() => {
        initAuth();
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