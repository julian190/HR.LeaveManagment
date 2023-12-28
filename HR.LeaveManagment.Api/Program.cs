using HR.LeaveManagment.Application;
using HR.LeaveManagment.Infrastrucure;
using HR.LeaveManagment.Presistance;
using HR.LeaveManagement.Identity;
using EllipticCurve;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
AddSwaggerDoc(builder.Services);

 void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "HR LeaveManagment Api",
        });
    });
    
}

//Adding the other project services
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastrucureServices(builder.Configuration);
builder.Services.ConfigurePresistanceServices(builder.Configuration);
builder.Services.ConfigurationIdentityServices(builder.Configuration);

//Configure swagger 


//Cors service
builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPoliy",builder=>builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyHeader()
    
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}
app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPoliy");
app.MapControllers();

app.Run();
