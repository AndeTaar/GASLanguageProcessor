﻿using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.Frontend;
using GASLanguageProcessor.TableType;

var builder = WebApplication.CreateBuilder(args);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void Main(string[] args)
{

}
