using System.Collections.Generic;

namespace Cw12.Services.Dtos
{
    public class PagedResponse<T>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<T> Items { get; set; }
    }
}