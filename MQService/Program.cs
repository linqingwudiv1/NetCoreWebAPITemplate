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
            try
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    UserName = "admin",        // passport
                    Password = "1qaz@WSX",     // password
                    HostName = "127.0.0.1" // rabbitmq ip
                };

                //create of connection 
                var connection = factory.CreateConnection();

                //create of channel
                var channel = connection.CreateModel();

                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(" Hello，world！");

                channel.BasicPublish("", "", null, messageBodyBytes);

                connection.Close();
                channel.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            //Console.WriteLine("Hello World!");
        }
    }
}
