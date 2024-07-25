using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVOffice.Data.DTO
{
    public class ForgetPasswordDTO
    {
        public string PhoneNo { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
