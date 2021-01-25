using AdminServices.Command.Role;
using AutoMapper;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Entity;

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
            //CreateMap<Role, DTOAPI_Role>().ForMember(opt => opt.key, opt => opt.MapFrom(p => p.Id))
            //                              .ForMember(opt => opt.name, opt => opt.MapFrom(p => p.DisplayName))
            //                              .ReverseMap();
            CreateMap<DTOAPI_Role, AddRoleCommand>();
        }
    }

}
