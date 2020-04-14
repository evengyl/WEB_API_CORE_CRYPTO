using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelsDAL = DAL.Models;
using ModelsAPI = WEBAPI_DotNetCore.Models;

namespace WEBAPI_DotNetCore.Mappers
{
    public static class Mappers
    {
        public static ModelsAPI.LoginTodoInfos ToAPI(this ModelsDAL.UserTodo LocalToAPI)
        {
            return new ModelsAPI.LoginTodoInfos
            {
                Id = LocalToAPI.Id,
                Login = LocalToAPI.Login,
                Name = LocalToAPI.Name,
                LastName = LocalToAPI.LastName
            };
        }
    }
}
