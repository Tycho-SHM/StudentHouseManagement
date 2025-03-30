<script lang="ts">
    import { isLoading, isAuthenticated } from "$lib/stores/auth";
    import SignIn from "$lib/components/SignIn.svelte";
</script>

{#if $isLoading}
    <div class="loading">Loading authentication...</div>
{:else}
    {#if $isAuthenticated}
        Loading Dashboard...
    {:else}
        <SignIn />
    {/if}
{/if}