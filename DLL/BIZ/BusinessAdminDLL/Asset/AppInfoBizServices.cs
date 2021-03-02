using AdminServices.Command.Asset;
using AutoMapper;
using BaseDLL.DTO;
using BusinessAdminDLL.DTOModel.API.Asset;
using DBAccessBaseDLL.EF;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Entity.Asset;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfoBizServices : IAppInfoBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IAppInfoAccesser accesser;

        private readonly IMapper mapper;

        private readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        public AppInfoBizServices( IPublishEndpoint _publishEndpoint,
                                    IAppInfoAccesser _accesser , 
                                    IMapper _mapper) 
        {
            this.publishEndpoint = _publishEndpoint;
            this.accesser = _accesser;
            this.mapper = _mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<DTO_PageableModel<DTOAPI_AppInfo>> getAppInfos(DTO_PageableQueryModel<DTOAPIReq_GetAppInfos> info)
        {
            var dto_model = new DTO_PageableModel<DTOAPI_AppInfo>();

            IQueryable<AppInfo> query = accesser.GetByAppName(info.data.appName);
            dto_model.data = query.QueryPages(info.pageSize, info.pageNum).Select( x => mapper.Map <AppInfo, DTOAPI_AppInfo>( x )   ).ToArray();
            dto_model.total = query.LongCount();
            dto_model.pageNum = info.pageNum;

            return dto_model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAppInfo(long id)
        {
            AppInfo appinfo = this.accesser.Get(id);

            if (appinfo == null) 
            {
                throw new  Exception("密码错误");
            }

            await this.publishEndpoint.Publish(new DeleteAppInfoCommand 
            {
                id = id
            });
        }
    }
}
