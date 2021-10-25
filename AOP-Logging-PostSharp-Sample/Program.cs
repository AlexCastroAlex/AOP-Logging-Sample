using AOP_Logging_PostSharp_Sample.Common.Models;
using AOP_Logging_PostSharp_Sample.Repositories;
using AOP_Logging_PostSharp_Sample.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
Log.Logger = new LoggerConfiguration()
            .WriteTo.ColoredConsole()
            .WriteTo.File("serilog.log")
            .CreateLogger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/articles",  (IArticleService service) =>
{
    return  service.GetArticles();
});

app.MapGet("/article", (IArticleService service, int id) =>
{
    return service.GetArticle(id);
});

app.MapPost("addArticle", (IArticleService service, Article article) =>
{
    return service.AddArticle(article);
}
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

