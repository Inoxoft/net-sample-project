using AutoMapper;
using CSTrackWebAPI.Managers.Abstraction;

namespace CSTrackWebAPI.Managers
{
    public class ServiceManager : IServiceManager
    {
        

        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IMapper _mapper;
       

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
          
        }

        //public IAuthService AuthService
        //{
        //    get
        //    {
        //        _authService = _authService ?? new AuthService(_unitOfWork, _mapper, EmailService);
        //        return _authService;
        //    }
        //}

      


        public void Dispose()
        {
            //_authService?.Dispose();
           
        }

    }
}
