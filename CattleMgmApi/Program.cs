using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Dtos.BreedDtos;
using CattleMgmApi.Repository;
using CattleMgmApi.Repository.BreedRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));

builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();

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

//listimi i Breed
app.MapGet("api/v1/breed", async (IBreedRepository repo, IMapper mapper) =>
{
    var breed = await repo.GetAllBreed();

    return Results.Ok(mapper.Map<IEnumerable<BreedReadDto>>(breed));
});
//listimi sipas id-se
app.MapGet("api/v1/breeds/{id}", async (IBreedRepository repo, IMapper mapper, int id) =>
{
    var breed = await repo.GetBreedById(id);
    if (breed is not null)
    {
        return Results.Ok(mapper.Map<CattleReadDto>(breed));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});
//krijimi i breed
app.MapPost("api/v1/breeds", async (IBreedRepository repo, IMapper mapper, BreedCreateDto breed) =>
{
    if (breed is not null)
    {
        var mapped_object = mapper.Map<Breed>(breed);
        await repo.CreateBreed(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<BreedReadDto>(mapped_object);

        return Results.Created($"Lloji me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});
//editimi i breed
app.MapPut("api/v1/breeds/{id}", async (int id, IBreedRepository repo, IMapper mapper, BreedUpdateDto breed) =>
{
    var existing_breed = await repo.GetBreedById(id);
    if (existing_breed is null)
    {
        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
    }

    mapper.Map(breed, existing_breed);
    await repo.UpdateBreed(existing_breed);
    await repo.SaveChanges();

    var result = mapper.Map<BreedReadDto>(existing_breed);

    return Results.Ok(result);
});

//fshirja e breed
app.MapDelete("api/v1/breeds/{id}", async (int id, IBreedRepository repo) =>
{
    var existing_breed = await repo.GetBreedById(id);
    if (existing_breed is null)
    {
        return Results.NotFound($"Lloji me id {id} nuk u gjet!");
    }

    await repo.DeleteBreed(existing_breed);
    await repo.SaveChanges();

    return Results.NoContent();
});


//app.MapGet("/", () => "Hello World!");

app.Run();
