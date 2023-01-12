using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace API.Extensions;

public static class HttpExtensions
{
    private const string PaginationHeaderName = "Pagination";
    
    public static void AddPaginationHeader(this HttpResponse response, int currentPage,
        int itemsPerPage, int totalItems, int totalPages)
    {
        var paginationHeader = new
        {
            currentPage,
            itemsPerPage,
            totalItems,
            totalPages
        };
        response.Headers.Add(PaginationHeaderName, JsonSerializer.Serialize(paginationHeader));
        response.Headers.Add("Access-Control-Expose-Headers", PaginationHeaderName);
    }
}