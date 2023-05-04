using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CattleMgmApi.Repository;
using CattleMgmApi.Repository.Humidity;
using CattleMgmApi.Dtos.Humidity;
using CattleMgmApi.Repository.Humidity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));

builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();
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


//app.MapGet("api/v1/humidity", async (IHumidityRepository repo, IMapper mapper) =>
//{
//    var humidity = await repo.GetAllHumidity();
//    return Results.Ok(mapper.Map<IEnumerable<HumidityReadDto>>(humidity));
//});

//app.MapGet("api/v1/humiditys/{id}", async (IHumidityRepository repo, IMapper mapper, int id) =>
//{
//    var humidity = await repo.GetHumidityById(id);
//    if (humidity is not null)
//    {
//        return Results.Ok(mapper.Map<HumidityReadDto>(humidity));
//    }
//    else
//    {
//        return Results.NotFound(new { error = "not found" });
//    }
//});


//app.MapPost("api/v1/humiditys", async (IHumidityRepository repo, IMapper mapper, HumidityCreateDto humidity) =>
//{
//    if (humidity is not null)
//    {
//        var mapped_object = mapper.Map<CattleHumidity>(humidity);
//        await repo.CreateHumidity(mapped_object);
//        await repo.SaveChanges();
//        var result = mapper.Map<HumidityReadDto>(mapped_object);
//        return Results.Created($"Lloji me id {result.Id} u krijua!", result);
//    }
//    return Results.NoContent();
//});


//app.MapPut("api/v1/humiditys/{id}", async (int id, IHumidityRepository repo, IMapper mapper, HumidityUpdateDto humidity) =>
//{
//    var existing_humidity = await repo.GetHumidityById(id);
//    if (existing_humidity is null)
//    {
//        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
//    }
//    mapper.Map(humidity, existing_humidity);
//    await repo.UpdateHumidity(existing_humidity);
//    await repo.SaveChanges();
//    var result = mapper.Map<HumidityReadDto>(existing_humidity);
//    return Results.Ok(result);
//});


//app.MapDelete("api/v1/humiditys/{id}", async (int id, IHumidityRepository repo) =>
//{
//    var existing_humidity = await repo.GetHumidityById(id);
//    if (existing_humidity is null)
//    {
//        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
//    }
//    await repo.DeleteHumidity(existing_humidity);
//    await repo.SaveChanges();
//    return Results.NoContent();
//});

app.MapGet("api/v1/humidity", async (IHumidityRepository repo, IMapper mapper) =>
{
    var humidity = await repo.GetAllHumidity();
    return Results.Ok(mapper.Map<IEnumerable<HumidityReadDto>>(humidity));
});

app.MapGet("api/v1/humiditys/{id}", async (int id, IHumidityRepository repo, IMapper mapper) =>
{
    var humidity = await repo.GetHumidityById(id);
    if (humidity is not null)
    {
        return Results.Ok(mapper.Map<HumidityReadDto>(humidity));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }
});


app.MapPost("api/v1/humiditys", async (IHumidityRepository repo, IMapper mapper, HumidityCreateDto humidity) =>
{
    if (humidity is not null)
    {
        var mapped_object = mapper.Map<CattleHumidity>(humidity);
        await repo.CreateHumidity(mapped_object);
        await repo.SaveChanges();
        var result = mapper.Map<HumidityReadDto>(mapped_object);
        return Results.Created($"Lloji me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});


app.MapPut("api/v1/humiditys/{id}", async (int id, IHumidityRepository repo, IMapper mapper, HumidityUpdateDto humidity) =>
{
    var existing_humidity = await repo.GetHumidityById(id);
    if (existing_humidity is null)
    {
        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
    }
    mapper.Map(humidity, existing_humidity);
    await repo.UpdateHumidity(existing_humidity);
    await repo.SaveChanges();
    var result = mapper.Map<HumidityReadDto>(existing_humidity);
    return Results.Ok(result);
});


app.MapDelete("api/v1/humiditys/{id}", async (int id, IHumidityRepository repo) =>
{
    var existing_humidity = await repo.GetHumidityById(id);
    if (existing_humidity is null)
    {
        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
    }
    await repo.DeleteHumidity(existing_humidity);
    await repo.SaveChanges();
    return Results.NoContent();
});



app.Run();
