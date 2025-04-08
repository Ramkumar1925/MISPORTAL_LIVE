using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace SheenlacMISPortal.Controllers
{
     [Authorize]
  // [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerPartnerController : Controller
    {
        private readonly IConfiguration Configuration;

        public CustomerPartnerController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        [Route("Customerpartnerjson")]
        public ActionResult Customerpartnerjson()
        {


            DataSet ds = new DataSet();
            string query = "sp_get_customer_partner";
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


    }
}
