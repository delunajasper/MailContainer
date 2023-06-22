namespace MailContainerTest.Types;

public interface IMailChecker
{
    public bool IsValid(MailContainer? mailContainer, MakeMailTransferRequest request);
}