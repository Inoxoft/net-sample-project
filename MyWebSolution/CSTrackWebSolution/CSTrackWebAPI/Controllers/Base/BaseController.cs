using AutoMapper;
using CSTrackWebAPI.Managers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CytometrixWebAPI.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;

        public BaseController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

    }
}
