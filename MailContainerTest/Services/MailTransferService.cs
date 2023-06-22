using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private readonly IMailChecker _checker;
        private readonly IDataStore _dataStore;
        public MailTransferService(IDataStore dataStore, IMailChecker checker)
        {
            _dataStore = dataStore;
            _checker = checker;
        }
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var result = new MakeMailTransferResult
            {
                Success = false,
            };

            try
            {
                var mailDataStore = _dataStore.GetMailContainer(request.SourceMailContainerNumber);
                if (_checker.IsValid(mailDataStore, request))
                {
                    _dataStore.UpdateMailContainer(mailDataStore);
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return result;
        }
    }
}
