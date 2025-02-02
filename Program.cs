using ArulOliNagar.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SupabaseService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<SmsService>();


builder.Services.AddCors(options => { options.AddPolicy("AllowAllPorts",
                    policy =>
                        {
                            policy.AllowAnyOrigin().
                            AllowAnyHeader()
                            .AllowAnyMethod();
                         });
    
                    });

builder.Services.AddScoped<MembersService>(config =>
{

    var mongosettings = builder.Configuration.GetSection("MongoDB");
    return new MembersService(
        mongosettings["ConnectionString"],
        mongosettings["DataBase"],
        mongosettings["CollectionName"]);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPorts");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
