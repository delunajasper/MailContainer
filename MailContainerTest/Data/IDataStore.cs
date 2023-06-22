using MailContainerTest.Types;

namespace MailContainerTest.Data;

public interface IDataStore
{
    public MailContainer GetMailContainer(string mailContainerNumber);
    public void UpdateMailContainer(MailContainer mailContainer);
}