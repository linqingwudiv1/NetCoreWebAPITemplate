namespace BaseDLL.Singleton
{
    public class SingletonHelper
    {
        private static ISingletonInstance _instance;

        public static T Get<T>() where T : ISingletonInstance, new()
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return (T)_instance;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SingletonHelper<T> where T : ISingletonInstance, new()
    {
        private static T _instance;

        /// <summary>
        /// Get SingletonInstance 获取单例句柄
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get() 
        {
            if (_instance == null )
            {
                _instance = new T();
            }

            return _instance;
        }
    }
}