using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CattleMgmApi.Dtos.Roles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGet("api/v1/roles", async (IRolesRepository repo, IMapper mapper) =>
{
    var roles = await repo.GetRoles();
    var mapped_object = mapper.Map<List<RolesReadDto>>(roles);

    return Results.Ok(mapped_object);
});





//krijimi i api per editimin e gjedhes



//app.MapGet("/", () => "Hello World!");

app.Run();