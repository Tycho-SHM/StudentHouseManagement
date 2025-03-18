<script lang="ts">
    import { onMount } from 'svelte';
    import { getClerk } from '$lib/clerk';

    let signInDiv: any;
    let clerk;

    onMount(async () => {
        clerk = await getClerk();
        if (signInDiv && !clerk.user) {
            clerk.mountSignIn(signInDiv);
        }
    });
</script>

<div bind:this={signInDiv} class="clerk-sign-in-container"></div>
