using AutoMapper;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.DTOModel.API.Asset;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Entity.Asset;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessCoreDLL.AutoMapper
{

    /// <summary>
    /// 
    /// </summary>
    public class BizCoreProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public BizCoreProfile() 
        {
            //
            //
            //
             
            #region
            
            #endregion

            #region Account

            CreateMap<Account, DTOAPIRes_UserInfo>().ForMember(opt => opt.phone, opt => opt.MapFrom(p => string.IsNullOrEmpty(p.PhoneAreaCode) ? p.PhoneAreaCode + "-" + p.Phone : ""))
                                                    .ForMember(opt => opt.name, opt => opt.MapFrom(p => p.DisplayName))
                                                    .ReverseMap();
            #endregion

            #region Asset

            CreateMap<AppInfo, DTO_AppInfo>().ForMember(opt => opt.createTime, opt => opt.MapFrom(p => p.Q_CreateTime))
                                             .ForMember(opt => opt.updateTime, opt => opt.MapFrom(p => p.Q_UpdateTime))
                                             .ReverseMap();

            #endregion
        }
    }

}
