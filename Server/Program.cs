var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Iyzico Options
builder.Services.Configure<Server.Options.IyzicoOptions>(
    builder.Configuration.GetSection("IyzicoOptions"));

// --- CORS (Customer UI: https://localhost:7202) ---
const string UiCors = "UiCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(UiCors, policy =>
        policy
            .WithOrigins("https://localhost:7202")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

var app = builder.Build();

// Swagger (dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// CORS tam burada olmalý
app.UseCors(UiCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
