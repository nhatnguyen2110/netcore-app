﻿using Application.Common.Interfaces;
using AutoMapper;

namespace Infrastructure.Services
{
    public class CommonService : ICommonService
    {
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IDateTimeService _dateTimeService;
        protected readonly IApplicationDbContext _applicationDbContext;
        protected readonly ICacheService _cacheService;
        protected readonly IEmailService _emailService;
        public CommonService(
        IMapper mapper
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        , IApplicationDbContext applicationDbContext
        , ICacheService cacheService
        , IEmailService emailService
        )
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
            _applicationDbContext = applicationDbContext;
            _cacheService = cacheService;
            _emailService = emailService;
        }
        public IMapper Mapper => _mapper;

        public IApplicationDbContext ApplicationDBContext => _applicationDbContext;

        public ICacheService CacheService => _cacheService;

        public ICurrentUserService CurrentUserService => _currentUserService;

        public IDateTimeService DateTimeService => _dateTimeService;

        public IEmailService EmailService => _emailService;
    }
}
