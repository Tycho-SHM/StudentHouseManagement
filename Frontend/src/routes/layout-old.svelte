<script lang="ts">
	import '../app.css';

    import { onMount } from 'svelte';
    import { initAuth, isLoading, isAuthenticated } from "$lib/stores/auth";
    import SignIn from "$lib/components/SignIn.svelte";
    import UserProfile from "$lib/components/UserProfile.svelte";
    import SignOut from "$lib/components/SignOut.svelte";
    import SignUp from "$lib/components/SignUp.svelte";

    onMount(() => {
        initAuth();
    });

    let { children } = $props();
</script>

<nav class="bg-gray-800 text-white">
    {#if $isLoading}
        <div class="loading">Loading authentication...</div>
    {:else}
        {#if $isAuthenticated}
            <UserProfile />
            <SignOut />
        {:else}
            <SignUp />
            <SignIn />
        {/if}
    {/if}
</nav>

{@render children()}
