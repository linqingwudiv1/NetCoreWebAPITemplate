using AdminServices.Command.Asset;
using BaseDLL;
using BaseDLL.Helper.Asset;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Entity.Asset;
using MassTransit;
using System.Threading.Tasks;

namespace AdminServices.Event.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfoDomainEvent :
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
            string key = assetHelper.ConvertToKey(appInfo.url);
            //
            assetHelper.Delete(GAssetVariable.Bucket, key);

            //assetHelper.Delete();
            //assetHelper.Delete();
            //throw new NotImplementedException();
        }

    }
}
