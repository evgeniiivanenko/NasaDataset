using Microsoft.EntityFrameworkCore;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Mappings;
using NasaDataset.Application.Common.Models;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.ValueObjects;
using Npgsql;
using System.Data;
using System.Text.Json;
using static Dapper.SqlMapper;

namespace NasaDataset.Infrastructure.Repositories
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IDbConnection _connection;

        public MeteoriteRepository(IApplicationDbContext context, IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }

        public async Task<PaginatedList<TResult>> GetFilteredAsync<TResult>(
            ISpecification<Meteorite> specification,
            ISortingStrategy<Meteorite> sorting,
            Func<Meteorite, TResult> selector,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var query = _context.Meteorites.AsQueryable();
            query = specification.Apply(query);

            query = sorting.ApplySort(query);


            return await query.ToPaginatedListAsync(pageNumber, pageSize, selector, ct);
        }

        public async Task AddRangeAsync(IEnumerable<Meteorite> meteorites, CancellationToken ct = default)
        {
            await _context.Meteorites.AddRangeAsync(meteorites, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task AddRangeWithHighPerformance(IEnumerable<Meteorite> meteorites, CancellationToken ct = default)
        {
            try
            {
                if (_connection is not NpgsqlConnection npgsqlConnection)
                    throw new InvalidOperationException("IDbConnection must be of type NpgsqlConnection");

                await using var connection = npgsqlConnection;
                await connection.OpenAsync();

                var ids = meteorites.Select(m => Guid.NewGuid()).ToArray();
                var externalIds = meteorites.Select(m => m.ExternalId.Id).ToArray();
                var names = meteorites.Select(m => m.Name).ToArray();
                var nameTypes = meteorites.Select(m => m.NameType.ToString()).ToArray();
                var recclasses = meteorites.Select(m => m.Recclass).ToArray();
                var masses = meteorites.Select(m => m.Mass).ToArray();
                var falls = meteorites.Select(m => m.Fall.ToString()).ToArray();
                var years = meteorites.Select(m => m.Year).ToArray();
                var reclats = meteorites.Select(m => m.Reclat.Value).ToArray();
                var reclongs = meteorites.Select(m => m.Reclong.Value).ToArray();
                var geolocations = meteorites
                     .Select(m => m.Geolocation != null
                         ? JsonSerializer.Serialize(m.Geolocation)
                         : null)
                     .ToArray();
                var region1 = meteorites.Select(m => m.ComputedRegionCbhkFwbd).ToArray();
                var region2 = meteorites.Select(m => m.ComputedRegionNnqa25f4).ToArray();

                await connection.ExecuteAsync(@"
                    INSERT INTO public.""Meteorites""(
	                    ""Id"", ""ExternalId"", ""Name"", ""NameType"", ""Recclass"", ""Mass"", ""Fall"", ""Year"", ""Reclat"", ""Reclong"", ""Geolocation"", ""ComputedRegionCbhkFwbd"", ""ComputedRegionNnqa25f4"")
                    SELECT 
                        unnest(@Ids), unnest(@ExternalIds), unnest(@Names), unnest(@NameTypes), unnest(@Recclasses),
                        unnest(@Masses), unnest(@Falls), unnest(@Years), unnest(@Reclats), unnest(@Reclongs),
                        unnest(@Geolocations)::jsonb,
                        unnest(@Region1), unnest(@Region2)", new
                {
                    Ids = ids,
                    ExternalIds = externalIds,
                    Names = names,
                    NameTypes = nameTypes,
                    Recclasses = recclasses,
                    Masses = masses,
                    Falls = falls,
                    Years = years,
                    Reclats = reclats,
                    Reclongs = reclongs,
                    Geolocations = geolocations,
                    Region1 = region1,
                    Region2 = region2
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task DeleteRangeAsync(List<ExternalMeteoriteId> idsToRemove, CancellationToken ct = default)
        {
            if (idsToRemove == null) throw new ArgumentNullException(nameof(idsToRemove));

            if (idsToRemove.Any())
            {
                await _context.Meteorites
                .Where(m => idsToRemove.Contains(m.ExternalId))
                .ExecuteDeleteAsync(ct);

                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<List<string>> GetDistinctRecclassesAsync(CancellationToken cancellationToken)
        {
            return await _context.Meteorites
                .Where(x => x.Recclass != null)
                .Select(x => x.Recclass!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<int>> GetDistinctYearsAsync(CancellationToken cancellationToken)
        {
            return await _context.Meteorites
                .Where(x => x.Year != null)
                .Select(x => x.Year!.Value)
                .Distinct()
                .OrderByDescending(x => x)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ExternalMeteoriteId>> GetExternalIdsAsync(CancellationToken cancellationToken)
            => await _context.Meteorites.Select(x => x.ExternalId).ToListAsync(cancellationToken);

        public async Task<PaginatedList<TResult>> GetGroupedAsync<TResult>(ISpecification<Meteorite> specification, ISortingStrategy<TResult> sortingStrategy, IGroupingStrategy<Meteorite, TResult> groupingStrategy, int pageNumber, int pageSize, CancellationToken ct = default) where TResult : class
        {
            var query = _context.Meteorites.AsQueryable();

            query = specification.Apply(query);

            var groupedQuery = groupingStrategy.Group(query);
            groupedQuery = sortingStrategy.ApplySort(groupedQuery);

            return await groupedQuery.ToPaginatedListAsync(pageNumber, pageSize, ct);
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Meteorites\";");
        }
    }
}
