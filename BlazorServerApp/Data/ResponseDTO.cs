using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Models
{
    public class ResponseDTO<T>
    {
        public List<T> resultList { get; set; }
        public int resultValue { get; set; }
    }
}
