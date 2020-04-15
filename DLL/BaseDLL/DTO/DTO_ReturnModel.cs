namespace BaseDLL.DTO
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DTO_ReturnModel<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public DTO_ReturnModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_code"></param>
        /// <param name="_state"></param>
        /// <param name="_desc"></param>
        public DTO_ReturnModel(T _data,int _code = 20000, int _state = 20000, string _desc = "")
        {
            data    = _data;
            code    = _code;
            state   = _state;
            desc    = _desc;
        }

        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string desc { get; set; }
    }
}
