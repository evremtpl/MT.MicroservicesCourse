using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.FreeCourse.Services.PhotoStock.Dtos;
using MT.FreeCourse.Shared.ControllerBases;
using MT.FreeCourse.Shared.Dtos;

namespace MT.FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken) // if the process is terminated by user, while saving photo. the process will also be terminated here too, not continue. cancellationToken is for this.
                                                                                                         //  An asynchronous operation terminates only by throwing exception.CancellationToken is doing this here
        {
             if(photo != null && photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream,cancellationToken);

                var returnpath="photos/" +photo.FileName;

                PhotoDto photoDto= new() { Url = returnpath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto,201));
            }
             else
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
        }

        public IActionResult PhotoDelete(string photoUrl, CancellationToken cancellationToken)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos" + photoUrl);

            if(!System.IO.File.Exists(path))
            {
                return  CreateActionResultInstance(Response<NoContent>.Fail("photo not found",404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
