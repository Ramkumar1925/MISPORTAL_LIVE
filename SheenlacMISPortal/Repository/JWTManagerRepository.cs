using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SheenlacMISPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SheenlacMISPortal.Repository
{
    public class JWTManagerRepository: IJWTManagerRepository
    {
		Dictionary<string, string> UsersRecords = new Dictionary<string, string>();
		//{
		//{ "user1","password1"},
		//{ "user2","password2"},
		//{ "user3","password3"},
		//};

		private readonly IConfiguration Configuration;
		public JWTManagerRepository(IConfiguration _configuration)
		{
			Configuration = _configuration;
		}

	
		 Tokens IJWTManagerRepository.Authenticate(Users users)
        {
			DataSet ds = new DataSet();
			string query = "sp_get_mis_employee_verficiation ";
			using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
			{

				using (SqlCommand cmd = new SqlCommand(query))
				{
					cmd.Connection = con;
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Employeecode", users.Name);		


					con.Open();

					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
					adapter.Fill(ds);
					con.Close();
				}
			}

			UsersRecords = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(
row => row.Field<string>("loginuserid"),
row => row.Field<string>("loginpassword"));

			if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
			{
				return null;
			}

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, users.Name)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };
		}
    }
}
