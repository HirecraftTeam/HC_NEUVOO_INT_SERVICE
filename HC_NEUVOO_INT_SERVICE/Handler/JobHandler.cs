using HC_NEUVOO_INT_SERVICE.Models;
using HireCraft.HM_APIService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HC_NEUVOO_INT_SERVICE.Handler
{
    public class JobHandler
    {

        public void Process()
        {
            string DatabaseName = "";
            Int64 RID = 0;
            string JSONString = "";
            Int16 type = 0;
            DataSet ds = new DataSet();
            try
            {
                //fetching all the data team assign jobs from the database
                Log.LogData("Fetching Data... ", Log.Status.Info);
                using (SqlConnection conn = new SqlConnection(Helper.NeuvooConnectionString))
                {
                    conn.Open();
                    using (SqlCommand oCmd = conn.CreateCommand())
                    {
                        oCmd.CommandText = "usp_getNeuvooJobs";
                        oCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter oSql = new SqlDataAdapter(oCmd);
                        oSql.Fill(ds);
                    }
                }
                Log.LogData("Fetching Data Completed... ", Log.Status.Info);
                //preparing Json one by one and requesting tallint jobs for pushing the Jobs 
                //JSONString = JsonConvert.SerializeObject(ds.Tables[0].Rows[0].);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        RequestJsonDetails opRequestJsonDetails = new RequestJsonDetails();
                        opRequestJsonDetails.rid = Convert.ToInt64(dr["rid"]);
                        opRequestJsonDetails.state_titles = dr["state_titles"].ToString().TrimEnd(',');
                        opRequestJsonDetails.career_level = dr["career_level"].ToString();
                        opRequestJsonDetails.client_id = dr["client_id"].ToString();
                        opRequestJsonDetails.client_secret = dr["client_secret"].ToString();
                        opRequestJsonDetails.company_id = dr["company_id"].ToString();
                        opRequestJsonDetails.contact_name = dr["contact_name"].ToString();
                        opRequestJsonDetails.country_titles = dr["country_titles"].ToString().TrimEnd(',');
                        opRequestJsonDetails.ctc_currency_title = dr["ctc_currency_title"].ToString();
                        opRequestJsonDetails.ctc_duration_title = dr["ctc_duration_title"].ToString();
                        opRequestJsonDetails.ctc_from_salary = Convert.ToInt64(dr["ctc_from_salary"]);
                        opRequestJsonDetails.ctc_to_salary = Convert.ToInt64(dr["ctc_to_salary"]);
                        opRequestJsonDetails.designation_title = dr["designation_title"].ToString();
                        opRequestJsonDetails.education_Location = dr["education_Location"].ToString();
                        opRequestJsonDetails.employment_type = dr["employment_type"].ToString();
                        opRequestJsonDetails.enable_age_filter = Convert.ToInt16(dr["ctc_to_salary"]);
                        opRequestJsonDetails.enable_degree_filter = Convert.ToInt16(dr["enable_degree_filter"]);
                        opRequestJsonDetails.enable_experience_filter = Convert.ToInt16(dr["enable_experience_filter"]);
                        opRequestJsonDetails.enable_gender_filter = Convert.ToInt16(dr["enable_gender_filter"]);
                        opRequestJsonDetails.enable_jobcategories_filter = Convert.ToInt16(dr["enable_jobcategories_filter"]);
                        opRequestJsonDetails.enable_jobrole_filter = Convert.ToInt16(dr["enable_jobrole_filter"]);
                        opRequestJsonDetails.enable_notification_settings = Convert.ToInt16(dr["enable_notification_settings"]);
                        opRequestJsonDetails.enable_residencelocation_Filter = Convert.ToInt16(dr["enable_residencelocation_Filter"]);
                        opRequestJsonDetails.experience_from = Convert.ToDouble(dr["experience_from"]);
                        opRequestJsonDetails.experience_to = Convert.ToDouble(dr["experience_to"]);
                        opRequestJsonDetails.function_title = Convert.ToString(dr["function_title"]);
                        opRequestJsonDetails.grade_titles = Convert.ToString(dr["grade_titles"]);
                        opRequestJsonDetails.hide_comany_details = Convert.ToInt16(dr["hide_comany_details"]);
                        opRequestJsonDetails.industry_title = Convert.ToString(dr["industry_title"]);
                        opRequestJsonDetails.job_contact_email = Convert.ToString(dr["job_contact_email"]);
                        opRequestJsonDetails.job_description = Convert.ToString(dr["job_description"]);
                        opRequestJsonDetails.job_link = Convert.ToString(dr["job_link"]);
                        opRequestJsonDetails.job_portal_id = Convert.ToInt16(dr["job_portal_id"]);
                        opRequestJsonDetails.job_title = Convert.ToString(dr["job_title"]);
                        opRequestJsonDetails.location_titles = (dr["location_titles"].ToString().TrimEnd(','));
                        opRequestJsonDetails.max_age = Convert.ToInt16(dr["max_age"]);
                        opRequestJsonDetails.min_age = Convert.ToInt16(dr["min_age"]);
                        opRequestJsonDetails.no_of_openings = Convert.ToInt64(dr["no_of_openings"]);
                        opRequestJsonDetails.plain_jd = Convert.ToString(dr["plain_jd"]);
                        opRequestJsonDetails.premium = Convert.ToInt16(dr["premium"]);
                        //DateTime date = DateTime.Parse(dr["req_end_date"].ToString());
                        opRequestJsonDetails.req_start_date = Convert.ToDateTime(dr["req_start_date"]).ToString("yyyy-MM-dd");
                        //string newdate = DateTime.ParseExact(date.ToString(), "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        //DateTime oDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm tt", null);
                        //opRequestJsonDetails.req_end_date = date.ToString("'yyyy'-'mm'-'dd'");
                        opRequestJsonDetails.req_number = Convert.ToString(dr["req_number"]);
                        opRequestJsonDetails.req_salary = Convert.ToInt64(dr["req_salary"]);
                        opRequestJsonDetails.req_end_date = Convert.ToDateTime(dr["req_end_date"]).ToString("yyyy-MM-dd");
                        opRequestJsonDetails.req_status = Convert.ToString(dr["req_status"]);
                        opRequestJsonDetails.req_type = Convert.ToString(dr["req_type"]);
                        RID = Convert.ToInt64(dr["rid"]);
                        DatabaseName = Convert.ToString(dr["databasename"]);
                        type = Convert.ToInt16(dr["type"]);
                        opRequestJsonDetails.skill_titles = dr["skill_titles"].ToString().TrimEnd(',');
                        opRequestJsonDetails.status = Convert.ToInt64(dr["status"]);
                        opRequestJsonDetails.sub_function_title = Convert.ToString(dr["sub_function_title"]);
                        opRequestJsonDetails.tn_id = Convert.ToInt64(dr["tn_id"]);
                        opRequestJsonDetails.token = Convert.ToString(dr["token"]);
                        JSONString = JsonConvert.SerializeObject(opRequestJsonDetails);
                        SendRequirementDetails(JSONString, RID, type, DatabaseName);//in this method requesting and updating the IsPushed flag 
                    }
                }

                else
                    Log.LogData("No Data... ", Log.Status.Info);


            }
            catch (Exception ex)
            {
                Log.LogData("Exception at Process: " + ex.Message, Log.Status.Fatal);
            }
        }

        private void SendRequirementDetails(string jSONString, Int64 RID, Int16 type, string dbName)
        {
            RequirementResponseDetails opRequirementDetailsResponse = new RequirementResponseDetails();
            string result = "";
            try
            {
                Log.LogData("Requesting... ", Log.Status.Info);
                string link = Helper.CreateUrl;
                jSONString = jSONString.Trim('[', ']');
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(link);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                Log.LogData("request " + jSONString, Log.Status.Info);
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jSONString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                Log.LogData("Response " + result, Log.Status.Info);
                opRequirementDetailsResponse = JsonConvert.DeserializeObject<RequirementResponseDetails>(result);
                Log.LogData("Requesting Ended... ", Log.Status.Info);
                if (opRequirementDetailsResponse.code == 200)
                {
                    UpdateRequisitionFlag(jSONString, result, RID, type, dbName);//Flag updation here
                }
            }
            catch (Exception ex)
            {
                Log.LogData("Exception at SendRequirementDetails" + ex, Log.Status.Error);
            }
        }

        private void UpdateRequisitionFlag(string jSONString, string result, long rID, Int16 type, string dbName)
        {
            try
            {
                Log.LogData("Updating Flag... ", Log.Status.Info);
                using (SqlConnection conn = new SqlConnection(Helper.NeuvooConnectionString))
                {
                    conn.Open();
                    using (SqlCommand oCmd = conn.CreateCommand())
                    {
                        oCmd.CommandText = "usp_updateBaytFlag";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.AddWithValue("@rid", rID);
                        oCmd.Parameters.AddWithValue("@result", result);
                        oCmd.Parameters.AddWithValue("@type", type);
                        oCmd.Parameters.AddWithValue("@db", dbName);
                        oCmd.ExecuteNonQuery();
                    }
                }
                Log.LogData("Updating Flag Completed... ", Log.Status.Info);
            }
            catch (Exception ex)
            {
                Log.LogData("Exception at UpdateRequisitionFlag" + ex.ToString(), Log.Status.Error);
            }
        }

    }
}
