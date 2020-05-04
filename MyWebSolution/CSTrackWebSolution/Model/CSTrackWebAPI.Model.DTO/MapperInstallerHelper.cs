using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTrackWebAPI.Model.DTO
{
    public static class MapperInstallerHelper
    {
        private static readonly Type[] _baseProfileTypes = new Type[] { typeof(IProfileBase) };

        public static IEnumerable<Type> GetTypes()
        {
            var profileTypes =
                _baseProfileTypes.SelectMany(baseType =>
                baseType.Assembly.GetTypes()
                    .Where(type => type.GetInterfaces().Contains(baseType) && !type.IsAbstract));

            return profileTypes;
        }
    }
}
