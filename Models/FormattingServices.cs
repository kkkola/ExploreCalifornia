using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCaliforniaNow.Models
{
    public class FormattingServices
    {
        public string ReadableDate(DateTime dt)
        {
            return dt.ToString("d");
        }
    }
}
