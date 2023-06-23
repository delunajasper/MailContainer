using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Moq;
using Xunit;

namespace MailContainerTest.Tests;

public class MailTransferServiceTests
{
    private readonly Mock<IDataStore> _dataStore;
    private readonly Mock<IMailChecker> _mailChecker;
    private const string mailNumber = "111";
    
    public MailTransferServiceTests()
    {
        _dataStore = new Mock<IDataStore>();
        _mailChecker = new Mock<IMailChecker>();
    }

    [Fact]
    public void SmallParcelSuccess()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            MailType = MailType.SmallParcel,
            SourceMailContainerNumber = mailNumber
        };

        var mailContainer = new MailContainer
        {
            Status = MailContainerStatus.Operational,
            AllowedMailType = AllowedMailType.SmallParcel
        };

        _dataStore.Setup(d => d.GetMailContainer(request.SourceMailContainerNumber))
            .Returns(mailContainer);

        _mailChecker.Setup(m => m.IsValid(mailContainer, request)).Returns(true);
        
        var mailServiceTransfer = new MailTransferService(_dataStore.Object, _mailChecker.Object);
        
        // Act
        var result = mailServiceTransfer.MakeMailTransfer(request);
        
        // Assert
        Assert.True(result.Success);
        _dataStore.Verify(d => d.UpdateMailContainer(It.IsAny<MailContainer>()), Times.Once);
    }

    [Fact]
    public void LargeLetterSuccess()
    {
        
        // Arrange
        
        var request = new MakeMailTransferRequest
        {
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 4,
            SourceMailContainerNumber = mailNumber
        };

        var mailContainer = new MailContainer
        {
            AllowedMailType = AllowedMailType.LargeLetter,
            Capacity = 4
        };
        
        _dataStore.Setup(d => d.GetMailContainer(request.SourceMailContainerNumber))
            .Returns(mailContainer);
        _mailChecker.Setup(m => m.IsValid(mailContainer, request)).Returns(true);

        var mailServiceTransfer = new MailTransferService(_dataStore.Object, _mailChecker.Object);
        
        // Act
        var result = mailServiceTransfer.MakeMailTransfer(request);
        
        // Assert
        Assert.Equal(4, mailContainer.Capacity);
        Assert.True(result.Success);
        _dataStore.Verify(d => d.UpdateMailContainer(mailContainer), Times.Once);

    }

    [Fact]
    public void StandardLetterSuccess()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            MailType = MailType.StandardLetter,
            SourceMailContainerNumber = mailNumber
        };

        var mailContainer = new MailContainer
        {
            AllowedMailType = AllowedMailType.StandardLetter
        };
        
        _dataStore.Setup(d => d.GetMailContainer(request.SourceMailContainerNumber))
            .Returns(mailContainer);
        _mailChecker.Setup(m => m.IsValid(mailContainer, request)).Returns(true);

        var mailServiceTransfer = new MailTransferService(_dataStore.Object, _mailChecker.Object);
        
        // Act
        var result = mailServiceTransfer.MakeMailTransfer(request);
        
        // Assert
        Assert.True(result.Success);
        _dataStore.Verify(d => d.UpdateMailContainer(mailContainer), Times.Once);


    }
}