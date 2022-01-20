using AutoMapper;
using BaseDLL.Helper.Asset;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.DTOModel.API.Asset;
using MassTransit;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Asset
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
        /// <returns></returns>
        public async Task<DTO_AppInfo> GetLatest(string appName)
        {
            
            var data = (from 
                            x 
                        in 
                            this.accesser.db.AppInfos 
                        where 
                            x.AppName == appName && x.bEnable && x.bLatest 
                        orderby 
                            x.Id descending 
                        select 
                            mapper.Map<DTO_AppInfo>(x) ).FirstOrDefault();
            return await Task.FromResult(data);

            //throw new NotImplementedException();
        }
    }
}
