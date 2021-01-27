using Autofac;
using BaseDLL.DTO;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using BusinessAdminDLL.Roles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebAdminService.Controllers;
using Xunit;

namespace XUnitTest_WebAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitWebAdmin
    {
        readonly ContainerBuilder builder; 

        /// <summary>
        /// 
        /// </summary>
        public UnitWebAdmin() 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        private IList<DTOAPI_Role> TestRoleData() 
        {
            IList<DTOAPI_Role> list = new List<DTOAPI_Role> 
            {
                new DTOAPI_Role{key = 1, description = "", name = "" , routes = new List<DTOAPI_RoutePages>() }
            };

            return list;
        }

        [Fact]
        public void UnitTest_Role()
        {
            // Mock<IRolesBizServices> mockRepo = new Mock<IRolesBizServices>();
            // 
            // mockRepo.Setup(repo => repo.GetRoles()).Returns( TestRoleData() );
            // RolesController controller = new RolesController(mockRepo.Object);
            // 
            // var result = controller.GetRolesAsync() as OkObjectResult;
            // DTO_ReturnModel<dynamic>  val = result.Value as DTO_ReturnModel<dynamic>;
            // 
            // var data = (val.data);
            // Assert.True(data.Count > 0);
        }

    }
}
