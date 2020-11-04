using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC_NEUVOO_INT_SERVICE.Models
{
    public class RequirementResponseDetails
    {
        public string message { get; set; }
        public Int64 code { get; set; }
        public string error { get; set; }
        public bool status { get; set; }

    }
    public class Data
    {
        public string jobCode { get; set; }
        public Int64 jobId { get; set; }
        public string jobTitle { get; set; }
        public string createdDate { get; set; }

    }

}
