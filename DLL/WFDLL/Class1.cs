using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WFDLL
{

    public class MyData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }

    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }

    public class GoodBye : StepBody
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
            // throw new NotImplementedException();
        }
    }

    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<GoodBye>();
        }
    }
}
