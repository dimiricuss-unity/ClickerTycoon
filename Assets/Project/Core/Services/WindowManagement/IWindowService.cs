using System.Threading.Tasks;

namespace Project.Core.Services.WindowManagement
{
    public interface IWindowService
    {
        Task OpenWindow(string windowId);
    }
}