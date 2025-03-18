<script>
    import { getClerk } from '$lib/clerk';

    async function handleSignOut() {
        const clerk = await getClerk();
        await clerk.signOut();
        // Optionally redirect after sign out
        window.location.href = '/';
    }
</script>

<button on:click={handleSignOut} class="sign-out-button">
    Sign Out
</button>
