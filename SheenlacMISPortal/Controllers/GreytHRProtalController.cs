using System.Text;
using SheenlacMISPortal.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Data.OleDb;
using System.ComponentModel;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;
//using static SheenlacMISPortal.Models.GSTToken.AuthResponse;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace SheenlacMISPortal.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]


    public class GreytHRProtalController : Controller
    {

        private readonly IConfiguration Configuration;
        private static string Username = string.Empty;
        private static string Password = string.Empty;
        private static string baseAddress = "https://sheenlac.greythr.com/uas/v1/oauth2/client-token";
        public GreytHRProtalController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        [HttpPost]
        [Route("getRecruitmentReport")]
        public ActionResult getRecruitmentReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_GreythrHRPortal";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
                    cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
                    cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
                    cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
                    cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
                    cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                    cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }
        [HttpPost]
        [Route("UploadAttritionExcel")]
        public IActionResult UploadAttritionExcel(IFormFile files)
        {
            byte[] datautr;
            string result = "";
            ByteArrayContent bytes;

            var files1 = Request.Form.Files;

            if (files1.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files1)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                fileName = fileName.Replace("\"", "");
                var fullPath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", fileName.ToString());
                //var fullPath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());


                //using (var stream = new FileStream(fullPath, FileMode.Create))
                //{
                //    file.CopyTo(stream);
                //}
                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving file: {ex.Message}");
                }
            }



            //  MultipartFormDataContent multiForm = new MultipartFormDataContent();

            try
            {
                var filePath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", files.FileName.ToString());
                //var filePath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());

                DataTable dt = new DataTable();
                var dt1 = new DataTable();
                var fi = new FileInfo(filePath);
                // Check if the file exists
                if (!fi.Exists)
                    throw new Exception("File " + filePath + " Does Not Exists");

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var xlPackage = new ExcelPackage(fi);
                var worksheet2 = xlPackage.Workbook.Worksheets[0];

                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets[worksheet2.Name];

                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);


                dynamic data = JsonConvert.DeserializeObject<dynamic>(JSONresult.ToString());

                string conutr = @"""excelUpdtData""";
                string jsdata = string.Empty;
                jsdata = "{" + conutr + ":" + data + "}";

                jsdata = jsdata.Replace("Reporting Manager", "Reporting_Manager");
                jsdata = jsdata.Replace("Department/Cluster", "Department_Cluster");

                jsdata = jsdata.Replace("Total Persons hired", "Total_Persons_hired");
                jsdata = jsdata.Replace("Exit Type", "Exit_Type");
                jsdata = jsdata.Replace("Exit Feedback", "Exit_Feedback");
                jsdata = jsdata.Replace("Regrettable exit or not", "Regrettable_exit_or_not");
                jsdata = jsdata.Replace("Educational Qualification", "Educational_Qualification");
                jsdata = jsdata.Replace("Previous Company Name", "Previous_Company_Name");
                jsdata = jsdata.Replace("Previous Industry", "Previous_Industry");
                jsdata = jsdata.Replace("Experience in Previous Company", "Experience_in_Previous_Company");
                jsdata = jsdata.Replace("Source of hiring", "Source_of_hiring");
                jsdata = jsdata.Replace("Target Achieved(Months)", "Target_Achieved_Months");
                jsdata = jsdata.Replace("Incentive Received(Months)", "Incentive_Received_Months");
                jsdata = jsdata.Replace("Date Of Join", "Date_Of_Join");
                jsdata = jsdata.Replace("Date Of Exit", "Date_Of_Exit");


                //string st = "{\"excelUpdtData\":[{\"PositionId\":\"NA\",\"Reporting_Manager\":\"Jain Jose K\"}]}\r\n";
                var result5 = JObject.Parse(jsdata);

                var items1 = result5["excelUpdtData"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


                //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1[0]);
                //string sd2 = "[" + jsonString2 + "]";

                //var model2 = JsonConvert.DeserializeObject<List<Models.attritionExcelModel>>(sd2);
                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
                //string sd2 = "[" + jsonString2 + "]";

                var model2 = JsonConvert.DeserializeObject<List<Models.attritionExcelModel>>(jsonString2);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_attrition_insert";
                        sqlBulkCopy.ColumnMappings.Add("PositionId", "PositionId");
                        sqlBulkCopy.ColumnMappings.Add("Reporting_Manager", "Reporting_Manager");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Department_Cluster", "Department_Cluster");
                        sqlBulkCopy.ColumnMappings.Add("Region", "Region");
                        sqlBulkCopy.ColumnMappings.Add("Total_Persons_hired", "Total_Persons_hired");
                        //sqlBulkCopy.ColumnMappings.Add("qualCompletionYear", "qualCompletionYear");
                        sqlBulkCopy.ColumnMappings.Add("Exit_Type", "Exit_Type");
                        sqlBulkCopy.ColumnMappings.Add("Exit_Feedback", "Exit_Feedback");

                        sqlBulkCopy.ColumnMappings.Add("Experience", "Experience");
                        sqlBulkCopy.ColumnMappings.Add("Regrettable_exit_or_not", "Regrettable_exit_or_not");
                        sqlBulkCopy.ColumnMappings.Add("Recruitment_Age", "Recruitment_Age");

                        sqlBulkCopy.ColumnMappings.Add("Educational_Qualification", "Educational_Qualification");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("Previous_Company_Name", "Previous_Company_Name");
                        sqlBulkCopy.ColumnMappings.Add("Previous_Industry", "Previous_Industry");
                        sqlBulkCopy.ColumnMappings.Add("Experience_in_Previous_Company", "Experience_in_Previous_Company");
                        sqlBulkCopy.ColumnMappings.Add("Source_of_hiring", "Source_of_hiring");
                        sqlBulkCopy.ColumnMappings.Add("Target_Achieved_Months", "Target_Achieved_Months");
                        sqlBulkCopy.ColumnMappings.Add("Incentive_Received_Months", "Incentive_Received_Months");
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("employeeNo", "employeeNo");
                        sqlBulkCopy.ColumnMappings.Add("Channel", "Channel");
                        sqlBulkCopy.ColumnMappings.Add("Date_Of_Join", "Date_Of_Join");
                        sqlBulkCopy.ColumnMappings.Add("Date_Of_Exit", "Date_Of_Exit");

                        sqlBulkCopy.ColumnMappings.Add("cremarks1", "cremarks2");
                        sqlBulkCopy.ColumnMappings.Add("cremarks3", "cremarks3");
                        sqlBulkCopy.ColumnMappings.Add("CCREATEDBY", "CCREATEDBY");
                        sqlBulkCopy.ColumnMappings.Add("LDATE", "LDATE");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }



                return Ok(200);

            }
            catch (Exception e)
            {

            }

            return Ok("201");
        }


        [HttpPost]
        [Route("UploadjobDescriptionExcel")]
        public IActionResult UploadjobDescriptionExcel(IFormFile files)
        {
            byte[] datautr;
            string result = "";
            ByteArrayContent bytes;

            var files1 = Request.Form.Files;

            if (files1.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files1)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                fileName = fileName.Replace("\"", "");
                var fullPath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", fileName.ToString());
                // var fullPath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());

                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving file: {ex.Message}");
                }
            }



            //  MultipartFormDataContent multiForm = new MultipartFormDataContent();

            try
            {
                var filePath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", files.FileName.ToString());
                //var filePath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());

                DataTable dt = new DataTable();
                var dt1 = new DataTable();
                var fi = new FileInfo(filePath);
                // Check if the file exists
                if (!fi.Exists)
                    throw new Exception("File " + filePath + " Does Not Exists");

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var xlPackage = new ExcelPackage(fi);
                var worksheet2 = xlPackage.Workbook.Worksheets[0];

                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets[worksheet2.Name];

                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);


                dynamic data = JsonConvert.DeserializeObject<dynamic>(JSONresult.ToString());

                string conutr = @"""excelUpdtData""";
                string jsdata = string.Empty;
                jsdata = "{" + conutr + ":" + data + "}";

                //string st = "{\"excelUpdtData\":[{\"PositionId\":\"NA\",\"Reporting_Manager\":\"Jain Jose K\"}]}\r\n";
                var result5 = JObject.Parse(jsdata);

                var items1 = result5["excelUpdtData"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
                //string sd2 = "[" + jsonString2 + "]";

                var model2 = JsonConvert.DeserializeObject<List<Models.jobDescriptionExcelModel>>(jsonString2);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "job_description";
                        sqlBulkCopy.ColumnMappings.Add("Cluster_Dept", "Cluster_Dept");
                        sqlBulkCopy.ColumnMappings.Add("Territory_Location", "Territory_Location");
                        sqlBulkCopy.ColumnMappings.Add("Role", "Role");
                        sqlBulkCopy.ColumnMappings.Add("Reporting_Manager", "Reporting_Manager");
                        sqlBulkCopy.ColumnMappings.Add("JD", "JD");
                        sqlBulkCopy.ColumnMappings.Add("Roles_and_Responsibilities", "Roles_and_Responsibilities");
                        sqlBulkCopy.ColumnMappings.Add("SkillSet_Required", "SkillSet_Required");
                        sqlBulkCopy.ColumnMappings.Add("Preferred_Skills", "Preferred_Skills");

                        sqlBulkCopy.ColumnMappings.Add("Preferred_Experience", "Preferred_Experience");
                        sqlBulkCopy.ColumnMappings.Add("Grade", "Grade");
                        sqlBulkCopy.ColumnMappings.Add("Salary_Range_minimum", "Salary_Range_minimum");
                        sqlBulkCopy.ColumnMappings.Add("Salary_Range_maximum", "Salary_Range_maximum");

                        sqlBulkCopy.ColumnMappings.Add("Travel_Category", "Travel_Category");
                        sqlBulkCopy.ColumnMappings.Add("Org_Unit", "Org_Unit");
                        sqlBulkCopy.ColumnMappings.Add("New_Position_ID_short_code", "New_Position_ID_short_code");
                        sqlBulkCopy.ColumnMappings.Add("New_Position_ID_Long_description", "New_Position_ID_Long_description");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_hardware", "IT_Access_hardware");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_SIM_Card", "IT_Access_SIM_Card");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_Access_Card_HO", "IT_Access_Access_Card_HO");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_SAP_ID", "IT_Access_SAP_ID");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_SAP_T_Codes_Access", "IT_Access_SAP_T_Codes_Access");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_MIS_access", "IT_Access_MIS_access");
                        sqlBulkCopy.ColumnMappings.Add("IT_Access_Email_Category", "IT_Access_Email_Category");
                        sqlBulkCopy.ColumnMappings.Add("Visiting_Cards", "Visiting_Cards");
                        sqlBulkCopy.ColumnMappings.Add("Cost_Centre", "Cost_Centre");

                        sqlBulkCopy.ColumnMappings.Add("createdby", "createdby");
                        sqlBulkCopy.ColumnMappings.Add("createddate", "createddate");
                        sqlBulkCopy.ColumnMappings.Add("modifiedby", "modifiedby");
                        sqlBulkCopy.ColumnMappings.Add("modifieddate", "modifieddate");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }



                return Ok(200);

            }
            catch (Exception e)
            {

            }

            return Ok("201");
        }




        [HttpPost]
        [Route("UploadrecruitmentExcel")]
        public IActionResult UploadrecruitmentExcel(IFormFile files)
        {
            byte[] datautr;
            string result = "";
            ByteArrayContent bytes;

            var files1 = Request.Form.Files;

            if (files1.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files1)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                fileName = fileName.Replace("\"", "");
                var fullPath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", fileName.ToString());
                // var fullPath = Path.Combine(@"D:\MIS_UIAngular_Git\MISPortalUI\src\assets\images", files.FileName.ToString());


                //using (var stream = new FileStream(fullPath, FileMode.Create))
                //{
                //    file.CopyTo(stream);
                //}
                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving file: {ex.Message}");
                }
            }



            //  MultipartFormDataContent multiForm = new MultipartFormDataContent();

            try
            {
                var filePath = Path.Combine(@"D:\MISPortal\MISUI\assets\images", files.FileName.ToString());
                //var filePath = Path.Combine(@"D:\MIS_UIAngular_Git\MISPortalUI\src\assets\images", files.FileName.ToString());

                DataTable dt = new DataTable();
                var dt1 = new DataTable();
                var fi = new FileInfo(filePath);
                // Check if the file exists
                if (!fi.Exists)
                    throw new Exception("File " + filePath + " Does Not Exists");

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var xlPackage = new ExcelPackage(fi);
                var worksheet2 = xlPackage.Workbook.Worksheets[0];

                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets[worksheet2.Name];

                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);


                dynamic data = JsonConvert.DeserializeObject<dynamic>(JSONresult.ToString());

                string conutr = @"""excelUpdtData""";
                string jsdata = string.Empty;
                jsdata = "{" + conutr + ":" + data + "}";

                jsdata = jsdata.Replace("position ID", "positionID");
                jsdata = jsdata.Replace("Date on which the position became vacant", "Date_on_which_the_position_became_vacant ");

                jsdata = jsdata.Replace("Date on which profiles are shared", "Date_on_which_profiles_are_shared");
                jsdata = jsdata.Replace("No. of profiles shared", "No_of_profiles_shared");
                jsdata = jsdata.Replace("Date(s) on which the profiles are shortlisted by the interviewer", "Dates_on_which_the_profiles_are_shortlisted_by_the_interviewer");
                jsdata = jsdata.Replace("No. of profiles shortlisted", "No_of_profiles_shortlisted");
                jsdata = jsdata.Replace("Date(s) on which interview(s) are scheduled", "Date_on_which_interview_are_scheduled");
                jsdata = jsdata.Replace("Name of the Interviewer", "Name_of_the_Interviewer");
                jsdata = jsdata.Replace("No of persons interviewed", "No_of_persons_interviewed");
                jsdata = jsdata.Replace("No of offer letters issued", "No_of_offer_letters_issued");


                jsdata = jsdata.Replace("Job portals", "Job_portals");
                jsdata = jsdata.Replace("Employee Referrals  (Name of the employee)", "Employee_Referrals");
                jsdata = jsdata.Replace("Social media posting", "Social_media_posting");
                jsdata = jsdata.Replace("Companys career page", "Companys_career_page");
                jsdata = jsdata.Replace("Internal promotion", "Internal_promotion");


                jsdata = jsdata.Replace("Date of Issuance", "Date_of_Issuance ");
                jsdata = jsdata.Replace("Date of Acceptance", "Date_of_Acceptance");
                jsdata = jsdata.Replace("Date of Rejection", "Date_of_Rejection");
                jsdata = jsdata.Replace("In case of rejection reason", "In_case_of_rejection_reason");
                jsdata = jsdata.Replace("Date on which the new candidate onboarded", "Date_on_which_the_new_candidate_onboarded");
                jsdata = jsdata.Replace("Recruitment Age", "Recruitment_Age");
                jsdata = jsdata.Replace("Educational Qualification", "Educational_Qualification");
                jsdata = jsdata.Replace("The candidate has not performed", "The_candidate_has_not_performed");

                jsdata = jsdata.Replace("No of months it took for the candidate to achieve minimum no", "No_of_months_achieve_minimum_no");
                jsdata = jsdata.Replace("No of months it took for the candidate to achieve 100% target", "No_of_months_it_took_for_the_candidate_to_achieve_100_percent_target");
                jsdata = jsdata.Replace("Hiring Manager Satisfaction(on a scale of 1 to 5, with 5 being the least)", "Hiring_Manager_Satisfaction");
                jsdata = jsdata.Replace("Candidates job satisfaction(on a scale of 1 to 5, with 5 being the least)", "Candidates_job_satisfaction");
                jsdata = jsdata.Replace("0 to 3 months voluntary", "one_Voluntary");
                jsdata = jsdata.Replace("0 to 3 months involuntary", "one_involuntary");
                jsdata = jsdata.Replace("0 to 3 months abscond", "one_abscond");
                jsdata = jsdata.Replace("3 to 6 months~voluntary", "two_Voluntary");

                jsdata = jsdata.Replace("3 to 6 months~involuntary", "two_involuntary");
                jsdata = jsdata.Replace("3 to 6 months~abscond", "two_abscond");
                jsdata = jsdata.Replace("6 to 9 months~voluntary", "three_Voluntary");
                jsdata = jsdata.Replace("6 to 9 months~involuntary", "three_involuntary");
                jsdata = jsdata.Replace("6 to 9 months~abscond", "three_abscond");
                jsdata = jsdata.Replace("9 to 12 months~voluntary", "four_Voluntary");
                jsdata = jsdata.Replace("9 to 12 months~involuntary", "four_involuntary");
                jsdata = jsdata.Replace("9 to 12 months~abscond", "four_abscond");


                jsdata = jsdata.Replace("No of years of previous experience in the same role", "No_of_years_of_previous_experience_in_the_same_role");
                jsdata = jsdata.Replace("Total years of previous experience", "Total_years_of_previous_experience");
                jsdata = jsdata.Replace("Performance in the previous company(any major achievements / awards received / exceptional growth, etc.)", "Performance_in_the_previous_company");
                jsdata = jsdata.Replace("Background verification Conducted or not", "background_Conducted_or_not");
                jsdata = jsdata.Replace("If yes, what is the feedback 1", "background_If_yes_what_is_the_feedback");
                jsdata = jsdata.Replace("Health Check-up Conducted or not", "health_Conducted_or_not");
                jsdata = jsdata.Replace("If yes, what is the feedback 2", "health_If_yes_what_is_the_feedback");
                jsdata = jsdata.Replace("Reference check with the previous company Conducted or not", "Reference_Conducted_or_not");


                jsdata = jsdata.Replace("If yes, what is the feedback 3", "Reference_If_yes_what_is_the_feedback");
                jsdata = jsdata.Replace("Aptitude test score(if conducted)", "Aptitude_test_score");

                //jsdata = jsdata.Replace("percentage of salary increase offered", "percentage_of_salary_increase_offered");

                jsdata = jsdata.Replace("Perfect Match or not", "Perfect_Match_or_not");
                jsdata = jsdata.Replace("If no details", "If_no_details");


                //string st = "{\"excelUpdtData\":[{\"PositionId\":\"NA\",\"Reporting_Manager\":\"Jain Jose K\"}]}\r\n";
                var result5 = JObject.Parse(jsdata);

                var items1 = result5["excelUpdtData"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
                //string sd2 = "[" + jsonString2 + "]";

                var model2 = JsonConvert.DeserializeObject<List<Models.recruitmentExcelModel>>(jsonString2);



                //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1[0]);
                //string sd2 = "[" + jsonString2 + "]";

                //var model2 = JsonConvert.DeserializeObject<List<Models.recruitmentExcelModel>>(sd2);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_recruitment_insert";
                        sqlBulkCopy.ColumnMappings.Add("positionID", "positionID");
                        sqlBulkCopy.ColumnMappings.Add("Date_on_which_the_position_became_vacant", "Date_on_which_the_position_became_vacant");
                        sqlBulkCopy.ColumnMappings.Add("Date_on_which_profiles_are_shared", "Date_on_which_profiles_are_shared");
                        sqlBulkCopy.ColumnMappings.Add("No_of_profiles_shared", "No_of_profiles_shared");
                        sqlBulkCopy.ColumnMappings.Add("Dates_on_which_the_profiles_are_shortlisted_by_the_interviewer", "Dates_on_which_the_profiles_are_shortlisted_by_the_interviewer");
                        sqlBulkCopy.ColumnMappings.Add("No_of_profiles_shortlisted", "No_of_profiles_shortlisted");
                        sqlBulkCopy.ColumnMappings.Add("Date_on_which_interview_are_scheduled", "Date_on_which_interview_are_scheduled");
                        sqlBulkCopy.ColumnMappings.Add("Name_of_the_Interviewer", "Name_of_the_Interviewer");
                        sqlBulkCopy.ColumnMappings.Add("No_of_persons_interviewed", "No_of_persons_interviewed");

                        sqlBulkCopy.ColumnMappings.Add("No_of_offer_letters_issued", "No_of_offer_letters_issued");
                        sqlBulkCopy.ColumnMappings.Add("Job_portals", "Job_portals");
                        sqlBulkCopy.ColumnMappings.Add("Consultants", "Consultants");

                        sqlBulkCopy.ColumnMappings.Add("Employee_Referrals", "Employee_Referrals");
                        sqlBulkCopy.ColumnMappings.Add("Social_media_posting", "Social_media_posting");
                        sqlBulkCopy.ColumnMappings.Add("Companys_career_page", "Companys_career_page");
                        sqlBulkCopy.ColumnMappings.Add("Internal_promotion", "Internal_promotion");
                        sqlBulkCopy.ColumnMappings.Add("Date_of_Issuance", "Date_of_Issuance");
                        sqlBulkCopy.ColumnMappings.Add("Date_of_Acceptance", "Date_of_Acceptance");
                        sqlBulkCopy.ColumnMappings.Add("Date_of_Rejection", "Date_of_Rejection");
                        sqlBulkCopy.ColumnMappings.Add("In_case_of_rejection_reason", "In_case_of_rejection_reason");
                        sqlBulkCopy.ColumnMappings.Add("Date_on_which_the_new_candidate_onboarded", "Date_on_which_the_new_candidate_onboarded");
                        sqlBulkCopy.ColumnMappings.Add("Recruitment_Age", "Recruitment_Age");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("Educational_Qualification", "Educational_Qualification");
                        sqlBulkCopy.ColumnMappings.Add("The_candidate_has_not_performed", "The_candidate_has_not_performed");
                        sqlBulkCopy.ColumnMappings.Add("No_of_months_achieve_minimum_no", "No_of_months_achieve_minimum_no");
                        sqlBulkCopy.ColumnMappings.Add("No_of_months_it_took_for_the_candidate_to_achieve_100_percent_target", "No_of_months_it_took_for_the_candidate_to_achieve_100_percent_target");
                        sqlBulkCopy.ColumnMappings.Add("Hiring_Manager_Satisfaction", "Hiring_Manager_Satisfaction");
                        sqlBulkCopy.ColumnMappings.Add("Candidates_job_satisfaction", "Candidates_job_satisfaction");



                        sqlBulkCopy.ColumnMappings.Add("one_Voluntary", "one_Voluntary");
                        sqlBulkCopy.ColumnMappings.Add("one_involuntary", "one_involuntary");
                        sqlBulkCopy.ColumnMappings.Add("one_abscond", "one_abscond");
                        sqlBulkCopy.ColumnMappings.Add("two_Voluntary", "two_Voluntary");
                        sqlBulkCopy.ColumnMappings.Add("two_involuntary", "two_involuntary");
                        sqlBulkCopy.ColumnMappings.Add("two_abscond", "two_abscond");
                        sqlBulkCopy.ColumnMappings.Add("three_Voluntary", "three_Voluntary");
                        sqlBulkCopy.ColumnMappings.Add("three_involuntary", "three_involuntary");
                        sqlBulkCopy.ColumnMappings.Add("three_abscond", "three_abscond");
                        sqlBulkCopy.ColumnMappings.Add("four_Voluntary", "four_Voluntary");
                        sqlBulkCopy.ColumnMappings.Add("four_involuntary", "four_involuntary");
                        sqlBulkCopy.ColumnMappings.Add("four_abscond", "four_abscond");
                        sqlBulkCopy.ColumnMappings.Add("No_of_years_of_previous_experience_in_the_same_role", "No_of_years_of_previous_experience_in_the_same_role");
                        sqlBulkCopy.ColumnMappings.Add("Total_years_of_previous_experience", "Total_years_of_previous_experience");
                        sqlBulkCopy.ColumnMappings.Add("Performance_in_the_previous_company", "Performance_in_the_previous_company");
                        sqlBulkCopy.ColumnMappings.Add("background_Conducted_or_not", "background_Conducted_or_not");
                        sqlBulkCopy.ColumnMappings.Add("background_If_yes_what_is_the_feedback", "background_If_yes_what_is_the_feedback");


                        sqlBulkCopy.ColumnMappings.Add("health_Conducted_or_not", "health_Conducted_or_not");
                        sqlBulkCopy.ColumnMappings.Add("health_If_yes_what_is_the_feedback", "health_If_yes_what_is_the_feedback");
                        sqlBulkCopy.ColumnMappings.Add("Reference_Conducted_or_not", "Reference_Conducted_or_not");
                        sqlBulkCopy.ColumnMappings.Add("Reference_If_yes_what_is_the_feedback", "Reference_If_yes_what_is_the_feedback");
                        sqlBulkCopy.ColumnMappings.Add("Aptitude_test_score", "Aptitude_test_score");
                        // sqlBulkCopy.ColumnMappings.Add("percentage_of_salary_increase_offered", "percentage_of_salary_increase_offered");
                        sqlBulkCopy.ColumnMappings.Add("Perfect_Match_or_not", "Perfect_Match_or_not");
                        sqlBulkCopy.ColumnMappings.Add("If_no_details", "If_no_details");
                        sqlBulkCopy.ColumnMappings.Add("cremarks1", "cremarks1");
                        sqlBulkCopy.ColumnMappings.Add("cremarks2", "cremarks2");
                        sqlBulkCopy.ColumnMappings.Add("cremarks3", "cremarks3");
                        sqlBulkCopy.ColumnMappings.Add("CCREATEDBY", "CCREATEDBY");
                        sqlBulkCopy.ColumnMappings.Add("LDATE", "LDATE");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }




                return Ok(200);

            }
            catch (Exception e)
            {

            }

            return Ok("201");
        }

        [Route("PostEmployeeLeaveApply")]
        [HttpPost]
        public async Task<IActionResult> PostEmployeeLeaveApply(leaveApply empId)
        {

            try
            {

                int employeeId = int.Parse(empId.employeeNo);
                empId.employeeNo = employeeId.ToString();
            }
            catch (Exception e)
            {
                return BadRequest($"Error : {e.Message}");
            }



            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query2 = "update tbl_leave_master set cstatus=@cstatus where cempno=@cempno and ndocno=@ndocno";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@cempno", empId.employeeNo ?? "");
                    cmd2.Parameters.AddWithValue("@ndocno", empId.ndocno ?? "");
                    cmd2.Parameters.AddWithValue("@cstatus", empId.status);


                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }
            // empId.employeeNo

            if (empId.status == "Rejected")
            {

                string result5 = empId.status + " Successfully";
                List<string> objresponse1 = new List<string>();
                objresponse1.Add(result5);

                //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

                string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

                return new JsonResult(op12);

            }
            else if ((empId.status == "Approved") && (empId.leaveTypeDescription == "Permission"))
            {

                DataSet ds1 = new DataSet();
                string query1 = "sp_GreythrHRPortal";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", "ptime");
                        cmd.Parameters.AddWithValue("@FilterValue2", empId.ndocno);
                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds1);
                        con.Close();
                    }
                }
                string op = JsonConvert.SerializeObject(ds1.Tables[0], Formatting.Indented);
                List<TimeRange> timeRanges = JsonConvert.DeserializeObject<List<TimeRange>>(op);


                DateTime fromDate = Convert.ToDateTime(empId.fromDate);
                empId.fromDate = fromDate.ToString("yyyy-MM-dd");

                DataSet ds = new DataSet();
                string query = "sp_GreythrHRPortal";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", "permission");
                        cmd.Parameters.AddWithValue("@FilterValue2", empId.employeeNo);
                        cmd.Parameters.AddWithValue("@FilterValue3", empId.fromDate);
                        //cmd.Parameters.AddWithValue("@FilterValue4", empId.starttime);
                        //cmd.Parameters.AddWithValue("@FilterValue5", empId.endtime);
                        cmd.Parameters.AddWithValue("@FilterValue4", timeRanges[0].starttime);
                        cmd.Parameters.AddWithValue("@FilterValue5", timeRanges[0].endtime);


                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                string result5 = "Permission " + empId.status + " Successfully";
                List<string> objresponse1 = new List<string>();
                objresponse1.Add(result5);


                string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);
                return new JsonResult(op12);


                //string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

                //return new JsonResult(op);

                //string result5 = "Permission " + empId.status + " Successfully";
                //List<string> objresponse1 = new List<string>();
                //objresponse1.Add(result5);
                //DataSet ds22 = new DataSet();
                //string dsquery11 = "sp_GreythrHRPortal";
                //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                //{

                //    using (SqlCommand cmd = new SqlCommand(dsquery11))
                //    {
                //        cmd.Connection = con;
                //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //        cmd.Parameters.AddWithValue("@FilterValue1", "permission");
                //        cmd.Parameters.AddWithValue("@FilterValue2", empId.employeeNo);
                //        cmd.Parameters.AddWithValue("@FilterValue3", empId.fromDate);
                //        cmd.Parameters.AddWithValue("@FilterValue4", empId.starttime ?? "");
                //        cmd.Parameters.AddWithValue("@FilterValue5", empId.endtime ?? "");
                //        con.Open();
                //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //        // adapter.Fill(ds2);
                //        con.Close();
                //    }
                //}



                //string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

                //return new JsonResult(op12);




            }
            else if ((empId.status == "Rejected") && (empId.leaveTypeDescription == "Permission"))
            {
                string result5 = "Permission " + empId.status + " Successfully";
                List<string> objresponse1 = new List<string>();
                objresponse1.Add(result5);



                string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

                return new JsonResult(op12);
            }
            else if (empId.status == "Withdraw")
            {

                DataSet ds = new DataSet();
                string query = "sp_GreythrHRPortal";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", empId.employeeNo);
                        cmd.Parameters.AddWithValue("@FilterValue2", "");
                        cmd.Parameters.AddWithValue("@FilterValue3", "LeaveWithdraw");
                        cmd.Parameters.AddWithValue("@FilterValue4", empId.finyear);
                        cmd.Parameters.AddWithValue("@FilterValue5", empId.ndocno);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }
                string result5 = empId.status + " Successfully";
                List<string> objresponse1 = new List<string>();
                objresponse1.Add(result5);


                string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

                return new JsonResult(op12);
            }


            else if (empId.status == "Approved")
            {

                string empmaxno = string.Empty;



                string gempid = string.Empty;
                string Gyear = DateTime.Now.Year.ToString();



                Token token = new Token();

                var client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response1 = await client.PostAsync(baseAddress, null);

                var result1 = await response1.Content.ReadAsStringAsync();


                var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

                JObject studentObj1 = JObject.Parse(JsonContent1);

                var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

                var result23 = JObject.Parse(sd);
                var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

                token.access_token = (string)items12[0];



                var client2 = new HttpClient();


                client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
                client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


                var url1 = "https://api.greythr.com/leave/v2/employee/transactions";
                if (empId.fromSession == "Session 1")
                {
                    empId.fromSession = "1";
                }
                if (empId.fromSession == "Session 2")
                {
                    empId.fromSession = "2";
                }
                if (empId.toSession == "Session 1")
                {
                    empId.toSession = "1";
                }
                if (empId.toSession == "Session 2")
                {
                    empId.toSession = "2";
                }
                grthhrApply objleave = new grthhrApply();
                objleave.employeeNo = empId.employeeNo;
                DateTime fromDate = Convert.ToDateTime(empId.fromDate);
                DateTime toDate = Convert.ToDateTime(empId.toDate);
                objleave.fromDate = fromDate.ToString("yyyy-MM-dd");
                objleave.toDate = toDate.ToString("yyyy-MM-dd");

                //ToString("yyyy/MM/dd");
                objleave.leaveTypeDescription = empId.leaveTypeDescription;
                objleave.leaveTransactionTypeDescription = "Availed";
                objleave.fromSession = int.Parse(empId.fromSession);
                objleave.toSession = int.Parse(empId.toSession);
                objleave.days = empId.days;
                objleave.ignoreLeaveRule = false;
                objleave.reason = empId.reason;


                var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(objleave);
                var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");

                //  Lin
                var response2 = await client2.PostAsync(url1, data2);
                string result41 = response2.Content.ReadAsStringAsync().Result;

                // string getstr= 

                // string result41 = "";
                DataSet ds3 = new DataSet();

                string opjsonresult = JsonConvert.SerializeObject(empId, Formatting.Indented);

                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_Greyhrlog(employeeNo,fromDate,toDate,ndocno,fromSession,toSession,apiresponse,createddate) values (@employeeNo,@fromDate,@toDate,@ndocno,@fromSession,@toSession,@apiresponse,@createddate)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@employeeNo", empId.employeeNo);
                        cmd1.Parameters.AddWithValue("@fromDate", empId.fromDate);
                        cmd1.Parameters.AddWithValue("@toDate", empId.toDate);
                        cmd1.Parameters.AddWithValue("@ndocno", empId.ndocno);
                        cmd1.Parameters.AddWithValue("@fromSession", empId.fromSession);
                        cmd1.Parameters.AddWithValue("@toSession", empId.toSession);
                        cmd1.Parameters.AddWithValue("@apiresponse", opjsonresult);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

                        //cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }



                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_Greyhrlog(employeeNo,fromDate,toDate,ndocno,fromSession,toSession,apiresponse,createddate) values (@employeeNo,@fromDate,@toDate,@ndocno,@fromSession,@toSession,@apiresponse,@createddate)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@employeeNo", empId.employeeNo);
                        cmd1.Parameters.AddWithValue("@fromDate", empId.fromDate);
                        cmd1.Parameters.AddWithValue("@toDate", empId.toDate);
                        cmd1.Parameters.AddWithValue("@ndocno", empId.ndocno);
                        cmd1.Parameters.AddWithValue("@fromSession", empId.fromSession);
                        cmd1.Parameters.AddWithValue("@toSession", empId.toSession);
                        cmd1.Parameters.AddWithValue("@apiresponse", result41);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

                        //cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }




            }




            string result3 = empId.status + " Successfully";
            List<string> objresponse = new List<string>();
            objresponse.Add(result3);

            //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

            string op1 = JsonConvert.SerializeObject(objresponse, Formatting.Indented);

            return new JsonResult(op1);



        }



        //[Route("PostEmployeeLeaveApply")]
        //[HttpPost]
        //public async Task<IActionResult> PostEmployeeLeaveApply(leaveApply empId)
        //{

        //    try
        //    {

        //        int employeeId = int.Parse(empId.employeeNo);
        //        empId.employeeNo = employeeId.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest($"Error : {e.Message}");
        //    }


        //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        string query2 = "update tbl_leave_master set cstatus=@cstatus where cempno=@cempno and ndocno=@ndocno";
        //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //        {
        //            cmd2.Parameters.AddWithValue("@cempno", empId.employeeNo ?? "");
        //            cmd2.Parameters.AddWithValue("@ndocno", empId.ndocno ?? "");
        //            cmd2.Parameters.AddWithValue("@cstatus", empId.status);


        //            con2.Open();
        //            int iii = cmd2.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                //return StatusCode(200);
        //            }
        //            con2.Close();
        //        }
        //    }
        //    // empId.employeeNo

        //    if (empId.status == "Rejected")
        //    {

        //        string result5 = empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);

        //    }
        //    else if ((empId.status == "Approved") && (empId.leaveTypeDescription == "Permission"))
        //    {
        //        string result5 = "Permission " + empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);
        //    }
        //    else if ((empId.status == "Rejected") && (empId.leaveTypeDescription == "Permission"))
        //    {
        //        string result5 = "Permission " + empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);
        //    }

        //    else if (empId.status == "Approved")
        //    {

        //        string empmaxno = string.Empty;



        //        string gempid = string.Empty;
        //        string Gyear = DateTime.Now.Year.ToString();



        //        Token token = new Token();

        //        var client = new HttpClient();

        //        var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //        var response1 = await client.PostAsync(baseAddress, null);

        //        var result1 = await response1.Content.ReadAsStringAsync();


        //        var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

        //        JObject studentObj1 = JObject.Parse(JsonContent1);

        //        var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //        var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //        string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

        //        var result23 = JObject.Parse(sd);
        //        var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

        //        token.access_token = (string)items12[0];



        //        var client2 = new HttpClient();


        //        client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
        //        client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


        //        var url1 = "https://api.greythr.com/leave/v2/employee/transactions";
        //        if (empId.fromSession == "Session 1")
        //        {
        //            empId.fromSession = "1";
        //        }
        //        if (empId.fromSession == "Session 2")
        //        {
        //            empId.fromSession = "2";
        //        }
        //        if (empId.toSession == "Session 1")
        //        {
        //            empId.toSession = "1";
        //        }
        //        if (empId.toSession == "Session 2")
        //        {
        //            empId.toSession = "2";
        //        }
        //        grthhrApply objleave = new grthhrApply();
        //        objleave.employeeNo = empId.employeeNo;
        //        DateTime fromDate = Convert.ToDateTime(empId.fromDate);
        //        DateTime toDate = Convert.ToDateTime(empId.toDate);
        //        objleave.fromDate = fromDate.ToString("yyyy-MM-dd");
        //        objleave.toDate = toDate.ToString("yyyy-MM-dd");

        //        //ToString("yyyy/MM/dd");
        //        objleave.leaveTypeDescription = empId.leaveTypeDescription;
        //        objleave.leaveTransactionTypeDescription = "Availed";
        //        objleave.fromSession = int.Parse(empId.fromSession);
        //        objleave.toSession = int.Parse(empId.toSession);
        //        objleave.days = empId.days;
        //        objleave.ignoreLeaveRule = false;
        //        objleave.reason = empId.reason;


        //        var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(objleave);
        //        var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");

        //        //  Lin
        //        var response2 = await client2.PostAsync(url1, data2);
        //        string result41 = response2.Content.ReadAsStringAsync().Result;

        //        // string getstr= 

        //        // string result41 = "";
        //        DataSet ds3 = new DataSet();

        //        string opjsonresult = JsonConvert.SerializeObject(empId, Formatting.Indented);

        //        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query1 = "insert into tbl_Greyhrlog(employeeNo,fromDate,toDate,ndocno,fromSession,toSession,apiresponse,createddate) values (@employeeNo,@fromDate,@toDate,@ndocno,@fromSession,@toSession,@apiresponse,@createddate)";


        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@employeeNo", empId.employeeNo);
        //                cmd1.Parameters.AddWithValue("@fromDate", empId.fromDate);
        //                cmd1.Parameters.AddWithValue("@toDate", empId.toDate);
        //                cmd1.Parameters.AddWithValue("@ndocno", empId.ndocno);
        //                cmd1.Parameters.AddWithValue("@fromSession", empId.fromSession);
        //                cmd1.Parameters.AddWithValue("@toSession", empId.toSession);
        //                cmd1.Parameters.AddWithValue("@apiresponse", opjsonresult);
        //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                //cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }



        //        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query1 = "insert into tbl_Greyhrlog(employeeNo,fromDate,toDate,ndocno,fromSession,toSession,apiresponse,createddate) values (@employeeNo,@fromDate,@toDate,@ndocno,@fromSession,@toSession,@apiresponse,@createddate)";


        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@employeeNo", empId.employeeNo);
        //                cmd1.Parameters.AddWithValue("@fromDate", empId.fromDate);
        //                cmd1.Parameters.AddWithValue("@toDate", empId.toDate);
        //                cmd1.Parameters.AddWithValue("@ndocno", empId.ndocno);
        //                cmd1.Parameters.AddWithValue("@fromSession", empId.fromSession);
        //                cmd1.Parameters.AddWithValue("@toSession", empId.toSession);
        //                cmd1.Parameters.AddWithValue("@apiresponse", result41);
        //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                //cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }




        //    }

        //    string result3 = empId.status + " Successfully";
        //    List<string> objresponse = new List<string>();
        //    objresponse.Add(result3);

        //    //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //    string op1 = JsonConvert.SerializeObject(objresponse, Formatting.Indented);

        //    return new JsonResult(op1);



        //}


        //[Route("PostEmployeeLeaveApply")]
        //[HttpPost]
        //public async Task<IActionResult> PostEmployeeLeaveApply(leaveApply empId)
        //{




        //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        string query2 = "update tbl_leave_master set cstatus=@cstatus where cempno=@cempno and ndocno=@ndocno";
        //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //        {
        //            cmd2.Parameters.AddWithValue("@cempno", empId.employeeNo ?? "");
        //            cmd2.Parameters.AddWithValue("@ndocno", empId.ndocno ?? "");
        //            cmd2.Parameters.AddWithValue("@cstatus", empId.status);


        //            con2.Open();
        //            int iii = cmd2.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                //return StatusCode(200);
        //            }
        //            con2.Close();
        //        }
        //    }
        //    // empId.employeeNo

        //    if (empId.status == "Rejected")
        //    {

        //        string result5 = empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);

        //    }
        //    else if ((empId.status == "Approved") && (empId.leaveTypeDescription == "Permission"))
        //    {
        //        string result5 = "Permission " + empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);
        //    }
        //    else if ((empId.status == "Rejected") && (empId.leaveTypeDescription == "Permission"))
        //    {
        //        string result5 = "Permission " + empId.status + " Successfully";
        //        List<string> objresponse1 = new List<string>();
        //        objresponse1.Add(result5);

        //        //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //        string op12 = JsonConvert.SerializeObject(objresponse1, Formatting.Indented);

        //        return new JsonResult(op12);
        //    }

        //    else if (empId.status == "Approved")
        //    {

        //        string empmaxno = string.Empty;



        //        string gempid = string.Empty;
        //        string Gyear = DateTime.Now.Year.ToString();



        //        Token token = new Token();

        //        var client = new HttpClient();

        //        var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //        var response1 = await client.PostAsync(baseAddress, null);

        //        var result1 = await response1.Content.ReadAsStringAsync();


        //        var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

        //        JObject studentObj1 = JObject.Parse(JsonContent1);

        //        var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //        var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //        string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

        //        var result23 = JObject.Parse(sd);
        //        var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

        //        token.access_token = (string)items12[0];



        //        var client2 = new HttpClient();


        //        client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
        //        client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


        //        var url1 = "https://api.greythr.com/leave/v2/employee/transactions";
        //        if (empId.fromSession == "Session 1")
        //        {
        //            empId.fromSession = "1";
        //        }
        //        if (empId.fromSession == "Session 2")
        //        {
        //            empId.fromSession = "2";
        //        }
        //        if (empId.toSession == "Session 1")
        //        {
        //            empId.toSession = "1";
        //        }
        //        if (empId.toSession == "Session 2")
        //        {
        //            empId.toSession = "2";
        //        }
        //        grthhrApply objleave = new grthhrApply();
        //        objleave.employeeNo = empId.employeeNo;
        //        DateTime fromDate = Convert.ToDateTime(empId.fromDate);
        //        DateTime toDate = Convert.ToDateTime(empId.toDate);
        //        objleave.fromDate = fromDate.ToString("yyyy/MM/dd");
        //        objleave.toDate = toDate.ToString("yyyy/MM/dd");

        //        //ToString("yyyy/MM/dd");
        //        objleave.leaveTypeDescription = empId.leaveTypeDescription;
        //        objleave.leaveTransactionTypeDescription = "Availed";
        //        objleave.fromSession = int.Parse(empId.fromSession);
        //        objleave.toSession = int.Parse(empId.toSession);
        //        objleave.days = empId.days;
        //        objleave.ignoreLeaveRule = false;
        //        objleave.reason = empId.reason;

        //        //var SaveRequestBody3 = new Dictionary<string, string>
        //        //{
        //        //{ "employeeNo",empId.employeeNo},
        //        //{ "fromDate",empId.fromDate},
        //        //{ "toDate",empId.toDate},
        //        //{ "leaveTypeDescription",empId.leaveTypeDescription},
        //        //{ "leaveTransactionTypeDescription",empId.leaveTransactionTypeDescription},
        //        //{ "fromSession",empId.fromSession},
        //        //{ "toSession",empId.toSession},
        //        //{ "days",empId.days},
        //        //{ "ignoreLeaveRule",empId.ignoreLeaveRule},
        //        //{ "reason",empId.reason},

        //        //};
        //        var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(objleave);
        //        var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");

        //        //  Lin
        //        var response2 = await client2.PostAsync(url1, data2);
        //        string result41 = response2.Content.ReadAsStringAsync().Result;
        //        // string result41 = "";
        //        DataSet ds3 = new DataSet();

        //        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query1 = "insert into tbl_Greyhrlog(employeeNo,fromDate,toDate,ndocno,fromSession,toSession,apiresponse,createddate) values (@employeeNo,@fromDate,@toDate,@ndocno,@fromSession,@toSession,@apiresponse,@createddate)";


        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@employeeNo", empId.employeeNo);
        //                cmd1.Parameters.AddWithValue("@fromDate", empId.fromDate);
        //                cmd1.Parameters.AddWithValue("@toDate", empId.toDate);
        //                cmd1.Parameters.AddWithValue("@ndocno", empId.ndocno);
        //                cmd1.Parameters.AddWithValue("@fromSession", empId.fromSession);
        //                cmd1.Parameters.AddWithValue("@toSession", empId.toSession);
        //                cmd1.Parameters.AddWithValue("@apiresponse", result41);
        //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                //cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }




        //    }




        //    string result3 = empId.status + " Successfully";
        //    List<string> objresponse = new List<string>();
        //    objresponse.Add(result3);

        //    //var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

        //    string op1 = JsonConvert.SerializeObject(objresponse, Formatting.Indented);

        //    return new JsonResult(op1);



        //}




        [Route("GetEmployeeLeaveBalance")]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeLeaveBalance(Param empId)
        {
            try
            {

            
            string gempid = string.Empty;
            string Gyear = DateTime.Now.Year.ToString();
            //  string INPUTID = empId.Tostring();
            DataSet ds12 = new DataSet();
            string dsquery2 = "sp_mis_grathremployee";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery2))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", empId.filtervalue1);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds12);
                    con.Close();
                }
            }
            if (ds12.Tables[0].Rows.Count > 0)
            {

                gempid = ds12.Tables[0].Rows[0][0].ToString();
            }






            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            var url1 = "https://api.greythr.com/leave/v2/employee/" + gempid + "/years/" + Gyear + "/balance";

            var response3 = await client2.GetAsync(url1);


            var result3 = await response3.Content.ReadAsStringAsync();


            string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

            var result5 = JObject.Parse(result3);

            var items3 = result5["list"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

            var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
            var model2 = JsonConvert.DeserializeObject<List<Models.balanceList1>>(jsonString3);

            string op = JsonConvert.SerializeObject(model2, Formatting.Indented);

            return Ok(op);

            }
            catch (Exception)
            {

            }
            return Ok(201);

        }

        [HttpPost]
        [Route("hrApprovalReport")]
        public ActionResult hrApprovalReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_hrapproval";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
                    cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
                    cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
                    cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
                    cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
                    cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                    cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }
        [HttpPost]
        [Route("attendanceSummary")]
        public ActionResult attendanceSummaryReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_attendance_summary";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
                    cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
                    cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
                    cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
                    cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
                    cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                    cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }




        [HttpPost]
        [Route("SaveEmployeeLeaveDtls")]
        public ActionResult<tbl_leave_master> SaveEmployeeLeaveDtls(List<tbl_leave_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int maxno1 = 0;
            DataSet ds2 = new DataSet();
            string dsquery1 = "sp_Get_Leavestatus";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel[0].cempno);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel[0].creportingmanager);
                    cmd.Parameters.AddWithValue("@FilterValue3", prsModel[0].lfromdate);
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel[0].ltodate);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel[0].csession1);
                    cmd.Parameters.AddWithValue("@FilterValue6", prsModel[0].csession2);
                    cmd.Parameters.AddWithValue("@FilterValue7", prsModel[0].cleavetype);
                    cmd.Parameters.AddWithValue("@FilterValue8", prsModel[0].Lduration);
                    cmd.Parameters.AddWithValue("@FilterValue9", prsModel[0].starttime);
                    cmd.Parameters.AddWithValue("@FilterValue10", prsModel[0].endtime);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds2);
                    con.Close();
                }
            }
            maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
            if (maxno1 == 1)
            {

                // return StatusCode(201);

                string result33 = "Already Applied";

                return StatusCode(201, result33);


            }

            else if (maxno1 == 2)
            {

                // return StatusCode(201);

                string result34 = "Leave Allowed Maximum Three Days";

                return StatusCode(202, result34);


            }

            else if (maxno1 == 3)
            {

                // return StatusCode(201);

                string result35 = "Permission Allowed Maximum two hours";

                return StatusCode(203, result35);


            }
            else if (maxno1 == 4)
            {

                // return StatusCode(201);

                string result36 = "Permission Allowed Maximum four Days";

                return StatusCode(204, result36);


            }
            else if (maxno1 == 5)
            {
                // return StatusCode(201);

                string result36 = "Please Select Session 1 and Session 2";

                return StatusCode(205, result36);

            }
            else if (maxno1 == 7)
            {
                
                string result36 = "Prevews month is locked";

                return StatusCode(207, result36);

            }
            else if ((prsModel[0].cleavetype == "Comp - Off") && (prsModel[0].Lduration == "0.5"))
            {

                string result36 = "Please select minimum 1.0 days to apply for Comp - Off";

                return StatusCode(208, result36);

            }

            else if (maxno1 == 0)
            {

                for (int i = 0; i < prsModel.Count; i++)
                {
                    int maxno = 0;

                    string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_leave_master";
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlCommand cmdd = new SqlCommand(que))
                        {
                            cmdd.Connection = con;
                            con.Open();
                            using (SqlDataReader sdr = cmdd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    maxno = Convert.ToInt32(sdr["Maxno"]);
                                }
                            }
                            con.Close();
                        }
                    }

                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        string query2 = "insert into tbl_leave_master values (@ccomcode,@cloccode,@corgcode," +
                                                   "@cfincode," +
                                                   "@cdoctype,@ndocno,@ldocdate,@cempno,@creportingmanager,@cleavetype," +
                                                   "@cavailableleave,@lfromdate,@ltodate,@csession1,@csession2,@Lduration,@creason,@cattachment,@ccreatedby," +
                                                   "@lcreateddate,@cmodifedby,@lmodifieddate,@cstatus,@cremarks1,@cremarks2,@cremarks3,@mobileno,@starttime,@endtime)";
                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                        {
                            cmd2.Parameters.AddWithValue("@ccomcode", prsModel[i].ccomcode ?? "");
                            cmd2.Parameters.AddWithValue("@cloccode", prsModel[i].cloccode ?? "");
                            cmd2.Parameters.AddWithValue("@corgcode", prsModel[i].corgcode ?? "");
                            cmd2.Parameters.AddWithValue("@cfincode", prsModel[i].cfincode ?? "");
                            cmd2.Parameters.AddWithValue("@cdoctype", prsModel[i].cdoctype ?? "");
                            cmd2.Parameters.AddWithValue("@ndocno", maxno);
                            cmd2.Parameters.AddWithValue("@ldocdate", prsModel[i].ldocdate ?? DateTime.Now);


                            cmd2.Parameters.AddWithValue("@cempno", prsModel[i].cempno);
                            cmd2.Parameters.AddWithValue("@creportingmanager", prsModel[i].creportingmanager);
                            cmd2.Parameters.AddWithValue("@cleavetype", prsModel[i].cleavetype);
                            cmd2.Parameters.AddWithValue("@cavailableleave", prsModel[i].cavailableleave ?? "");
                            cmd2.Parameters.AddWithValue("@lfromdate", prsModel[i].lfromdate);
                            cmd2.Parameters.AddWithValue("@ltodate", prsModel[i].ltodate);
                            cmd2.Parameters.AddWithValue("@csession1", prsModel[i].csession1);
                            cmd2.Parameters.AddWithValue("@csession2", prsModel[i].csession2);
                            cmd2.Parameters.AddWithValue("@Lduration", prsModel[i].Lduration ?? "");



                            cmd2.Parameters.AddWithValue("@creason", prsModel[i].creason ?? "");
                            cmd2.Parameters.AddWithValue("@cattachment", prsModel[i].cattachment ?? "");
                            cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[i].ccreatedby);
                            cmd2.Parameters.AddWithValue("@lcreateddate", DateTime.Now);

                            cmd2.Parameters.AddWithValue("@cmodifedby", prsModel[i].cmodifedby ?? "");
                            cmd2.Parameters.AddWithValue("@lmodifieddate", DateTime.Now);
                            cmd2.Parameters.AddWithValue("@cstatus", "Pending");
                            cmd2.Parameters.AddWithValue("@cremarks1", prsModel[i].cremarks1 ?? "");
                            cmd2.Parameters.AddWithValue("@cremarks2", prsModel[i].cremarks2 ?? "");
                            cmd2.Parameters.AddWithValue("@cremarks3", prsModel[i].cremarks3 ?? "");
                            cmd2.Parameters.AddWithValue("@mobileno", prsModel[i].mobileno ?? "");

                            // if(prsModel[i].cleavetype == "Permision")
                            // {
                            cmd2.Parameters.AddWithValue("@starttime", prsModel[i].starttime ?? "");
                            cmd2.Parameters.AddWithValue("@endtime", prsModel[i].endtime ?? "");
                            // }

                            //@ctemp7,@ctemp8,@ctemp9
                            //  @starttime,@endtime

                            con2.Open();
                            int iii = cmd2.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                //return StatusCode(200);
                            }
                            con2.Close();
                        }
                    }
                }
                // return BadRequest();
                //  return StatusCode(200,"Data Saved Successfully");
                // return Ok("Data Saved Successfully");
                //  return Ok(200, "Data Saved Successfully");
                //string result3 = "{'saved':Data Saved Successfully}";

                string result3 = "Data Saved Successfully";

                try
                {


                    DataSet ds22 = new DataSet();
                    string dsquery11 = "sp_GreythrHRPortal";
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        using (SqlCommand cmd = new SqlCommand(dsquery11))
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FilterValue1", prsModel[0].cempno);
                            cmd.Parameters.AddWithValue("@FilterValue2", "");
                            // cmd.Parameters.AddWithValue("@FilterValue2", prsModel[0].creportingmanager);
                            cmd.Parameters.AddWithValue("@FilterValue3", "leavealert");
                            cmd.Parameters.AddWithValue("@FilterValue4", prsModel[0].cfincode);
                            con.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(ds2);
                            con.Close();
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                return StatusCode(200, result3);


            }




            return StatusCode(210);



        }

        [HttpPost]
        [Route("jobDescription")]
        public ActionResult GetAllPaints()
        {
            try
            {


                DataSet ds = new DataSet();
                string query = "sp_get_job_description";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


                return new JsonResult(op);
            }
            catch (Exception)
            {


            }
            return Ok(500);

        }


        // Get All Employees Details
        [Route("AllEmployeeDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeDtls()
        {
            try
            {


                Token token = new Token();

                var client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response1 = await client.PostAsync(baseAddress, null);

                var result1 = await response1.Content.ReadAsStringAsync();


                var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

                JObject studentObj1 = JObject.Parse(JsonContent1);

                var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

                var result23 = JObject.Parse(sd);
                var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

                token.access_token = (string)items12[0];



                var client2 = new HttpClient();


                client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
                client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


                DataSet ds3 = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "delete from tbl_grathrportal";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {


                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }



                int pagecount = 0;


                for (int i = 0; i <= pagecount; i++)
                {

                    var url1 = "https://api.greythr.com/employee/v2/employees?page=" + pagecount + "&size=2000";

                    var response3 = await client2.GetAsync(url1);


                    var result3 = await response3.Content.ReadAsStringAsync();


                    string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                    var result5 = JObject.Parse(result3);

                    var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                    var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                    var model2 = JsonConvert.DeserializeObject<List<Models.AllEmpDtlsmodel>>(jsonString3);

                    DataTable dt2 = new DataTable();

                    dt2 = CreateDataTable(model2);
                    JobRoot objclass2 = new JobRoot();
                    if (model2.Count == 0)
                    {
                        return Ok(200);
                    }

                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "tbl_grathrportal";
                            sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                            sqlBulkCopy.ColumnMappings.Add("name", "name");
                            sqlBulkCopy.ColumnMappings.Add("firstName", "firstName");
                            sqlBulkCopy.ColumnMappings.Add("middleName", "middleName");
                            sqlBulkCopy.ColumnMappings.Add("lastName", "lastName");
                            sqlBulkCopy.ColumnMappings.Add("email", "email");
                            sqlBulkCopy.ColumnMappings.Add("employeeNo", "employeeNo");
                            sqlBulkCopy.ColumnMappings.Add("dateOfJoin", "dateOfJoin");
                            sqlBulkCopy.ColumnMappings.Add("leavingDate", "leavingDate");

                            sqlBulkCopy.ColumnMappings.Add("originalHireDate", "originalHireDate");
                            sqlBulkCopy.ColumnMappings.Add("leftorg", "leftorg");
                            sqlBulkCopy.ColumnMappings.Add("lastModified", "lastModified");
                            sqlBulkCopy.ColumnMappings.Add("status", "status");
                            sqlBulkCopy.ColumnMappings.Add("dateOfBirth", "dateOfBirth");
                            sqlBulkCopy.ColumnMappings.Add("gender", "gender");
                            sqlBulkCopy.ColumnMappings.Add("probationPeriod", "probationPeriod");
                            sqlBulkCopy.ColumnMappings.Add("personalEmail", "personalEmail");
                            sqlBulkCopy.ColumnMappings.Add("personalEmail2", "personalEmail2");
                            sqlBulkCopy.ColumnMappings.Add("personalEmail3", "personalEmail3");
                            sqlBulkCopy.ColumnMappings.Add("mobile", "mobile");
                            sqlBulkCopy.ColumnMappings.Add("relevantExperience", "relevantExperience");
                            sqlBulkCopy.ColumnMappings.Add("title", "title");
                            sqlBulkCopy.ColumnMappings.Add("yearsInJob", "yearsInJob");
                            sqlBulkCopy.ColumnMappings.Add("yearsInService", "yearsInService");
                            sqlBulkCopy.ColumnMappings.Add("prevExperience", "prevExperience");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dt2);
                            con.Close();
                        }
                    }


                    pagecount++;

                }


                return Ok(200);
            }
            catch (Exception ex)
            {


            }
            return Ok(201);
        }




        //// Get All Employees Details
        //[Route("AllEmployeeDtls")]
        //[HttpPost]
        //public async Task<IActionResult> AllEmployeeDtls()
        //{

        //    Token token = new Token();

        //    var client = new HttpClient();

        //    var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //    var response1 = await client.PostAsync(baseAddress, null);

        //    var result1 = await response1.Content.ReadAsStringAsync();


        //    var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

        //    JObject studentObj1 = JObject.Parse(JsonContent1);

        //    var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //    var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //     string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

        //    var result23 = JObject.Parse(sd);
        //    var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

        //       token.access_token = (string)items12[0];



        //    var client2 = new HttpClient();


        //    client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
        //    client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);





        //    int pagecount = 0;


        //    for (int i = 0; i <= pagecount; i++)
        //    {

        //        var url1 = "https://api.greythr.com/employee/v2/employees?page="+ pagecount +"&size=2000";

        //        var response3 = await client2.GetAsync(url1);


        //        var result3 = await response3.Content.ReadAsStringAsync();


        //        string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

        //        var result5 = JObject.Parse(result3);

        //        var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

        //        var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
        //        var model2 = JsonConvert.DeserializeObject<List<Models.AllEmpDtlsmodel>>(jsonString3);

        //        DataTable dt2 = new DataTable();

        //        dt2 = CreateDataTable(model2);
        //        JobRoot objclass2 = new JobRoot();
        //        if(model2.Count==0)
        //        {
        //           return Ok(200);
        //        }

        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {
        //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //            {
        //                sqlBulkCopy.DestinationTableName = "tbl_grathrportal";
        //                sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
        //                sqlBulkCopy.ColumnMappings.Add("name", "name");
        //                sqlBulkCopy.ColumnMappings.Add("firstName", "firstName");
        //                sqlBulkCopy.ColumnMappings.Add("middleName", "middleName");
        //                sqlBulkCopy.ColumnMappings.Add("lastName", "lastName");
        //                sqlBulkCopy.ColumnMappings.Add("email", "email");
        //                sqlBulkCopy.ColumnMappings.Add("employeeNo", "employeeNo");
        //                sqlBulkCopy.ColumnMappings.Add("dateOfJoin", "dateOfJoin");
        //                sqlBulkCopy.ColumnMappings.Add("leavingDate", "leavingDate");

        //                sqlBulkCopy.ColumnMappings.Add("originalHireDate", "originalHireDate");
        //                sqlBulkCopy.ColumnMappings.Add("leftorg", "leftorg");
        //                sqlBulkCopy.ColumnMappings.Add("lastModified", "lastModified");
        //                sqlBulkCopy.ColumnMappings.Add("status", "status");
        //                sqlBulkCopy.ColumnMappings.Add("dateOfBirth", "dateOfBirth");
        //                sqlBulkCopy.ColumnMappings.Add("gender", "gender");
        //                sqlBulkCopy.ColumnMappings.Add("probationPeriod", "probationPeriod");
        //                sqlBulkCopy.ColumnMappings.Add("personalEmail", "personalEmail");
        //                sqlBulkCopy.ColumnMappings.Add("personalEmail2", "personalEmail2");
        //                sqlBulkCopy.ColumnMappings.Add("personalEmail3", "personalEmail3");
        //                sqlBulkCopy.ColumnMappings.Add("mobile", "mobile");
        //                sqlBulkCopy.ColumnMappings.Add("relevantExperience", "relevantExperience");
        //                sqlBulkCopy.ColumnMappings.Add("title", "title");
        //                sqlBulkCopy.ColumnMappings.Add("yearsInJob", "yearsInJob");
        //                sqlBulkCopy.ColumnMappings.Add("yearsInService", "yearsInService");
        //                sqlBulkCopy.ColumnMappings.Add("prevExperience", "prevExperience");

        //                con.Open();
        //                sqlBulkCopy.WriteToServer(dt2);
        //                con.Close();
        //            }
        //        }


        //        pagecount++;

        //    }


        //    return Ok(200);
        //}




        // Get All Employee Profile Details
        [Route("AllEmployeeProfileDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeProfileDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/profile?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeeProfilemodel>>(jsonString3);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_profiledata";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("nickname", "nickname");
                        sqlBulkCopy.ColumnMappings.Add("twitter", "twitter");
                        sqlBulkCopy.ColumnMappings.Add("linkedIn", "linkedIn");
                        sqlBulkCopy.ColumnMappings.Add("facebook", "facebook");
                        sqlBulkCopy.ColumnMappings.Add("googlePlus", "googlePlus");
                        sqlBulkCopy.ColumnMappings.Add("biography", "biography");
                        sqlBulkCopy.ColumnMappings.Add("wishDOB", "wishDOB");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }

            return Ok(200);
        }




        // Get All Employee Personal Details
        [Route("AllEmployeePersonalDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeePersonalDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];

            var client2 = new HttpClient();

            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/personal?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeePersonalmodel>>(jsonString3);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_Personaldata";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("bloodGroup", "bloodGroup");
                        sqlBulkCopy.ColumnMappings.Add("maritalStatus", "maritalStatus");
                        sqlBulkCopy.ColumnMappings.Add("marriageDate", "marriageDate");
                        sqlBulkCopy.ColumnMappings.Add("spouseBirthday", "spouseBirthday");
                        sqlBulkCopy.ColumnMappings.Add("spouseName", "spouseName");
                        sqlBulkCopy.ColumnMappings.Add("actualDOB", "actualDOB");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }

            return Ok(200);
        }



        // Get All Employee Work Details
        [Route("AllEmployeeWorkDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeWorkDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/work?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeeWorkmodel>>(jsonString3);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_EmpWorkdata";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("extension", "extension");
                        sqlBulkCopy.ColumnMappings.Add("confirmDate", "confirmDate");
                        sqlBulkCopy.ColumnMappings.Add("lastPromotionDate", "lastPromotionDate");
                        sqlBulkCopy.ColumnMappings.Add("lastPrevEmployment", "lastPrevEmployment");
                        sqlBulkCopy.ColumnMappings.Add("noticePeriod", "noticePeriod");
                        sqlBulkCopy.ColumnMappings.Add("originalHireDate", "originalHireDate");
                        sqlBulkCopy.ColumnMappings.Add("probationExtendedBy", "probationExtendedBy");
                        sqlBulkCopy.ColumnMappings.Add("extendedProbationDays", "extendedProbationDays");
                        sqlBulkCopy.ColumnMappings.Add("onboardingStatus", "onboardingStatus");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }
            string Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            return Ok(200);
        }



        // Get All Employee Separation Details
        [Route("AllEmployeeSeparationDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeSeparationDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/separation?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeeSeparationmodel>>(jsonString3);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_EmpSeparationdata";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("leftOrg", "leftOrg");
                        sqlBulkCopy.ColumnMappings.Add("leavingDate", "leavingDate");
                        sqlBulkCopy.ColumnMappings.Add("retirementDate", "retirementDate");
                        sqlBulkCopy.ColumnMappings.Add("tentativeRelieveDate", "tentativeRelieveDate");
                        sqlBulkCopy.ColumnMappings.Add("exitInterviewDate", "exitInterviewDate");
                        sqlBulkCopy.ColumnMappings.Add("submittedResignation", "submittedResignation");
                        sqlBulkCopy.ColumnMappings.Add("tentativeLeavingDate", "tentativeLeavingDate");
                        sqlBulkCopy.ColumnMappings.Add("submissionDate", "submissionDate");
                        sqlBulkCopy.ColumnMappings.Add("fitToBeRehired", "fitToBeRehired");
                        sqlBulkCopy.ColumnMappings.Add("finalSettlementDate", "finalSettlementDate");
                        sqlBulkCopy.ColumnMappings.Add("leavingReason", "leavingReason");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }
            string Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            return Ok(200);
        }




        // Get All Employee Address Details
        [Route("AllEmployeeAddressDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeAddressDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/addresses/presentaddress?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeeAddressmodel>>(jsonString3);

                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(model2);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_EmpAddressdata";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("address1", "address1");
                        sqlBulkCopy.ColumnMappings.Add("address2", "address2");
                        sqlBulkCopy.ColumnMappings.Add("address3", "address3");
                        sqlBulkCopy.ColumnMappings.Add("city", "city");
                        sqlBulkCopy.ColumnMappings.Add("state", "state");
                        sqlBulkCopy.ColumnMappings.Add("country", "country");
                        sqlBulkCopy.ColumnMappings.Add("pin", "pin");
                        sqlBulkCopy.ColumnMappings.Add("phone1", "phone1");
                        sqlBulkCopy.ColumnMappings.Add("phone2", "phone2");
                        sqlBulkCopy.ColumnMappings.Add("extnno", "extnno");
                        sqlBulkCopy.ColumnMappings.Add("fax", "fax");
                        sqlBulkCopy.ColumnMappings.Add("mobile", "mobile");
                        sqlBulkCopy.ColumnMappings.Add("email", "email");
                        sqlBulkCopy.ColumnMappings.Add("name", "name");
                        sqlBulkCopy.ColumnMappings.Add("addressType", "addressType");
                        sqlBulkCopy.ColumnMappings.Add("companyId", "companyId");
                        sqlBulkCopy.ColumnMappings.Add("recordId", "recordId");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }
            string Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            return Ok(200);
        }







        // Get All Employee Orgtree - Reporting Structure
        [Route("AllEmployeeOrgReportingDtls")]
        [HttpPost]
        public async Task<IActionResult> AllEmployeeOrgReportingDtls()
        {

            Token token = new Token();

            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response1 = await client.PostAsync(baseAddress, null);

            var result1 = await response1.Content.ReadAsStringAsync();


            var JsonContent1 = response1.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token.access_token = (string)items12[0];



            var client2 = new HttpClient();


            client2.DefaultRequestHeaders.Add("x-greythr-domain", "sheenlac.greythr.com");
            client2.DefaultRequestHeaders.Add("ACCESS-TOKEN", token.access_token);


            int pagecount = 0;


            for (int i = 0; i <= pagecount; i++)
            {

                var url1 = "https://api.greythr.com/employee/v2/employees/org-tree?page=" + pagecount + "&size=2000";

                var response3 = await client2.GetAsync(url1);


                var result3 = await response3.Content.ReadAsStringAsync();


                string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + result3 + "]}";

                var result5 = JObject.Parse(result3);

                var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items3);
                var model2 = JsonConvert.DeserializeObject<List<Models.AllEmployeeOrgReportingmodel>>(jsonString3);

                ITEMGRN iTEMGRN = new ITEMGRN();
                List<ITEMGRN> objlist = new List<ITEMGRN>();


                List<AllEmployeeOrgReportid> objidlist = new List<AllEmployeeOrgReportid>();
                for (int k = 0; k <= model2.Count - 1; k++)
                {

                    try
                    {

                        if (model2[k].orgtree != null)
                        {
                            for (int j = 0; j <= model2[k].orgtree.Length; j++)
                            {
                                AllEmployeeOrgReportid id1 = new AllEmployeeOrgReportid();
                                id1.employeeId = model2[k].employeeId;
                                id1.orgtree = model2[k].orgtree[j];
                                // var sd1 = model2[k].orgtree[j];
                                objidlist.Add(id1);

                            }
                        }

                    }
                    catch (Exception)
                    {


                    }
                }


                DataTable dt2 = new DataTable();

                dt2 = CreateDataTable(objidlist);
                JobRoot objclass2 = new JobRoot();
                if (model2.Count == 0)
                {
                    return Ok(200);
                }

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "tbl_EmpOrgReportingData";
                        sqlBulkCopy.ColumnMappings.Add("employeeId", "employeeId");
                        sqlBulkCopy.ColumnMappings.Add("orgtree", "orgtree");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }
                pagecount++;
            }
            string Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            return Ok(200);
        }










        // Get Attrition Report
        [HttpPost]
        [Route("getAttritionReport")]
        public ActionResult getAttritionReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_attrition_report";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
                    cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
                    cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
                    cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
                    cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
                    cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                    cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }

    }
}
