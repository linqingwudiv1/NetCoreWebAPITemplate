using AdminServices.Command.Account;
using AutoMapper;
using BaseDLL;
using BaseDLL.Helper.Asset;
using BaseDLL.Helper.Captcha;
using BusinessCoreDLL.Base;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Validator;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    class AccountBizServices : BaseBizServices, IAccountsBizServices
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
        protected IAssetHelper AssetHelper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper { get; set; }

        protected readonly IPublishEndpoint publishEndpoint;

        ///
        protected readonly ICaptchaHelper captchaHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_captchaHelper"></param>
        /// <param name="_assetHelper"></param>
        public AccountBizServices(  IIDGenerator _IDGenerator,
                                    IAccountAccesser AccountAccesser, 
                                    IMapper _mapper, 
                                    IPublishEndpoint _publishEndpoint, 
                                    ICaptchaHelper _captchaHelper,
                                    IAssetHelper _assetHelper)
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
            this.mapper = _mapper;
            this.publishEndpoint = _publishEndpoint;
            this.captchaHelper = _captchaHelper;
            this.AssetHelper = _assetHelper;
        }

        /// <summary>
        /// User Role 不需要Role
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public async Task<dynamic> GetInfo(long AccountID)
        {
            var account = (from x
                      in
                          this.accesser.db.Accounts.Where(x => x.Id == AccountID).AsNoTracking()
                           select x).SingleOrDefault();


            var userInfo = mapper.Map<Account, DTOAPIRes_UserInfo>(account);
            return new
            {
                user = userInfo
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<dynamic> ChangeIntroduction(long userid, DTOAPI_ChangeIntroduction info)
        {
            if (!AccountValidator.bValidIntroduction(info.introduction) ) 
            {
                throw new Exception("简介不符合规则");
            }

            await this.publishEndpoint.Publish(new ChangeAccountIntroductionCommand 
            {
                id = userid,
                introduction = info.introduction
            });

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<dynamic> ChangeNickName(long userid, DTOAPI_ChangeNickName info)
        {
            if (!AccountValidator.bValidDisplayName(info.nickName))
            {
                throw new Exception("昵称不符合规则");
            }

            await this.publishEndpoint.Publish(new ChangeAccountNickNameCommand
            {
                id = userid,
                nickName = info.nickName
            });

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetCOSToken()
        {
            return AssetHelper.GetTempToken(GAssetVariable.Bucket);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task ChangeAvatar(long userId,DTOAPI_ChangeAvatar info)
        {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            this.publishEndpoint.Publish(new ChangeAvatarCommand 
            {
                userId = userId,
                avatar = info.avatar
            });
#pragma warning restore CS4014 
        }
    }
}
