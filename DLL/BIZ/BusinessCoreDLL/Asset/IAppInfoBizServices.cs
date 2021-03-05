using DBAccessCoreDLL.DTOModel.API.Asset;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppInfoBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<DTO_AppInfo> GetLatest(string appName);
    }
}
