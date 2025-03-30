
let Clerk: any;
let clerk: any = null;

export async function initClerk() {
    if (!clerk) {
        // Dynamically import the module
        const ClerkModule = await import('@clerk/clerk-js');
        Clerk = ClerkModule.default || ClerkModule.Clerk;

        clerk = new Clerk(import.meta.env.VITE_CLERK_PUBLISHABLE_KEY);
        await clerk.load({
            // Optional configuration
            appearance: {
                // Customize appearance
                elements: {
                    formButtonPrimary: 'bg-blue-500 hover:bg-blue-600 text-white',
                    // Add more styling as needed
                },
            },
            signInFallbackRedirectUrl: '/sign-in'
        });
    }
    return clerk;
}

// Helper functions to access common Clerk functionality
export async function getClerk() {
    return await initClerk();
}

export async function isSignedIn() {
    const clerk = await getClerk();
    return !!clerk.user;
}

export async function getUserData() {
    const clerk = await getClerk();
    return clerk.user;
}
