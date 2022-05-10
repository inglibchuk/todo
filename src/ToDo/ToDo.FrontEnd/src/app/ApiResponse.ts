export interface ApiResponse<T> {
  Errors: string[];
  Payload: T;
}