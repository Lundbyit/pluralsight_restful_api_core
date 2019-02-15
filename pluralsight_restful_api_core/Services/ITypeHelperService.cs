using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Services
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
