using ClientSupplierApi.Data;
using ClientSupplierApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Db(builder.Configuration);

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.SwaggerInfra(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
