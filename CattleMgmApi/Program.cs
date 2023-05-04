using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CattleMgmApi.Dtos.Roles;
using CattleMgm.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<PraktikadbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddScoped<IRolesRepository, RoleRepository>();


var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));




builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IRolesRepository, RoleRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//localhost:548954/api/v1/cattles
//perdorimi i repository, mapper per mshefjen e struktures se databazes nga end-user 
app.MapGet("api/v1/cattles", async (ICattleRepository repo, IMapper mapper) =>
{
    var cattles = await repo.GetAllCattles();

    return Results.Ok(mapper.Map<IEnumerable<CattleReadDto>>(cattles));
});


//perdorimi i dbcontextit per nxjerrje te te dhenave nga databaza pa readdto
//app.MapGet("api/v1/cattles", async (PraktikadbContext context) =>
//{
//    var cattles = await context.Cattle.ToListAsync();

//    return Results.Ok(cattles);
//});


// Cattle
app.MapGet("api/v1/cattles/{id}", async (ICattleRepository repo, IMapper mapper, int id) =>
{
    var cattle = await repo.GetCattleById(id);
    if (cattle is not null)
    {
        return Results.Ok(mapper.Map<CattleReadDto>(cattle));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});

app.MapPost("api/v1/cattles", async (ICattleRepository repo, IMapper mapper, CattleCreateDto cattle) =>
{
    if (cattle is not null)
    {
        var mapped_object = mapper.Map<Cattle>(cattle);
        await repo.CreateCattle(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<CattleReadDto>(mapped_object);

        return Results.Created($"Gjedhja me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});



// Roles
// GET request per te kerkuar te gjitha rolet nga databaza
app.MapGet("api/v1/roles", async (IRolesRepository repo, IMapper mapper) =>
{
    // Metoda GetRoles qe therret te gjitha rolet.
    var roles = await repo.GetRoles();

    // Kryen Map-imin e listes se kthyer ne nje liste e objektit RolesReadDto.
    var mapped_object = mapper.Map<List<RolesReadDto>>(roles);

    // Kthen te gjitha objektet e Map-uara me HTTP 200 (OK) status code.
    return Results.Ok(mapped_object);
});


// POST request e cila krijon rol te ri ne Databaze.
app.MapPost("api/v1/roles", async (HttpContext context) =>
{
    // Merr serviset e nevojitura.
    var serviceProvider = context.RequestServices;
    var repo = serviceProvider.GetRequiredService<IRolesRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();

    // Therret metoden CreateRole then e kalon emrin e rolit si argument.
    var result = await repo.CreateRole("Some Role Name");

    // Kryen Map-min e rezultatit tek nje objekt RolesCreateDto dhe e kthen.
    return mapper.Map<RolesCreateDto>(result);
});


// DELETE request e cila fshin rolin nga Databaza ne baze te ID te shenuar ne path.
app.MapDelete("api/v1/roles/{id}", async (HttpContext context) =>
{
    var serviceProvider = context.RequestServices;
    var repo = serviceProvider.GetRequiredService<IRolesRepository>();
    var roleId = context.GetRouteValue("id").ToString();

    // Merr rolin nga databaza duke perdorur ID-ne nga URL path.
    var role = await repo.GetRoleById(roleId);

    // Fshin rolin nga databaza duke perdorur ID-ne ne URL.
    var result = await repo.DeleteRole(roleId);

    // Kthen sukses-pergjigjje.
    return Results.Ok();
});







//krijimi i api per editimin e gjedhes


//app.MapGet("/", () => "Hello World!");

app.Run();