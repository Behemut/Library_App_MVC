using AutoMapper;
using Library_API.DBContext;
using Library_API.Models.Auth;
using Library_API.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Library_API.Repository.Interfaces;
using Library_API.Repository;
using Library_API;

var builder = WebApplication.CreateBuilder(args);

//SQL CONNECTION

//var connection = "Server=KELVIS\\DEVOPS;Database=dbLibrary;User Id=usr_library;Password=Libr@ry22!;TrustServerCertificate=True;MultipleActiveResultSets=True;";

var connection = "Server=KELVIS\\DEVOPS;Database=dbLibrary;User Id=devops;Password=Windows2020;TrustServerCertificate=True;MultipleActiveResultSets=True;";




builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
 .AddJwtBearer(options =>
 {
     var key = Encoding.Default.GetBytes(Tokenizator.SecretKey);
     
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters() {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ClockSkew = TimeSpan.Zero,
         ValidAudience = Tokenizator.AudienceToken,
         ValidIssuer = Tokenizator.IssuerToken,
         IssuerSigningKey = new SymmetricSecurityKey(key)
     };
 });


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
    
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Library_API", Version = "v1" });
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. ",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});


//IMAPPER 
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
//builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddScoped<ApplicationUser, ApplicationUser>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

#if RELEASE
    app.Environment.EnvironmentName = "Production";
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
