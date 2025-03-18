<script lang="ts">
    import { onMount } from 'svelte';
    import { getClerk } from '$lib/clerk';

    let userProfileDiv: any;
    let clerk;

    onMount(async () => {
        clerk = await getClerk();
        if (userProfileDiv && clerk.user) {
            clerk.mountUserProfile(userProfileDiv);
        }
    });
</script>

<div bind:this={userProfileDiv} class="clerk-user-profile-container"></div>
