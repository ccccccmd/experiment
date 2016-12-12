using System;
using Common.Logging;
using Quartz;

namespace EFTest.Quartz
{
    public class TestJob : IJob
    {
        private ILog logger = LogManager.GetLogger(typeof(TestJob));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info("Test_job_开始任务"+DateTime .Now .ToString( "HH:mm:ss tt zz"));
                //using (var db=new DbContext() )
                //{
                //    for (int i = 0; i < 4; i++)
                //    {
                //        var car = new Cars()
                //        {
                //             CreateTime =DateTime.Now , Name ="test_job_car_"+i
                //        };
                //        db.Configuration.AutoDetectChangesEnabled = false;
                //        db.Carses.Add(car);
                //    }

                //    db.SaveChanges();
                //    db.Configuration.AutoDetectChangesEnabled = true;
                //}

                logger.Info("Test_job_任务完成");
            }
            catch (Exception ex)
            {
              
               logger.Error("Test_job_异常",ex);
            }
        }
    }
}