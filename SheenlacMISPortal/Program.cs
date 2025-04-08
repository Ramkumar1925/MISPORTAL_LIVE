using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SheenlacMISPortal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SheenlacMISPortal.Services;
using SheenlacMISPortal.Models;
using CorePush.Google;
using CorePush.Apple;
using SheenlacMISPortal.Logging;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();
builder.Services.AddSingleton<ILog, LogNLog>();


var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SheenlacMISPortal", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });





    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://portal.sheenlac.com",
                                              "https://vendor.sheenlac.com",
                                              "https://bank.sheenlac.com",
                                              "https://devmisportal.sheenlac.com",
                                              "https://misportal.sheenlac.com",
                                              "http://localhost:4200",
                                              "http://localhost:5000",
                                              "http://103.154.252.203:96",
                                              "http://10.15.15.12:639",
                                              "https://pms.sheenlac.com"
                                              )
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});


var app = builder.Build();
app.UseRouting();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("/security/getMessage",
//() => "Hello World!").RequireAuthorization();
app.MapPost("/security/createToken",
[AllowAnonymous] (User user) => {

    var _config = app.Services.GetRequiredService<IConfiguration>();
    var connstrg = _config.GetConnectionString("Database");
    string status = string.Empty;
    using (SqlConnection conn = new SqlConnection(connstrg))
    {
        conn.Open();
        string cmdtext = "sp_validate_sheenlac_login";
        SqlCommand cmd = new SqlCommand(cmdtext, conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FilterValue1", user.UserName);
        cmd.Parameters.AddWithValue("@FilterValue2", user.Password);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            status = reader["cstatus"].ToString() ?? "";
        }
    }

    if (status == "Valid")
    {
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
        (builder.Configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
            Expires = DateTime.UtcNow.AddMinutes(120),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return Results.Ok(stringToken);
    }
    return Results.Unauthorized();
});
ILog logger = new LogNLog();
app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler("/Error");
app.UseMiddleware<ExceptionMiddleware>();


app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

