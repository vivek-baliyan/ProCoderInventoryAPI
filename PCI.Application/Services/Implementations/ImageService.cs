using PCI.Application.Services.Interfaces;

namespace PCI.Application.Services.Implementations;

public class ImageService : IImageService
{
    public async Task<string> UploadImage(string base64Data)
    {
        if (base64Data.Contains(','))
        {
            base64Data = base64Data.Split(',')[1];
        }

        byte[] imageBytes = Convert.FromBase64String(base64Data);

        // Generate a unique file name
        string fileName = $"{Guid.NewGuid()}.png";
        string folderPath = Path.Combine(Environment.CurrentDirectory, "Uploads");
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        await File.WriteAllBytesAsync(filePath, imageBytes);

        return $"Uploads/{fileName}";
    }

    // ImagePath to base64 string
    public async Task<string> ConvertImageToBase64(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath)) return string.Empty;

        if (imagePath.StartsWith('/') || imagePath.StartsWith('\\'))
        {
            imagePath = imagePath.TrimStart('/', '\\');
        }
        string fullPath = Path.Combine(Environment.CurrentDirectory, imagePath);
        byte[] imageBytes = await File.ReadAllBytesAsync(fullPath);
        return Convert.ToBase64String(imageBytes);
    }
}
