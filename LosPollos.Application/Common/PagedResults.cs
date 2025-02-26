using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Common
{
    public class PagedResults<T>
    {
        public PagedResults(IEnumerable<T>items,int TotalCount,int pageSize,int pageNumber)
        {
            Items = items;      
            this.TotalCount= TotalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            FromItem = pageSize * (pageNumber - 1) + 1;
            ToItem = FromItem + pageSize - 1;

        }
        public IEnumerable<T> Items { get; set; }   
        
        public int TotalCount {  get; set; }
        public int TotalPages { get; set; }
        public int FromItem {  get; set; }      
        public int ToItem { get; set; } 

    }
}
