using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Profiles;
using CattleMgmApi.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));

builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<ICattleMilkRepository, CattleMilkRepository>();
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

app.MapGet("api/v1/cattles", async (ICattleMilkRepository repo, IMapper mapper) =>
{
    var cattles = await repo.GetAllCattlesMilk();

    return Results.Ok(mapper.Map<IEnumerable<CattleMilkReadDto>>(cattles));
});

//perdorimi i dbcontextit per nxjerrje te te dhenave nga databaza pa readdto
//app.MapGet("api/v1/cattles", async (PraktikadbContext context) =>
//{
//    var cattles = await context.Cattle.ToListAsync();

//    return Results.Ok(cattles);
//});


app.MapGet("api/v1/cattles/{id}", async (ICattleRepository repo, IMapper mapper, int id) =>
{
    var cattle = await repo.GetCattleById(id);
    if(cattle is not null)
    {
        return Results.Ok(mapper.Map<CattleReadDto>(cattle));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});

app.MapGet("api/v1/cattlesmilk/{id}", async (ICattleMilkRepository repo, IMapper mapper, int id) =>
{
    var cattlemilk = await repo.GetCattleMilkById(id);
    if (cattlemilk is not null)
    {
        return Results.Ok(mapper.Map<CattleMilkReadDto>(cattlemilk));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});
//app.MapPost("api/v1/cattle", async (ICattleRepository repo, IMapper mapper, CattleCreateDto cattle) =>
//{
//    if(cattle is not null)
//    {
//        var mapped_object = mapper.Map<Cattle>(cattle);
//        await repo.CreateCattle(mapped_object);
//        await repo.SaveChanges();

//        var result = mapper.Map<CattleReadDto>(mapped_object);

//        return Results.Created($"Gjedhja me id {result.Id} u krijua!", result);
//    }
//    return Results.NoContent();
//});

//Create CattleMilk
app.MapPost("api/v1/cattlemilk", async (ICattleMilkRepository repo, IMapper mapper, CattleMilkCreateDto cattlemilk) =>
{
    if (cattlemilk is not null)
    {
        var mapped_object = mapper.Map<CattleMilk>(cattlemilk);
        await repo.CreateCattleMilk(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<CattleMilkReadDto>(mapped_object);

        return Results.Created($"Gjedhja me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});
//Last CattleMilk
app.MapGet("api/v1/cattlemilk/{id}", async (ICattleMilkRepository repo, IMapper mapper, int id) =>
{
    var pos = await repo.GetLastCattleMilk(id);
    if (pos is not null)
    {
        return Results.Ok(mapper.Map<CattleMilkReadDto>(pos));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }
});
// Delete
app.MapDelete("api/v1/cattlemilk/{id}", async (ICattleMilkRepository repo, int id) =>
{
    var milk = await repo.GetCattleMilkById(id);
    if (milk is null)
    {
        return Results.NotFound(new { error = "CattleMilk not found" });
    }
    repo.DeleteCattleMilk(milk);
    await repo.SaveChanges();
    return Results.NoContent();
});
// Editimi
app.MapPut("api/v1/cattlemilk/{id}", async (ICattleMilkRepository repo, IMapper mapper, CattleMilkUpdateDto cattlemilk, int id) =>
{
    if (cattlemilk is not null)
    {
        var mapped_object = mapper.Map<CattleMilk>(cattlemilk);
        repo.UpdateCattleMilk(mapped_object, id);
        await repo.SaveChanges();
        var result = mapper.Map<CattleMilkReadDto>(mapped_object);
        return Results.Created($"CattleMilk me id {result.Id} u editua!", result);
    }
    return Results.NoContent();
});


//app.MapGet("/", () => "Hello World!");

app.Run();
