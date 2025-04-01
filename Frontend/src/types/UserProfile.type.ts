export type UserProfile = {
  id: string;
  imgUrl?: string | null;
  userId?: string | null;
  lastUpdatedDateTimeUtc: string;
  createdDateTimeUtc: string;
  displayName?: string | null;
  houses: Array<any>; // Replace 'any' with the actual type if available
};
