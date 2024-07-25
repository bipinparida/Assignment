using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVOffice.Data.DTO.Mappings
{
    public class FacultyBatchClassMappingDTO
    {
        public int? FacultyBatchClassMappingId { get; set; }
        public int FacultyId { get; set; }
        public int BatchId { get; set; }
        public List<int> ClassId { get; set; }

        public Int64 CreatedBy { get; set; }
    }
}
