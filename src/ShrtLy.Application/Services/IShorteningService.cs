using ShrtLy.Domain;
using System.Threading.Tasks;

namespace ShrtLy.Application.Services
{
    public interface IShorteningService
    {
        string ProcessLink(string url);
    }
}