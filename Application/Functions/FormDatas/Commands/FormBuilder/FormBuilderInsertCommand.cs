using Application.Common.DBSupports;
using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Functions.FormDatas.Commands.FormBuilder
{
    public class FormBuilderInsertCommand : IRequest<Response<string>>
    {
        public string? TableName { get; set; }
        public Dictionary<string, string>? Fields { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class FormBuilderInsertCommandHandler : BaseHandler<FormBuilderInsertCommand, Response<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        public FormBuilderInsertCommandHandler(ICommonService commonService,
            ICurrentUserService currentUserService,
            IIdentityService identityService,
            IConfiguration configuration,
            ILogger<FormBuilderInsertCommand> logger) : base(commonService, logger)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
            _configuration = configuration;
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async override Task<Response<string>> Handle(FormBuilderInsertCommand request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            try
            {
                var formFields = await _commonService.ApplicationDBContext.FormDatas.AsNoTracking().Where(x => !x.Deleted && x.DBTable == request.TableName).ToListAsync();
#pragma warning disable CS8604 // Possible null reference argument.
                var userId = _currentUserService.UserId;
                var ado = new ADOBuilder(_commonService.ApplicationDBContext, request.TableName, userId, _configuration);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                foreach (var field in request.Fields)
                {
                    // check role permission
                    var formField = formFields.FirstOrDefault(x => x.DBColumn == field.Key);
                    if (formField != null)
                    {
                        if (string.IsNullOrEmpty(formField.PermissionRoles)
                            || await this.IsUserInRoles(userId, formField.PermissionRoles))
                        {
                            ado.AddParam(field.Key, field.Value);
                        }
                    }
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                var result = ado.ExecuteInsert();
                return Response<string>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert. Request: {Name} {@Request}", typeof(FormBuilderInsertCommand).Name, request);
                return new Response<string>(false, ex.Message, ex.Message, "Failed to insert", request.requestId);
            }
        }
        private async Task<bool> IsUserInRoles(string userId, string strRoles)
        {
            var roles = strRoles.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in roles)
            {
                if (await _identityService.IsInRoleAsync(userId, role))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
