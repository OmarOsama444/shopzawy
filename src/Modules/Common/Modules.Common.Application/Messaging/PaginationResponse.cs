using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Application.Messaging
{

    public class MetaData
    {

        public int total { get; init; }
        public int per_page { get; init; }
        public int current_page { get; init; }
        public int total_pages { get; init; }
        public int total_per_page { get; init; }
    }
    public class PaginationResponse<T>
    {
        public PaginationResponse(ICollection<T> Data, int total, int per_page, int current_page)
        {
            this.Data = Data;
            this.MetaData = new MetaData()
            {
                total = total,
                per_page = per_page,
                current_page = current_page,
                total_pages = (total / per_page) + (((total % per_page) == 0) ? 0 : 1),
                total_per_page = this.Data.Count
            };
        }
        public ICollection<T> Data { get; init; }
        public MetaData MetaData { get; init; }
    }
}
