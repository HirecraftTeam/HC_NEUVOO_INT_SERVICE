using Applicant_Integration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace HireCraft.HM_APIService
{
    internal class Helper
    {

        internal static string NeuvooConnectionString
        {
            get
            {
                Helper op = new Helper();
                return op.ConString(ConfigurationManager.AppSettings["NeuvooConnectionString"]);
            }
        }
        //internal static string IndeedConnectionString
        //{
        //    get
        //    {
        //        Helper op = new Helper();
        //        return op.ConString(ConfigurationManager.AppSettings["IndeedConString"]);
        //    }
        //}
        public  string ConString(string x)
        {
            
                if (Helper.IsEncrypted == 1)
                {
                    Cryptographer crypto = new Cryptographer();
                    return crypto.opDecryptPasswordBase64(x);
                }
                else
                {
                    return ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                }
                //return ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            
        }


        internal static string CreateUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CreateUrl"].ToString();
            }
        }

        internal static Int32 IsEncrypted
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["IsEncrypted"]);
            }
        }


        internal static Int64 mins
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["mins"]);
            }

        }

        internal static Int16 IsPushToHCtoIndium
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["IsPushToHCtoIndium"]);
            }

        }

        internal static Int16 IsPullFromIndiumtoHC
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["IsPullFromIndiumtoHC"]);
            }

        }

        internal static string EmployeeDetailsUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmployeeDetailsUrl"];
            }

        }

        internal static string GetEmployeeDetailsFromIndium
        {
            get
            {
                return ConfigurationManager.AppSettings["GetEmployeeDetailsFromIndium"];
            }

        }


        internal static string Accessid
        {
            get
            {
                return ConfigurationManager.AppSettings["AccessID"];
            }

        }


        internal static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings["username"];
            }

        }

        internal static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["password"];
            }

        }

        internal static string TallintUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["TallintUrl"];
            }
        }

        internal static Boolean IsNeuvoo
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsNeuvoo"]);
            }
        }

        internal static Boolean IsBayt
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsBayt"]);
            }
        }


        internal static Boolean EnableReqSync
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableReqSync"]);
            }
        }

    }
}