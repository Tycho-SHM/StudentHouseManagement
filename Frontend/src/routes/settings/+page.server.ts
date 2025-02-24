import type { PageServerLoad} from "./$types";

export const load: PageServerLoad = async ({ fetch, params, url}) => {
    const response = await fetch('https://localhost:7204/notifications/settings');

    const data = await response.json();

    const stringArray: string[] = data;

    return { stringArray };
}