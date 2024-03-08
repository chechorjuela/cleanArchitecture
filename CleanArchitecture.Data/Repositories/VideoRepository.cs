using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Video> GetVideoByNombre(string nameVideo)
        {
            return await _context.Videos!.Where(v => v.Nombre == nameVideo).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUsername(string userName)
        {
            return await _context.Videos!.Where(v => v.CreatedBy == userName).ToListAsync();

        }
    }
}
