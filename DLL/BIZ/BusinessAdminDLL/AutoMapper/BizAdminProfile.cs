using AutoMapper;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Entity;

using RoutePage_Alias = DBAccessCoreDLL.EFORM.Entity.RoutePages;
namespace BusinessAdminDLL.DTOModel.AutoMapper
{

    /// <summary>
    /// 
    /// </summary>
    public class BizAdminProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public BizAdminProfile() 
        {
            CreateMap<Role, DTOAPI_Role>()                  ;
            CreateMap<RoutePage_Alias, DTOAPI_RoutePages>() ;
        }
    }

}
