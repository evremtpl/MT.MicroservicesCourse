using AutoMapper;
using MongoDB.Driver;
using MT.FreeCourse.Services.Catalog.Dtos;
using MT.FreeCourse.Services.Catalog.Models;
using MT.FreeCourse.Services.Catalog.Settings;
using MT.FreeCourse.Shared.Dtos;

namespace MT.FreeCourse.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = _categoryCollection.Find<Category>(x => x.Id == item.CategoryId).FirstOrDefault();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetByAllUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            //newCourse.Id=Guid.NewGuid().ToString();
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 201);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updatedCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updatedCourse);
            
           return result == null ?Response<NoContent>.Fail("Course Not Found", 404): Response<NoContent>.Success(204);  

        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Course Not Found", 404);
        }
    }
}