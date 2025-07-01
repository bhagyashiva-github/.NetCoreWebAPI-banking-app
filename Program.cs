
using System.Text;
using coreAPI_banking_app.Models;
using coreAPIData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "coreAPI-banking-app v1");
});
app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();

#region Swagger Authorize Button Logic - Implementation in Progress
// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//         Name = "Authorization",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.Http,
//         Scheme = "Bearer"
//     });

//     options.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type = ReferenceType.SecurityScheme,
//                     Id = "Bearer"
//                 }
//             },
//             new string[] {}
//         }
//     });
// });
#endregion

#region JWT Implementation Logic - Implementation in Progress
//var jwtKey = builder.Configuration["Jwt:Key"];

// if (string.IsNullOrWhiteSpace(jwtKey))
//     throw new InvalidOperationException("JWT Key is missing from configuration.");

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//         };
//     });
#endregion 







