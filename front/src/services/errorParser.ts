import { STATUS_CODES } from '@enums/statusCodes.enum';
import { AxiosError } from 'axios';

export function handleError(err: unknown, handler: (error: any) => any) {
  if (err instanceof AxiosError && err.response?.data && STATUS_CODES.BAD_REQUEST === err.response.status) {
    // errors come from the server with different keys
    const { error } = err.response.data;
    const errorWithDescription = err.response?.data?.description;
    const errorDescription = err.response?.data?.error_description;
    const finalError = errorWithDescription || errorDescription || error;

    const errorText = typeof finalError === 'string' ? finalError : '';
    return handler(errorText);
  }
  throw err;
}
