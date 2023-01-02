using Application;
using Application.Abstractions;
using Application.Posts.Commands;
using Application.Posts.Queries;
using DataAccess;
using DataAccess.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();

app.MinimalApi();

app.Run();

