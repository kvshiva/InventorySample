using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventorySampleServer.Api._Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(s => s.Conventions.Add(new ActionHiding()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
	o.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader().SetIsOriginAllowed(origin => true));
});

builder.Services.AddAuthentication(k =>
{
	k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p =>
{
	var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
	p.SaveToken = true;
	p.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
});

//builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin",
	policyBuilder => policyBuilder.RequireClaim("Admin").RequireClaim("User"));
});

builder.Services.AddControllers().AddJsonOptions(o =>
{
	o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
	o.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
	o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
	o.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
