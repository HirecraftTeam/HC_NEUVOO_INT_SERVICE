using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC_NEUVOO_INT_SERVICE.Models
{
    public class RequestJsonDetails
    {
        public string state_titles { get; set; }
        public string company_id { get; set; }
        public string token { get; set; }
        public long rid { get; set; }
        public string career_level { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string function_title { get; set; }
        public string sub_function_title { get; set; }
        public string job_title { get; set; }
        public string req_type { get; set; }
        public string req_number { get; set; }
        public long no_of_openings { get; set; }
        public string job_description { get; set; }
        public string plain_jd { get; set; }
        public string req_status { get; set; }
        public long status { get; set; }
        public string contact_name { get; set; }
        public string designation_title { get; set; }
        public double experience_from { get; set; }
        public double experience_to { get; set; }
        public string req_start_date { get; set; }
        public string req_end_date { get; set; }
        public string employment_type { get; set; }
        public string grade_titles { get; set; }
        public string location_titles { get; set; }
        public long ctc_from_salary { get; set; }
        public long ctc_to_salary { get; set; }
        public string ctc_currency_title { get; set; }
        public string ctc_duration_title { get; set; }
        public string industry_title { get; set; }
        public long req_salary { get; set; }
        public long tn_id { get; set; }
        public string skill_titles { get; set; }
        public string job_contact_email { get; set; }
        public Int32 job_portal_id { get; set; }
        public string country_titles { get; set; }
        public Int32 enable_residencelocation_Filter { get; set; }
        public Int32 enable_degree_filter { get; set; }
        public Int32 enable_age_filter { get; set; }
        public Int32 enable_jobcategories_filter { get; set; }
        public Int32 enable_jobrole_filter { get; set; }
        public Int32 enable_experience_filter { get; set; }
        public Int32 enable_gender_filter { get; set; }
        public Int32 premium { get; set; }
        public Int32 min_age { get; set; }
        public Int32 max_age { get; set; }
        public string education_Location { get; set; }
        public Int32 hide_comany_details { get; set; }
        public Int32 enable_notification_settings { get; set; }
        public string job_link { get; set; }

    }
}
