using Microsoft.Graph.Models;

namespace GraphAPI
{
    public interface IGraphApiService
    {
        Task<Stream> ConvertDocumentToPDF(Stream fileStream, string fileExtension);
        Task<Message> GetMessage(string messageId, bool markAsRead, bool reducePayload = true);
        Task<List<Message>> GetMessages(string mailFolderId, string? searchString = null, bool includeAttachments = false, AttachmentData attachmentData = AttachmentData.None);
        Task<List<Message>> GetMessageAttachmentsContentData(List<Message> messages, AttachmentData attachmentData = AttachmentData.All);
        Task MarkAsRead(string messageId);
        Task<bool> HasUnreadMail(string mailFolderId);
        Task<Attachment> GetAttachment(string messageId, string attachmentId);
        Task<string> GetMailboxName();
        Task<string> MoveMessage(string mailFolderId, string messageId);
        Task<string> SendMessage(string subject, string body, string to, string cc, string bcc);
        Task<string> SendMessage(Message message, long? mailboxId = null);
        Task<Message> SaveDraft(Message message);
        Task<List<Message>> GetDraftMessages(int? claimId, bool includeAttachments = false, AttachmentData attachmentData = AttachmentData.All);
        Task DeleteAttachment(string messageId, string attachmentId);
    }
}
