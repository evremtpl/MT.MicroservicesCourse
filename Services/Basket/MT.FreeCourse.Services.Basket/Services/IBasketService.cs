using MT.FreeCourse.Services.Basket.Dtos;
using MT.FreeCourse.Shared.Dtos;

namespace MT.FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        public Task<Response<BasketDto>> GetBasket(string userId);
        public Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        public Task<Response<bool>> Delete(string userId);
    }
}
