using BaseDLL;
using Microsoft.Extensions.Configuration;

namespace DBAccessBaseDLL.Static
{
    /// <summary>
    ///
    /// </summary>
    static public class GConnStrings
    {
        /// <summary>
        /// 
        /// </summary>
        static public string IDGeneratorDBConn
        {
            get
            {
                return GVariable.configuration.GetConnectionString("IDGeneratorDB");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public string PostgreSQLCoreDBConn
        {
            get
            {

                return GVariable.configuration.GetConnectionString("PostgreSQLCoreDB");
            }
        }
    }
}
