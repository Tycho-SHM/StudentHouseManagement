<script lang="ts">
    import { isLoading, isAuthenticated } from "$lib/stores/auth";
    import SignIn from "$lib/components/SignIn.svelte";
</script>

{#if $isLoading}
    <div class="loading">Loading authentication...</div>
{:else}
    {#if $isAuthenticated}
        You're already logged in.
    {:else}
        <SignIn />
    {/if}
{/if}