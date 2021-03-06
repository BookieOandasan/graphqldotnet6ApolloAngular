using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using GraphQlApi.Notes;
using GraphQlApi.Notes.Subscription;
using GraphQlApi.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<INotePublish, NotePublish>();
builder.Services.AddScoped<INotesContext, NotesContext>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddDbContext<NotesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
//Add notes schema
builder.Services.AddScoped<ISchema, NotesSchema>(services =>
    new NotesSchema(new SelfActivatingServiceProvider(services))

);

builder.Services.AddTransient<IValidationRule, AuthorizationValidationRule>();


// register graphQL
builder.Services.AddGraphQL(options =>
    {
        options.EnableMetrics = true;
    })
    .AddGraphQLAuthorization(options=> { options.AddPolicy("Authorized", p => p.RequireAuthenticatedUser()); }).AddSystemTextJson();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o => o.Cookie.Name = "graph-auth");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                   .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
     //add altair UI to development only
    app.UseGraphQLAltair();
    app.UseGraphQLPlayground(options: new PlaygroundOptions());
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

//app.UseAuthorization();

app.MapControllers();
app.UseWebSockets();
// make sure all our schemas registered to route
app.UseGraphQL<ISchema>();
app.Run();
