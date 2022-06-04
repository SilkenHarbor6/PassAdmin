using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassAdmin.Model
{
    public class Response
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
    }
}
