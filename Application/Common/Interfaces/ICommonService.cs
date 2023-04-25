using AutoMapper;

namespace Application.Common.Interfaces
{
    public interface ICommonService
    {
        IMapper Mapper { get; }
        IDateTimeService DateTimeService { get; }
        //IDomainEventService DomainEventService { get; }
        //IWebsiteSettingsService WebsiteSettingsService { get; }
        IApplicationDbContext ApplicationDBContext { get; }
        //IOpenWeatherMapClient OpenWeatherMapClient { get; }
        //IEmailService EmailService { get; }
        ICacheService CacheService { get; }
        ICurrentUserService CurrentUserService { get; }
    }
}
