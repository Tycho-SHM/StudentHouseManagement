<script>
    import { getClerk } from '$lib/clerk';
    import Button from "$lib/components/ui/button/Button.svelte";

    async function handleSignOut() {
        const clerk = await getClerk();
        await clerk.signOut();
        // Optionally redirect after sign out
        window.location.href = '/';
    }
</script>

<Button onclick={handleSignOut}>Sign Out</Button>