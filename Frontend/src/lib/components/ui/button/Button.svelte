<script lang="ts">
    let { children, ...restProps } = $props();
</script>

<button {...restProps}
    class="bg-amber-500 hover:bg-amber-600 text-white font-bold py-2 px-4 rounded hover:cursor-pointer">
    {@render children()}
</button>