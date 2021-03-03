using AdminServices.Command.Asset;
using BaseDLL;
using BaseDLL.Helper.Asset;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Entity.Asset;
using MassTransit;
using System.Threading.Tasks;
using System.Linq;

namespace AdminServices.Event.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfoDomainEvent :
        IConsumer<AddAppInfoCommand>,
        IConsumer<UpdateAppInfoCommand>,
        IConsumer<DeleteAppInfoCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IAssetHelper assetHelper;

        /// <summary>
        /// 
        /// </summary>
        readonly IAppInfoAccesser accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_assetHelper"></param>
        public AppInfoDomainEvent(  IAppInfoAccesser _accesser,IAssetHelper _assetHelper ) 
        {
            assetHelper = _assetHelper;
            accesser = _accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteAppInfoCommand> context)
        {
            DeleteAppInfoCommand msg = context.Message;
            AppInfo appInfo = this.accesser.Get(msg.id);

            if (appInfo != null)
            {

            }

            //
            string key = assetHelper.ConvertToKey(appInfo.Url);
            //
            assetHelper.Delete(GAssetVariable.Bucket, key);
            this.accesser.Delete(appInfo.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UpdateAppInfoCommand> context)
        {
            var msg = context.Message;

            var appInfo = accesser.Get(msg.id);

            if (msg.bLatest)
            {
                var apps = ( from 
                                x 
                             in 
                                 accesser.db.AppInfos 
                             where 
                                x.AppName ==  appInfo.AppName && 
                                x.bLatest == true &&
                                x.Id != msg.id 
                             select 
                                x).ToList();
                
                foreach (var item in apps) 
                {
                    item.bLatest = false;
                    this.accesser.db.AppInfos.Update(item);
                }
            }

            appInfo.bLatest         = msg.bLatest;
            appInfo.bForceUpdate    = msg.bForceUpdate;
            appInfo.bEnable         = msg.bEnable;
            appInfo.bBeta           = msg.bBeta;

            this.accesser.Update(appInfo);
            this.accesser.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddAppInfoCommand> context)
        {
            var msg = context.Message;

            var appinfo = new AppInfo
            {
                Id = msg.id,
                AppName = msg.appName,
                AppVersion = msg.appVersion,
                Url = msg.url,
                bEnable = msg.bEnable,
                bBeta = msg.bBeta,
                bForceUpdate = msg.bForceUpdate,
                bLatest = msg.bLatest
            };

            if (msg.bLatest)
            {
                var apps = ( from
                                x
                             in
                                 accesser.db.AppInfos
                             where
                                x.AppName == appinfo.AppName &&
                                x.bLatest == true &&
                                x.Id != msg.id
                             select
                                x ).ToList();

                foreach (var item in apps)
                {
                    item.bLatest = false;
                    this.accesser.db.AppInfos.Update(item);
                }
            }

            this.accesser.Add(appinfo);

        }
    }
}
