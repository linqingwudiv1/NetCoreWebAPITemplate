using AdminServices.Command.Account;
using AdminServices.Command.PageRouteRole;
using AdminServices.Command.Role;
using AutoMapper;
using BusinessAdminDLL.DTOModel.API.Asset;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using BusinessAdminDLL.DTOModel.API.Users;
using DBAccessCoreDLL.DTOModel.API.Asset;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Entity.Asset;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessAdminDLL.AutoMapper
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
            //
            //
            //

            #region
            
            #endregion

            #region Role
            
            CreateMap<Role, DTOAPI_Role>().ForMember(opt => opt.key, opt => opt.MapFrom(p => p.Id))
                                          .ForMember(opt => opt.name, opt => opt.MapFrom(p => p.RoleName))
                                          .ForMember(opt => opt.routes, opt => opt.Ignore() )
                                          .ReverseMap();

            CreateMap<DTOAPI_Role, AddRoleCommand>();

            #endregion


            #region PageRoute
            CreateMap<RoutePages, DTOAPI_RoutePages>()
                .ForMember(opt=> opt.name, opt => opt.MapFrom( p => p.RouteName ) )
                .ForMember(opt => opt.meta, opt => 
                {
                    opt.MapFrom(p => new DTOAPI_RoutePagesMeta
                    {
                        activeMenu = p.ActiveMenu,
                        alwaysShow = p.AlwaysShow,
                        affix = p.Affix,
                        hidden = p.Hidden,
                        icon = p.Icon,
                        noCache = p.NoCache,
                        title = p.Title
                    });
                }) 
                .ReverseMap() ;

            CreateMap<DTOAPI_RoutePages, DTOIn_PageRoute>().ForMember(opt => opt.RouteName, opt => opt.MapFrom( p => p.name ?? "") )
                                                           .ForMember(opt => opt.Path, opt => opt.MapFrom( p => p.path ?? "") )
                                                           .ForMember(opt => opt.Redirect, opt => opt.MapFrom(p => p.redirect ?? ""))
                                                           .ForMember(opt => opt.Component, opt => opt.MapFrom(p => p.component ?? ""))
                                                           .ForMember(opt => opt.Platform, opt => opt.MapFrom(p => p.platform ?? ""))
                                                           .ForMember(opt => opt.GroupName, opt => opt.MapFrom(p => p.groupName ?? ""))
                                                           .ForMember(opt => opt.HierarchyPath, opt => opt.MapFrom(p => p.hierarchyPath ?? "") )
                                                           .ForMember(opt => opt.ActiveMenu, opt => opt.MapFrom(p => p.meta.activeMenu ?? ""))
                                                           .ForMember(opt => opt.Affix, opt => opt.MapFrom(p => p.meta.affix))
                                                           .ForMember(opt => opt.AlwaysShow, opt => opt.MapFrom(p => p.meta.alwaysShow))
                                                           .ForMember(opt => opt.Icon, opt => opt.MapFrom(p => p.meta.icon ?? ""))
                                                           .ForMember(opt => opt.Hidden, opt => opt.MapFrom(p => p.meta.hidden))
                                                           .ForMember(opt => opt.NoCache, opt => opt.MapFrom(p => p.meta.noCache))
                                                           .ForMember(opt => opt.Title , opt => opt.MapFrom(p => p.meta.title ?? "")); 


            #endregion

            #region Account

            CreateMap<Account, DTOAPIRes_UserInfo>().ForMember(opt => opt.phone, opt => opt.MapFrom(p => !string.IsNullOrEmpty(p.PhoneAreaCode) ? p.PhoneAreaCode + "-" + p.Phone : ""))
                                                    .ForMember(opt => opt.name, opt => opt.MapFrom(p => p.DisplayName))
                                                    .ForMember(opt => opt.roles, opt => opt.Ignore())
                                                    .ForMember(opt => opt.createTime, opt => opt.MapFrom( p => p.Q_CreateTime ) )
                                                    .ForMember(opt => opt.createTime, opt => opt.MapFrom(p => p.Q_UpdateTime ) )
                                                    .ReverseMap();
            #endregion


            #region Asset

            CreateMap<AppInfo, DTOAPI_AppInfo>()
                .ForMember(opt => opt.createTime, opt => opt.MapFrom(p => p.Q_CreateTime) )
                .ForMember(opt => opt.updateTime, opt => opt.MapFrom(p => p.Q_UpdateTime) )
                .ReverseMap();

            CreateMap<DTOAPI_AppInfo, DTO_AppInfo>().ReverseMap();

            #endregion
        }
    }

}
