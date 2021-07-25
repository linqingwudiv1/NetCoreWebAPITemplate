using AutoMapper;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Forum;
using MassTransit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Forum
{
    /// <summary>
    /// 改0
    /// </summary>
    public class ForumBizServices : IForumBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IForumModuleAccesser Accesser;

        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_Accesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        public ForumBizServices(IIDGenerator _IDGenerator,
                                IForumModuleAccesser _Accesser,
                                IMapper _mapper,
                                IPublishEndpoint _publishEndpoint) 
        {
            this.IDGenerator = _IDGenerator;
            this.mapper = _mapper;
            this.publishEndpoint = _publishEndpoint;
            this.Accesser = _Accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetForumPortalData()
        {
            var modules =  ( from x in this.Accesser.db.ForumModules select x ).ToArray();
            var topics =   ( from x in this.Accesser.db.ForumTopics  select x ).ToArray();
            var hotPosts = ( from x in this.Accesser.db.ForumPosts   select x ).Take(5).ToArray();

            var model = new
            {
                modules = modules,
                topics = topics,
                hotPosts = hotPosts
            };

            return model;
        }
    }
}
