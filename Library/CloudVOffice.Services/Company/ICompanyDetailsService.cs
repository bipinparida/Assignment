using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Company;
using CloudVOffice.Data.DTO.Company;

namespace CloudVOffice.Services.Company
{
    public interface ICompanyDetailsService
    {
        public MessageEnum CompanyDetailsCreate(CompanyDetailsDTO companyDetailsDTO);
        public CompanyDetails GetCompanyDetailsByCompanyDetailsId(int companyDetailsId);
        public List<CompanyDetails> GetCompanyDetailsList();
        public CompanyDetails GetCompanyDetails();
        public MessageEnum CompanyDetailsUpdate(CompanyDetailsDTO companyDetailsDTO);
        public MessageEnum CompanyDetailsDelete(int companyDetailsId, int DeletedBy);
    }
}
