namespace BankAccountTransactions.Domain.Dto
{
    public class GetTransactionByDateDto
    {

        public string UserDocument { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public GetTransactionByDateDto()
        {
            
        }
        public GetTransactionByDateDto(string userDocument, DateTime initialDate, DateTime finalDate)
        {
            UserDocument = userDocument;
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}
