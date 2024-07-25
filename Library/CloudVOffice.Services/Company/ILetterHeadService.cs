using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Company;
using CloudVOffice.Data.DTO.Company;

namespace CloudVOffice.Services.Company
{
    public interface ILetterHeadService
    {
        public MessageEnum LetterHeadCreate(LetterHeadDTO letterHeadDTO);
        public LetterHead GetLetterHeadByLetterHeadId(int letterHeadId);
        public List<LetterHead> GetLetterHeads();
        public LetterHead GetLetter();
        public MessageEnum LetterHeadUpdate(LetterHeadDTO letterHeadDTO);
        public MessageEnum LetterHeadDelete(int letterHeadId, int DeletedBy);
    }
}
