using System.Security.AccessControl;

namespace MailContainerTest.Types;

public class MailChecker : IMailChecker
{
    public bool IsValid(MailContainer? mailContainer, MakeMailTransferRequest? request)
    {
        return request != null && mailContainer != null && request.MailType 
            switch
        {
            MailType.StandardLetter => StandardLetter(mailContainer),
            MailType.LargeLetter => LargeLetter(mailContainer, request.NumberOfMailItems),
            MailType.SmallParcel => SmallParcel(mailContainer),
            _ => false
        };
    }

    private bool SmallParcel(MailContainer mailContainer)
    {
        return mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) &&
               mailContainer.Status == MailContainerStatus.Operational;
    }

    private static bool LargeLetter(MailContainer mailContainer, int mailCount)
    {
        return mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) && mailCount < mailContainer.Capacity;
    }

    private static bool StandardLetter(MailContainer mailContainer)
    {
        return mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) &&
               mailContainer.Status == MailContainerStatus.Operational;
    }
}