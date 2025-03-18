<script lang="ts">
    import { onMount } from 'svelte';
    import { getClerk } from '$lib/clerk';

    let signUpDiv: any;
    let clerk;

    onMount(async () => {
        clerk = await getClerk();
        if (signUpDiv && !clerk.user) {
            clerk.mountSignUp(signUpDiv);
        }
    });
</script>

<div bind:this={signUpDiv} class="clerk-sign-up-container"></div>
