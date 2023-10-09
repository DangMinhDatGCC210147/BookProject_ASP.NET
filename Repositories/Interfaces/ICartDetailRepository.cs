using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ICartDetailRepository
    {
        void SaveCartDetail(CartDetail cartDetail);
        CartDetail FindCartDetailById(int id);
        void DeleteCartDetailById(CartDetail cartDetail);
        void UpdateCartDetail(CartDetail cartDetail);
    }
}