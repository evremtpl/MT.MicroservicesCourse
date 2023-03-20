using MT.FreeCourse.Services.Catalog.Dtos;
using MT.FreeCourse.Shared.Dtos;

namespace MT.FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        public Task<Response<List<CourseDto>>> GetAllAsync();
        public Task<Response<CourseDto>> GetByIdAsync(string id);
        public Task<Response<List<CourseDto>>> GetByAllUserIdAsync(string userId);
        public Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        public Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        public Task<Response<NoContent>> DeleteAsync(string id);
    }
}
