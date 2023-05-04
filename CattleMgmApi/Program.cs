using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Dtos.Farmer;
using CattleMgmApi.Repository;
using CattleMgmApi.Repository.Farmers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));

builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
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

app.MapPost("api/v1/cattles", async (ICattleRepository repo, IMapper mapper, CattleCreateDto cattle) =>
{
    if(cattle is not null)
    {
        var mapped_object = mapper.Map<Cattle>(cattle);
        await repo.CreateCattle(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<CattleReadDto>(mapped_object);
        
        return Results.Created($"Gjedhja me id {result.Id} u krijua!",result);
    }
    return Results.NoContent();
});

//krijimi i api per editimin e gjedhes



//perdorimi i repository, mapper per mshefjen e struktures se databazes 


app.MapGet("api/v1/farmers", async (IFarmerRepository repo, IMapper mapper) =>
{
    var farmers = await repo.GetAllFarmers();

    return Results.Ok(mapper.Map<IEnumerable<FarmerReadDto>>(farmers));
});

app.MapGet("api/v1/farmers/{id}", async (IFarmerRepository repo, IMapper mapper, int id) =>
{
    var farmer = await repo.GetFarmerById(id);
    if (farmer is not null)
    {
        return Results.Ok(mapper.Map<FarmerReadDto>(farmer));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});

app.MapPost("api/v1/farmers", async (IFarmerRepository repo, IMapper mapper, FarmerCreateDto farmer) =>
{
    if (farmer is not null)
    {
        var mapped_object = mapper.Map<Farmer>(farmer);
        await repo.CreateFarmer(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<FarmerReadDto>(mapped_object);

        return Results.Created($"Fermeri me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});

app.MapPut("api/v1/farmers/{id}", async (IFarmerRepository repo, int id, IMapper mapper, FarmerCreateDto farmer) =>
{
   
    if (farmer is not null)
    {
        var mapped_object = mapper.Map<Farmer>(farmer);
        repo.Update(id, mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<FarmerReadDto>(mapped_object);

        return Results.Created($"Fermeri me id {result.Id} u Editua!", result);
    }
    return Results.NoContent();
});

app.MapDelete("api/v1/farmers/{id}",async (IFarmerRepository repo, int id, IMapper mapper) =>
{
    var farmer = await repo.GetFarmerById(id);
    if(farmer is not null)
    {
        var mapped_object = mapper.Map<Farmer>(farmer);
        repo.DeleteFarmer(id);
        await repo.SaveChanges();
        var result = mapper.Map<FarmerReadDto>(mapped_object);
    }
    return Results.NoContent();
});
//app.MapGet("/", () => "Hello World!");

app.Run();
