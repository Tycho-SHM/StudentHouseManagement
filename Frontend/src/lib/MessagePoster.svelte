<script>
    let message = '';
    let status = '';
    let isLoading = false;

    async function sendMessage() {
        if (!message) return;

        isLoading = true;
        status = 'Sending...';

        try {
            const encodedMessage = encodeURIComponent(message);
            const response = await fetch(`https://localhost:7204/tasks/task?message=${encodedMessage}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                status = 'Message sent successfully!';
                message = '';
            } else {
                status = `Error: ${response.status} - ${response.statusText}`;
            }
        } catch (error) {
            status = `Failed to send: ${error.message}`;
        } finally {
            isLoading = false;
        }
    }
</script>

<div>
    <div>
        <input
            type="text"
            bind:value={message}
            placeholder="Enter your message"
            disabled={isLoading}
        />
        <button on:click={sendMessage} disabled={isLoading}>
            Send
        </button>
    </div>

    {#if status}
        <p>{status}</p>
    {/if}
</div>