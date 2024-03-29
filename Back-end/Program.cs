using Back_end.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container or Dependency Injection
builder.Services.AddControllers();
builder.Services.AddScoped<AuthInterFace,AuthClass>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Using CORS Policy
app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
