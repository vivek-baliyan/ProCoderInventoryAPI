namespace PCI.Application.Services.Interfaces;

public interface IImageService
{
    Task<string> UploadImage(string base64Data);
    Task<string> ConvertImageToBase64(string imagePath);
}