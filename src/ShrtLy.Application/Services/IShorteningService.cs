using ShrtLy.Domain;
using System.Threading.Tasks;

namespace ShrtLy.Application.Services
{
    public interface IShorteningService
    {
        Task<string> ProcessLink(string url);
    }
}