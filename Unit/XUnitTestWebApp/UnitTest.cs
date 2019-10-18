using Bogus;
using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using Xunit;

namespace XUnitTestWebApp
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test()
        {

            int DebugThreadCount = 10;
            int Id = 3;
            string sqliteDBConn = "Data Source=.LocalDB/sqliteTestDB.db";
            Faker faker = new Faker(locale: "zh_CN");

            for (int i = 0; i < DebugThreadCount; i++)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    int count = 0;
                    while (true)
                    {
                        try
                        {
                            ExamContext db = new ExamContext(sqliteDBConn);
                            Account account = db.Accounts.Find(Id);

                            if (account != null)
                            {
                                account.Name = (faker.Name.FirstName() + faker.Name.LastName());
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine($"not account.....{count}");
                            }

                            count++;
                            Thread.Sleep(10);

                            if (count >= 1000)
                            {
                                break;
                            }
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            //locking....
                            Thread.Sleep(10);
                            continue;
                        }
                        catch (Exception ex)
                        {
                            //Other Exception....
                            break;
                        }
                    }
                }));

                //thread.Start();
                thread.Start();
            }
        }
    }
}
