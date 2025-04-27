public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile imageFile);
    Task DeleteImageAsync(string imagePath);
    bool IsImageValid(IFormFile imageFile);
}

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private const long _maxFileSize = 5 * 1024 * 1024; // 5MB

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        if (!IsImageValid(imageFile))
            throw new InvalidOperationException("Invalid image file");

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return Path.Combine("uploads", "products", uniqueFileName).Replace("\\", "/");
    }

    public async Task DeleteImageAsync(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath)) return;

        var fullPath = Path.Combine(_environment.WebRootPath, imagePath);
        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath));
        }
    }

    public bool IsImageValid(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0) return false;
        if (imageFile.Length > _maxFileSize) return false;

        var extension = Path.GetExtension(imageFile.FileName).ToLower();
        return _allowedExtensions.Contains(extension);
    }
}