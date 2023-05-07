using AutoMapper;

namespace Application.Common.Interfaces
{
    public interface ICommonService
    {
        IMapper Mapper { get; }
        IDateTimeService DateTimeService { get; }
        IApplicationDbContext ApplicationDBContext { get; }
        ICacheService CacheService { get; }
        ICurrentUserService CurrentUserService { get; }
        IEmailService EmailService { get; }
    }
}
