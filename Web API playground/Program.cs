using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoapCore;
using System.ServiceModel;
using System.Text;
using WebApiData;
using static Web_API_playground.Services.CalculatorSerice.CalculatorService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT authentication
var key = builder.Configuration["Jwt:Key"] ?? "default_key";
byte[] bytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(bytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Soap calculator injection config
builder.Services.AddSoapCore();
// Get service address from appsettings.json
string? soapServiceUrl = builder.Configuration["AppSettings:SoapServiceUrl"];
builder.Services.AddSingleton<IcalculatorService, CalculatorServices>();

// Soap calculator service
builder.Services.AddSingleton<CalculatorServiceClient>(provider =>
{
    BasicHttpBinding binding = new BasicHttpBinding();
    EndpointAddress endpoint = new EndpointAddress(soapServiceUrl);
    return new CalculatorServiceClient(binding, endpoint);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.UseSoapEndpoint<IcalculatorService>("/CalculatorService.svc", new SoapEncoderOptions());

app.Run();
