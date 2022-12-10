import { Pagination } from "./pagination.model";

export class PaginatedResult<T> {
    result: T | undefined;
    pagination: Pagination | undefined;
  }
