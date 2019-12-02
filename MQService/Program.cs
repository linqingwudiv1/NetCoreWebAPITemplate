using RabbitMQ.Client;
using System;

namespace MQService
{
    /// <summary>
    /// 进程处理
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "admin",        //用户名
                Password = "admin",        //密码
                HostName = "192.168.1.131" //rabbitmq ip
            };

            //create of connection 
            var connection = factory.CreateConnection();
      
            //create of channel
            var channel = connection.CreateModel();

            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(" Hello，world！");
            
            channel.BasicPublish("","",null,messageBodyBytes);
            
            connection.Close();
            channel.Close();

            //Console.WriteLine("Hello World!");
        }
    }
}
