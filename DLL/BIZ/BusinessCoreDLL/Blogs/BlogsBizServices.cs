using AutoMapper;
using BusinessCoreDLL.DTOModel.API.Blogs;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Blogs
{
    /// <summary>
    /// 
    /// </summary>
    public class BlogsBizServices : IBlogsBizServices
    {
        /// <summary>
        /// DAO层
        /// </summary>
        protected IAccountAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper { get; set; }

        protected readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        public BlogsBizServices(IIDGenerator _IDGenerator,
                                IAccountAccesser AccountAccesser,
                                IMapper _mapper,
                                IPublishEndpoint _publishEndpoint)
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
            this.mapper = _mapper;
            this.publishEndpoint = _publishEndpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DTOAPIRes_BlogInfo> GetBlogInfo(long id)
        {
            DTOAPIRes_BlogInfo ret_data = new DTOAPIRes_BlogInfo();

            var account = this.accesser.Get(key: id).Item1;

            if (account == null) 
            {
                throw new Exception("用户不存在");
            }

            ret_data.userId = account.Id;
            ret_data.username = account.DisplayName;
            ret_data.email = account.Email;
            ret_data.avatar = account.Avatar;
            ret_data.joinTime = account.Q_CreateTime.ToString("yyyy-MM-dd");
            ret_data.introduction = account.Introduction;
            return ret_data;
        }
    }
}
