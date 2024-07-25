using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVOffice.Data.DTO.Mappings
{
    public class StudentBatchClassMappingDTO
    {
        public int? StudentBatchClassMappingId { get; set; }
        public int StudentId { get; set; }
        public int BatchId { get; set; }
        public List<int> ClassId { get; set; }
        public Int64 CreatedBy { get; set; }

    }
}
