using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<bool> SaveToFavorites(Favorite favorite);
        Task<List<Favorite>> GetAllFavoritesByUserId(string userId);
        Task<bool> RemoveFromFavorites(string userId, int adId);
        bool Save();
    }
}
