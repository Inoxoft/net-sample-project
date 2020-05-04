using AutoMapper;
using CSTrackWebAPI.Managers.Abstraction;
using System;

namespace CSTrackWebAPI.Service
{
    public class BaseService : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
