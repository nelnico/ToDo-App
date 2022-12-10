export class TodoSearchParams {
    pageNumber = 1;
    pageSize = 12;
    orderBy = 'name';
    orderDirection = 'asc';

    query: string;
    statusIds: number[] = [];
}
