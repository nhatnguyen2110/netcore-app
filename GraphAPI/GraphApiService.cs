using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.CallRecords;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Kiota.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveUpload = Microsoft.Graph.Drives.Item.Items.Item.CreateUploadSession;

namespace GraphAPI
{
    
    public class GraphApiService : IGraphApiService
    {
        private GraphServiceClient _graphServiceClient;
        private GraphAuthDto _authDto;
        public GraphApiService(GraphAuthDto authDto)
        {
            this._authDto = authDto;
            var clientSecretCredential = new ClientSecretCredential(_authDto.tenantId, _authDto.clientId, _authDto.clientSecret);
            _graphServiceClient = new GraphServiceClient(clientSecretCredential, new[] { $"{_authDto.resource}/.default" });
        }
        
        public Task<Stream> ConvertDocumentToPDF(Stream fileStream, string fileExtension)
        {
            throw new NotImplementedException();
            //var uploadSessionRequestBody = new DriveUpload.CreateUploadSessionPostRequestBody
            //{
            //    Item = new DriveItemUploadableProperties
            //    {
            //        AdditionalData = new Dictionary<string, object>
            //        {
            //            { "@microsoft.graph.conflictBehavior", "replace" },
            //        },
            //    },
            //};
            //var driveItem = await _graphServiceClient.Drives[_authDto.userId].GetAsync();
            //// Upload file to onedrive cache folder using the large file upload
            //fileExtension = fileExtension.StartsWith(".") ? fileExtension : $".{fileExtension}";
            //string filePath = $"DocumentConversionCache\\{Guid.NewGuid()}{fileExtension}";
            //var uploadSession = await _graphServiceClient.Drives[driveItem?.Id].Root
            //    .ItemWithPath(filePath)
            //    .CreateUploadSession
            //    .PostAsync(uploadSessionRequestBody);

            //// Max slice size must be a multiple of 320 KiB
            //int maxSliceSize = 320 * 1024;
            //var fileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, fileStream, maxSliceSize,_graphServiceClient.RequestAdapter);
            //var uploadResult = await fileUploadTask.UploadAsync();

            //if (!uploadResult.UploadSucceeded)
            //{
            //    return null;
            //}

            //// Uploaded

            //try
            //{
            //    // Download file as new format
            //    var newFormatFile = _graphServiceClient.Drives[driveItem?.Id].Root
            //        .ItemWithPath(filePath)
            //        .Content.GetAsync()

            //    newFormatFile.QueryOptions.Add(new QueryOption("format", "pdf"));
            //    return await newFormatFile.GetAsync() as MemoryStream;

            //}
            //finally
            //{
            //    // Always delete uploaded file
            //    await _graphServiceClient.Users[_claimsUserId]
            //        .Drive
            //        .Root
            //       .ItemWithPath(filePath)
            //        .Request()
            //        .DeleteAsync();
            //}
        }

        public Task DeleteAttachment(string messageId, string attachmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> GetAttachment(string messageId, string attachmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetDraftMessages(int? claimId, bool includeAttachments = false, AttachmentData attachmentData = AttachmentData.All)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMailboxName()
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetMessage(string messageId, bool markAsRead, bool reducePayload = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessageAttachmentsContentData(List<Message> messages, AttachmentData attachmentData = AttachmentData.All)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessages(string mailFolderId, string? searchString = null, bool includeAttachments = false, AttachmentData attachmentData = AttachmentData.None)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasUnreadMail(string mailFolderId)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsRead(string messageId)
        {
            throw new NotImplementedException();
        }

        public Task<string> MoveMessage(string mailFolderId, string messageId)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SaveDraft(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<string> SendMessage(string subject, string body, string to, string cc, string bcc)
        {
            throw new NotImplementedException();
        }

        public Task<string> SendMessage(Message message, long? mailboxId = null)
        {
            throw new NotImplementedException();
        }
    }
}
