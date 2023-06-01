using Application.Common.Interfaces;
using Domain.Entities.Webhook;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Application.Functions.WebHooks.Internals
{
    public class ProcessWebHook: IRequest<Unit>
    {
        public Guid WebHookId { get; set; }
        public dynamic? PayLoad { get; set; }
        public HookEventType HookEventType { get; set; }
    }
    public class ProcessWebHookHandler : IRequestHandler<ProcessWebHook, Unit>
    {
        protected readonly ICommonService _commonService;
        protected readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public ProcessWebHookHandler(
            ICommonService commonService,
            IHttpClientFactory clientFactory,
            ILogger<EventNotify> logger
            ) {
            _logger = logger;
            _commonService = commonService;
            _clientFactory = clientFactory;
        }

        public async Task<Unit> Handle(ProcessWebHook request, CancellationToken cancellationToken)
        {
            var record = new WebHookRecord()
            {
                WebHookID = request.WebHookId,
                HookEventType= request.HookEventType,
                RunAtUTC = DateTimeOffset.UtcNow,
            };
            try
            {
                WebHook hook;

                try
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    hook = await _commonService.ApplicationDBContext.WebHooks
                        .Where(e => e.Id == request.WebHookId)
                        .FirstOrDefaultAsync(cancellationToken);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                }
                catch (Exception ex)
                {
                    record.Result = RecordResult.dataQueryError;
                    record.Exception = ex.ToString();

                    return Unit.Value;
                }
                if (hook != null)
                {
                    hook.LastTriggerAtUTC = DateTimeOffset.UtcNow;
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IncludeFields = true,
                    };

                    var serialised_request_body = new StringContent(
                          JsonSerializer.Serialize<dynamic>(request.PayLoad, options),
                          Encoding.UTF8,
                          "application/json");


                    var httpClient = _clientFactory.CreateClient();

                    /* Set Headers */
                    httpClient.DefaultRequestHeaders.Add("X-Trouble-Delivery", record.Id.ToString());

                    if (!string.IsNullOrWhiteSpace(hook.Secret))
                    {
                        httpClient.DefaultRequestHeaders.Add("X-Trouble-Secret", hook.Secret);
                    }

                    httpClient.DefaultRequestHeaders.Add("X-Trouble-Event", request.HookEventType.ToString().ToLowerInvariant());

                    record.RequestBody = await serialised_request_body.ReadAsStringAsync(cancellationToken);

                    var serialized_headers = new StringContent(
                                     JsonSerializer.Serialize(httpClient.DefaultRequestHeaders.ToList(), options),
                                     Encoding.UTF8,
                                     "application/json");

                    record.RequestHeaders = await serialized_headers.ReadAsStringAsync(cancellationToken);

                    if (!string.IsNullOrWhiteSpace(hook.WebHookUrl))
                    {
                        try
                        {
                            using var httpResponse = await httpClient.PostAsync(hook.WebHookUrl, serialised_request_body);

                            if (httpResponse != null)
                            {
                                record.StatusCode = $"{(int)httpResponse.StatusCode} - {httpResponse.StatusCode}";

                                if (httpResponse.Content != null)
                                {
                                    record.ResponseBody = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                                }
                            }

                            record.Result = RecordResult.ok;
                        }
                        catch (Exception ex)
                        {
                            record.Result = RecordResult.http_error;
                            record.Exception = ex.ToString();

                            //throw ex;
                        }
                    }
                    else
                    {
                        record.Result = RecordResult.parameter_error;
                    }
                }
                else
                {
                    record.Result = RecordResult.parameter_error;
                }
            }
            finally
            {
                try
                {
                    _commonService.ApplicationDBContext.WebHookRecords.Add(record);
                    await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                }
                catch(Exception ex) {
                    _logger.LogError(ex, "{Name} {@Request}", typeof(ProcessWebHook).Name, request);
                }
            }
            return Unit.Value;
        }
    }
 }
