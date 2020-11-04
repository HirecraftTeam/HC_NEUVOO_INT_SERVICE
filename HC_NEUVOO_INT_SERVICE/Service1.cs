using HireCraft.HM_APIService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HC_NEUVOO_INT_SERVICE
{
    public partial class Service1 : ServiceBase
    {
        Timer reqTimer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           // System.Diagnostics.Debugger.Launch();

            Log.LogData("--Neuvoo Job Integration Service---", Log.Status.Info);
            Log.LogData(ConfigurationManager.AppSettings["ServiceName"] + " Started", Log.Status.Info);
            try
            {

                //HC_INDEED_INT.Handler.IndeedJobHandler opIndeedHandler = new HC_INDEED_INT.Handler.IndeedJobHandler();
                //opIndeedHandler.Process();

                //HC_NEUVOO_INT_SERVICE.Handler.JobHandler opJobHandler = new HC_NEUVOO_INT_SERVICE.Handler.JobHandler();

                //opJobHandler.Process();
                if (Helper.IsNeuvoo)
                {
                    reqTimer = new Timer(TimeSpan.FromMinutes(Helper.mins).TotalMilliseconds);
                    reqTimer.Elapsed += new ElapsedEventHandler(opReqNeuvooHandler);
                    reqTimer.Start();
                }

               
            }
            catch (Exception ex)
            {
                Log.LogData(ex.Message, Log.Status.Info);
            }
        }

        void opReqNeuvooHandler(object sender, ElapsedEventArgs args)
        {
            Task.Run(() =>
            {
                HC_NEUVOO_INT_SERVICE.Handler.JobHandler opJobHandler = new HC_NEUVOO_INT_SERVICE.Handler.JobHandler();
                Log.LogData("------START------", Log.Status.Info);
                opJobHandler.Process();
                Log.LogData("-------END-------", Log.Status.Info);

            });
        }

        protected override void OnStop()
        {
        }
    }
}
