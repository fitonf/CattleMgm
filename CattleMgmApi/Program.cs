using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Dtos.BreedDtos;
using CattleMgmApi.Dtos.Farmer;
using CattleMgmApi.Dtos.CattlePositionDtos;
using CattleMgmApi.Dtos.MunicipalyDtos;
using CattleMgmApi.Repository;
using CattleMgmApi.Repository.Farmers;
using Microsoft.AspNetCore.Builder;
using CattleMgmApi.Repository.CattlePositionRepository;
using CattleMgmApi.Repository.MunicipalityRepository;
using CattleMgmApi.Repository.BreedRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CattleMgmApi.Repository.Media;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PraktikadbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));

builder.Services.AddScoped<ICattleRepository, CattleRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();

builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
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
//krijimi i api per editimin e gjedhes

app.MapGet("api/v1/media", async (IMediaRepository repo, IMapper mapper) =>
{
    var media = await repo.GetAllMedia();

    return Results.Ok(mapper.Map<IEnumerable<MediaReadDto>>(media));
});

app.MapGet("api/v1/media/{id}", async (IMediaRepository repo, IMapper mapper, int id) =>
{
    var media = await repo.GetAllMedia();
    if (media is not null)
    {
        return Results.Ok(mapper.Map<MediaReadDto>(media));
    }
    else
    {
        return Results.NotFound(new { error = "not found" });
    }

});

app.MapPost("api/v1/media", async (IMediaRepository repo, IMapper mapper, MediaCreateDto media) =>
{
    if (media is not null)
    {
        var mapped_object = mapper.Map<Media>(media);
        await repo.CreateMedia(mapped_object);
        await repo.SaveChanges();

        var result = mapper.Map<MediaReadDto>(mapped_object);

        return Results.Created($"Media me id {result.Id} u krijua!", result);
    }
    return Results.NoContent();
});
    //Krijimi i api per municipality

    app.MapGet("api/v1/municipality", async (IMunicipalityRepository repo, IMapper mapper) =>
        {
            var municipalities = await repo.GetAllMunicipality();

            return Results.Ok(mapper.Map<IEnumerable<MunicipalityReadDto>>(municipalities));
        });

    app.MapGet("api/v1/municipality/{id}", async (IMunicipalityRepository repo, IMapper mapper, int id) =>
    {
        var municipality = await repo.GetMunicipalityById(id);
        if (municipality is not null)
        {
            return Results.Ok(mapper.Map<MunicipalityReadDto>(municipality));
        }
        else
        {
            return Results.NotFound(new { error = "not found" });
        }
    });

    app.MapPost("api/v1/municipality", async (IMunicipalityRepository repo, IMapper mapper, MunicipalityCreateDto municipality) =>
    {
        if (municipality is not null)
        {
            var mapped_object = mapper.Map<Municipality>(municipality);
            await repo.CreateMunicipality(mapped_object);
            await repo.SaveChanges();

            var result = mapper.Map<MunicipalityReadDto>(mapped_object);

            return Results.Created($"Komuna me id {result.Id} u krijua!", result);
        }
        return Results.NoContent();
    });

    app.MapPut("api/v1/municipality/{id}", async (IMunicipalityRepository repo, IMapper mapper, MunicipalityCreateDto municipality, int id) =>
    {
        if (municipality is not null)
        {
            var mapped_object = mapper.Map<Municipality>(municipality);
            repo.UpdateMunicipality(mapped_object, id);
            await repo.SaveChanges();
            var result = mapper.Map<MunicipalityReadDto>(mapped_object);
            return Results.Created($"Komuna me id {result.Id} u editua!", result);
        }
        return Results.NoContent();
    });







    app.MapGet("api/v1/position", async (IPositionRepository repo, IMapper mapper) =>
    {
        var pos = await repo.GetAllPositions();

        return Results.Ok(mapper.Map<IEnumerable<PositionReadDto>>(pos));
    });

    app.MapGet("api/v1/position/{id}", async (IPositionRepository repo, IMapper mapper, int id) =>
    {
        var pos = await repo.GetPositionById(id);
        if (pos is not null)
        {
            return Results.Ok(mapper.Map<PositionReadDto>(pos));
        }
        else
        {
            return Results.NotFound(new { error = "not found" });
        }

    });

    app.MapPost("api/v1/position", async (IPositionRepository repo, IMapper mapper, PositionCreateDto pos) =>
    {
        if (pos is not null)
        {
            var mapped_object = mapper.Map<CattlePosition>(pos);
            await repo.CreatePosition(mapped_object);
            await repo.SaveChanges();

            var result = mapper.Map<PositionReadDto>(mapped_object);

            return Results.Created($"Gjedhja me id {result.Id} u krijua!", result);
        }
        return Results.NoContent();
    });

    app.MapPut("api/v1/position/{id}", async (IPositionRepository repo, IMapper mapper, PositionUpdateDto pos, int id) =>
    {
        if (pos is not null)
        {
            var mapped_object = mapper.Map<CattlePosition>(pos);
            repo.UpdatePosition(mapped_object, id);
            await repo.SaveChanges();

            var result = mapper.Map<PositionReadDto>(mapped_object);

            return Results.Created($"Gjedhja me id {result.Id} u editua!", result);
        }
        return Results.NoContent();
    });

    //krijimi i api per editimin e gjedhes

    app.MapGet("api/v1/positions/{id}", async (IPositionRepository repo, IMapper mapper, int id) =>
    {
        var pos = await repo.GetLastPosition(id);
        if (pos is not null)
        {
            return Results.Ok(mapper.Map<PositionReadDto>(pos));
        }
        else
        {
            return Results.NotFound(new { error = "not found" });
        }

    });
    //perdorimi i repository, mapper per mshefjen e struktures se databazes 


    app.MapGet("api/v1/farmers", async (IFarmerRepository repo, IMapper mapper) =>
    {
        var farmers = await repo.GetAllFarmers();
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

    app.MapDelete("api/v1/farmers/{id}", async (IFarmerRepository repo, int id, IMapper mapper) =>
    {
        var farmer = await repo.GetFarmerById(id);
        if (farmer is not null)
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