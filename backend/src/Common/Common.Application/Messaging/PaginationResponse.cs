using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Messaging
{

    public class MetaData
    {

        public int Total { get; init; }
        public int Per_page { get; init; }
        public int Current_page { get; init; }
        public int Total_pages { get; init; }
        public int Total_per_page { get; init; }
    }
    public class PaginationResponse<T>
    {
        public PaginationResponse(ICollection<T> Data, int total, int per_page, int current_page)
        {
            this.Data = Data;
            MetaData = new MetaData()
            {
                Total = total,
                Per_page = per_page,
                Current_page = current_page,
                Total_pages = total / per_page + (total % per_page == 0 ? 0 : 1),
                Total_per_page = this.Data.Count
            };
        }
        public ICollection<T> Data { get; init; }
        public MetaData MetaData { get; init; }
    }
}
