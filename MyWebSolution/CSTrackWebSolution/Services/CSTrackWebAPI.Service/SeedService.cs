using AutoMapper;
using CSTrackWebAPI.Managers.Abstraction;
using CSTrackWebAPI.Service.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Service
{
    public class SeedService : BaseService, ISeedService
    {
        public SeedService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        #region Methods
        public async Task SeedAsync()
        {
            await SeedScriptsAsync();
        }

   

        private async Task SeedScriptsAsync()
        {
            string[] filesToExcecute = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SeedScripts"));
            foreach (var file in filesToExcecute)
            {
                if (File.Exists(file))
                {
                    var sql = File.ReadAllText(file);
                    var batches = sql.Split("GO");
                    foreach (var sqlBatch in batches)
                    {
                        await _unitOfWork.ExecuteSqlCommandAsync(new RawSqlString(sqlBatch));
                    }
                }
            }
        }
        #endregion

    }
}
