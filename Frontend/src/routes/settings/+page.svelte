<script lang="ts">
    import type { PageData } from "./$types.js";

    export let data: PageData;

    const settings = data.stringArray;
    console.log(settings)
</script>

<h1>Settings</h1>
<h3>Notifications:</h3>
<ul>
    {#each settings as setting }
        <li>{setting}</li>
    {/each}
</ul>
