using AdminServices.Event.Role;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using MassTransit.Testing;
using ServiceStack.Script;
using System;
using Xunit;

namespace XUnitTest_MTCore
{
    public class IT_Core
    {
        /// <summary>
        /// 
        /// </summary>
        protected InMemoryTestHarness harness;
        /// <summary>
        /// 
        /// </summary>
        public IT_Core()
        {
            this.harness = new InMemoryTestHarness();
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async void Roles()
        {
            await harness.Start();
            harness.Consumer<RoleDomainEvent>( () =>
            {
                // harness.Consume<RoleDomainEvent>( )
                return new RoleDomainEvent(null, null);
            });      

            try
            {
            }
            catch (Exception ) 
            {
                await harness.Stop();
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
