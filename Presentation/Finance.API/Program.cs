using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Finance.Application;
using Finance.Application.Abstractions;
using Finance.Application.Validators;
using Finance.Infastructure;
using Finance.Infastructure.Filters;
using Finance.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfastructureServices();

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true)
      .AddJsonOptions(opts =>
      {
          var enumConverter = new JsonStringEnumConverter();
          opts.JsonSerializerOptions.Converters.Add(enumConverter);
      });


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();


builder.Services.AddStackExchangeRedisCache(action =>
{
    action.Configuration = builder.Configuration["ConnectionStrings:Redis"];
});

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["ConnectionStrings:Redis"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("App", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });
    

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
              builder =>
              {
                  builder.AllowAnyHeader()
                         .AllowAnyMethod()
                         .SetIsOriginAllowed((host) => true)
                         .AllowCredentials();
              }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseCustomException();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<IAppInit>();
    _ = context.InitAsync();


}
app.Run();
