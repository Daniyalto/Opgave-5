using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_5
{
    public class Response
    {
        public string? SelectedMethod { get; set; }
        public string? Result { get; set; }

        public Response(string selectedMethod, string result)
        {
            SelectedMethod = selectedMethod;
            Result = result;
        }
    }
}
