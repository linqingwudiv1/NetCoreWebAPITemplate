using AdminServices.Command.Asset;
using AutoMapper;
using BaseDLL.DTO;
using BaseDLL.Helper.Asset;
using BusinessAdminDLL.DTOModel.API.Asset;
using DBAccessBaseDLL.EF;
using DBAccessBaseDLL.IDGenerator;
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

        private readonly IIDGenerator IIDGenerator;

        private readonly IAssetHelper assetHelper;

        /// <summary>
        /// 
        /// </summary>
        public AppInfoBizServices(  IIDGenerator _IDGenerator,
                                    IPublishEndpoint _publishEndpoint,
                                    IAppInfoAccesser _accesser , 
                                    IAssetHelper     _assetHelper,
                                    IMapper _mapper) 
        {
            this.IIDGenerator = _IDGenerator;
            this.publishEndpoint = _publishEndpoint;
            this.accesser = _accesser;
            this.assetHelper = _assetHelper;
            this.mapper = _mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appinfo"></param>
        /// <returns></returns>
        public async Task<long> AddAppInfo(DTOAPI_AppInfo appinfo)
        {
            long newid = this.IIDGenerator.GetNewID<AppInfo>();

            var appInfos = (from 
                                x 
                            in 
                                this.accesser.db.AppInfos 
                            where 
                                x.AppName == appinfo.appName && x.AppVersion == appinfo.appVersion 
                            select x).FirstOrDefault();

            if ( appInfos != null ) 
            {
                throw new Exception("版本号已存在");
            }

            await this.publishEndpoint.Publish(new AddAppInfoCommand
            {
                id           = newid,
                appName      = appinfo.appName,
                appVersion   = appinfo.appVersion,
                bBeta        = appinfo.bBeta,
                bEnable      = appinfo.bEnable,
                bForceUpdate = appinfo.bForceUpdate,
                bLatest      = appinfo.bLatest,
                url          = appinfo.url
            });

            return newid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<DTO_PageableModel<DTOAPI_AppInfo>> getAppInfos(DTO_PageableQueryModel<DTOAPIReq_GetAppInfos> info)
        {
            DTO_PageableModel<DTOAPI_AppInfo> dto_model = new DTO_PageableModel<DTOAPI_AppInfo>();

            IQueryable<AppInfo> query = accesser.GetByAppName(info.data.appName);
            dto_model.data = query.OrderByDescending(x => x.Id).QueryPages(info.pageSize, info.pageNum).Select( x => mapper.Map <AppInfo, DTOAPI_AppInfo>( x )   ).ToArray();
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
                throw new  Exception("不存在");
            }

            await this.publishEndpoint.Publish(new DeleteAppInfoCommand 
            {
                id = id
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appinfo"></param>
        /// <returns></returns>
        public async Task UpdateAppInfo(DTOAPI_AppInfo appinfo)
        {
            var _appInfo = this.accesser.Get(appinfo.id);


            if (_appInfo == null)
            {
                throw new Exception("不存在");
            }

            if (_appInfo.AppVersion != appinfo.appVersion)
            {
                _appInfo = this.accesser.db.AppInfos.Where(x => x.AppName == appinfo.appName && x.AppVersion == appinfo.appVersion).FirstOrDefault();
                if (_appInfo != null) 
                {
                    throw new Exception("版本号已存在");
                }
            }

            await this.publishEndpoint.Publish( new UpdateAppInfoCommand
            {
                id           = appinfo.id ,
                bBeta        = appinfo.bBeta ,
                bEnable      = appinfo.bEnable ,
                bForceUpdate = appinfo.bForceUpdate ,
                bLatest      = appinfo.bLatest
            });

        }
    }
}
