using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideoList
{
    public class GetVideoListQuery : IRequest<List<VideosVm>>
    {
        public string? _Username { get; set; } = String.Empty;
        public GetVideoListQuery(string username) {
            _Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
