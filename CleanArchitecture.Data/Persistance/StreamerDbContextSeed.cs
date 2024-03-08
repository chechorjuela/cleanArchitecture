using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistance
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger) {
            if (!context.Streamers!.Any()) {
                context.Streamers!.AddRange(GetPreconfigureStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Se han insertado varios registros de streamers");
            }
        
        }

        private static IEnumerable<Streamer> GetPreconfigureStreamer() {
            return new List<Streamer>
            {
                new Streamer {CreatedBy = "verixdrez", Nombre = "Maxi HBP", Url= "http://MaxiHBP.dev.com"},
                new Streamer {CreatedBy = "verixdrez", Nombre = "Maxi CPM", Url= "http://MaxiCPM.dev.com"},
                new Streamer {CreatedBy = "verixdrez", Nombre = "Xavi RBP", Url= "http://XaviRBP.dev.com"},
                new Streamer {CreatedBy = "verixdrez", Nombre = "Massi HRP", Url= "http://MassiHRP.dev.com"},
                new Streamer {CreatedBy = "verixdrez", Nombre = "Mavi HBP", Url= "http://MaviHBP.dev.com"},
                new Streamer {CreatedBy = "verixdrez", Nombre = "Mati HBP", Url= "http://MatiHBP.dev.com"}

            };
        }
    }
}
