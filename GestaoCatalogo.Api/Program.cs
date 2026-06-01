using FluentValidation;
using FluentValidation.AspNetCore;
using GestaoCatalogo.Application.Validations;
using GestaoCatalogo.CrossCutting.Ioc;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("StringConnectionSqlServer")
);

builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão de Categoria e Produtos API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
