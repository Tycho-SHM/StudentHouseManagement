<script lang="ts">
    import { initAuth, isLoading, isAuthenticated } from "$lib/stores/auth";
    import UserProfile from "$lib/components/UserProfile.svelte";
</script>

{#if $isLoading}
    <div class="loading">Loading authentication...</div>
{:else}
    <UserProfile />
{/if}