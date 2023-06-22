using System.Security.AccessControl;

namespace MailContainerTest.Types;

public class MailChecker : IMailChecker
{
    public bool IsValid(MailContainer? mailContainer, MakeMailTransferRequest? request)
    {
        if (mailContainer is null)
        {
            return false;
        }

        if (request is null) return false;
        switch (request.MailType)
        {
            case MailType.StandardLetter:
                return StandardLetter(mailContainer);
                    
            case MailType.LargeLetter:
                return LargeLetter(mailContainer, request.NumberOfMailItems);
                    
            case MailType.SmallParcel:
                return SmallParcel(mailContainer);
        }

        return false;
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