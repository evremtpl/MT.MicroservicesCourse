using MT.FreeCourse.Services.Catalog.Dtos;
using MT.FreeCourse.Services.Catalog.Models;
using MT.FreeCourse.Shared.Dtos;

namespace MT.FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        public Task<Response<List<CategoryDto>>> GetAllAsync();
        public Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        public Task<Response<CategoryDto>> GetByIdAsync(string id);
    }

}
