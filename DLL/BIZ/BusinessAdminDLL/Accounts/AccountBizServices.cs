using AdminServices.Command.Account;
using AutoMapper;
using BaseDLL;
using BaseDLL.DTO;
using BaseDLL.Helper.Asset;
using BaseDLL.Helper.Captcha;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.DTO.API.Users;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Accounts
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

        /// <summary>
        /// 
        /// </summary>
        protected readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        protected readonly ICaptchaHelper captchaHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_captchaHelper"></param>
        /// <param name="_AssetHelper"></param>
        public AccountBizServices(  IIDGenerator        _IDGenerator, 
                                    IAccountAccesser    AccountAccesser, 
                                    IMapper             _mapper, 
                                    IPublishEndpoint    _publishEndpoint, 
                                    ICaptchaHelper      _captchaHelper,
                                    IAssetHelper        _AssetHelper   )
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
            this.mapper = _mapper;
            this.publishEndpoint = _publishEndpoint;
            this.captchaHelper = _captchaHelper;
            this.AssetHelper = _AssetHelper;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public  async Task<DTO_PageableModel<DTOAPIRes_UserInfo>> GetUsers(DTO_PageableQueryModel<DTO_GetUsers> query )
        {
            var users = await this.accesser.Get(query);

            DTO_PageableModel<DTOAPIRes_UserInfo> ret_model = new DTO_PageableModel<DTOAPIRes_UserInfo>
            {
                pageNum = users.pageNum,
                pageSize = users.pageSize,
                total = users.total,
                data = users.data.Select(x => 
                {
                    DTOAPIRes_UserInfo userInfo = this.mapper.Map<Account, DTOAPIRes_UserInfo>(x);
                    userInfo.roles = x.AccountRoles.Select(c => this.mapper.Map<DTOAPI_Role>(c.role) ).ToList();
                    return userInfo;
                }).ToArray()
            };

            return ret_model;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public async Task<dynamic> GetInfo(long AccountID)
        {
            var account = (from x
                      in
                          this.accesser.db.Accounts.Where(x => x.Id == AccountID)
                                                   .Include(c => c.AccountRoles)
                                                   .ThenInclude(c => c.role)
                           select x).SingleOrDefault();


            var userInfo = mapper.Map<Account, DTOAPIRes_UserInfo>(account);
            userInfo.roles = account.AccountRoles.Select(x => mapper.Map<RoleType, DTOAPI_Role>(x.role)).ToArray();
            return new
            {
                user = userInfo
            };
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task UpdateUsersRole(DTOAPIReq_UpdateUsersRole info)
        {
            var cmd = new UpdateAccountsRoleCommand 
            {
                users = info.users,
                roles = info.roles
            }; //this.mapper.Map<DTOAPIReq_UpdateUsersRole, UpdateAccountsRoleCommand>(info);

            await this.publishEndpoint.Publish(cmd);
            return;
            // throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<IList<long>> GetAdminPageRoles(long userid)
        {
            Account account = this.accesser.Get(key: userid).Item1;

            accesser.db.Entry(account).Collection(x => x.AccountRoles).Load();
            if (account != null)
            {
                return account.AccountRoles.Select(x => x.RoleId).ToArray();
            }
            else
            {
                return new Int64[] { };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetCOSToken()
        {
            return AssetHelper.GetTempToken(GAssetVariable.Bucket);
        }
    }
}
