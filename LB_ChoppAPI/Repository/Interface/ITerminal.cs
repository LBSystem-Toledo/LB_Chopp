using LB_ChoppAPI.Models;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface ITerminalMobile
    {
        Task<bool> ValidarDeviceAsync(string Token, TerminalMobile terminal);
    }
}
