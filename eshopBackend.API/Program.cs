using eshopBackend.DAL;

DataAccessLayer _ = new(); //create instance of DependencyInjection

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(b => b
                .AddDebug()
                .AddConsole());
                
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
