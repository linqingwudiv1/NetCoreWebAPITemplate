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
    public class AssetDomainEvent :
        IConsumer<DeleteAssetCommand>
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
        public AssetDomainEvent(  IAppInfoAccesser _accesser,IAssetHelper _assetHelper ) 
        {
            assetHelper = _assetHelper;
            accesser = _accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteAssetCommand> context)
        {
            string coskey = context.Message.coskey;
            this.assetHelper.Delete(GAssetVariable.Bucket, coskey);
        }

    }
}
