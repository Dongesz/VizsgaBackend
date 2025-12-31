using BackEnd.Application.DTOs;

namespace BackEnd.Application.Helpers
{
    public class UploadHelper
    {
        public async Task<ResponseOutputDto> UploadFileAsync(IFormFile file, string rootPath, string existingFileName = null)
        {
            var validExtensions = new List<string> { ".jpg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(ext))
                return new ResponseOutputDto { Message = $"Invalid file format! ({string.Join(',', validExtensions)})", Success = false };

            if (file.Length > 5 * 1024 * 1024)
                return new ResponseOutputDto { Message = "File size must be under 5MB!", Success = false };

            var fileName = Guid.NewGuid() + ext;
            var path = Path.Combine(rootPath, "images");

            if (!string.IsNullOrEmpty(existingFileName))
            {
                var oldPath = Path.Combine(path, existingFileName);
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }

            Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return new ResponseOutputDto
            {
                Message = "File successfully uploaded!",
                Success = true,
                Result = fileName
            };
        }
    }

}
