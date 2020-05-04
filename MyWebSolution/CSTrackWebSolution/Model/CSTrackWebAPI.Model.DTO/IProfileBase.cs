using AutoMapper;

namespace CSTrackWebAPI.Model.DTO
{
    public interface IProfileBase
    {
        IProfileExpression Configure(IProfileExpression config);
    }
}
