using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Infrastruct
{
    internal class AzureStorage
    {
        /*
         * public class AzureStorage : Storage, IAzureStorage
{
    // Blob servis istemcisini tutar
    readonly BlobServiceClient _blobServiceClient;
    // Blob konteyner istemcisini tutar
    BlobContainerClient _blobContainerClient;

    // Constructor: Yapılandırmadan Blob hizmet istemcisini oluşturur
    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storage:Azure"]);
    }

    // Dosyayı siler
    public async Task DeleteAsync(string containerName, string fileName)
    {
        // Blob konteyner istemcisini alır
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        // Blob istemcisini alır ve dosyayı siler
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    // Belirli bir konteynerdeki dosyaları listeler
    public List<string> GetFiles(string containerName)
    {
        // Blob konteyner istemcisini alır ve dosyaları listeler
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    // Belirli bir dosyanın belirli bir konteynerde bulunup bulunmadığını kontrol eder
    public bool HasFile(string containerName, string fileName)
    {
        // Blob konteyner istemcisini alır ve dosyanın varlığını kontrol eder
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    // Dosyaları yükler
    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        // Blob konteyner istemcisini alır ve varsa oluşturur
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        // Yüklenen dosyaların bilgilerini tutar
        List<(string fileName, string pathOrContainerName)> datas = new();
        // Her bir dosya için yükleme işlemi yapar
        foreach (IFormFile file in files)
        {
            // Dosya ismini yeniden adlandırır
            string newFileName = await FileRenameAsync(containerName, file.Name, HasFile);
            // Blob istemcisini alır ve dosyayı yükler
            BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            // Dosya bilgilerini listeye ekler
            datas.Add((newFileName, $"{containerName}/{newFileName}"));
        }
        // Yüklenen dosyaların bilgilerini döndürür
        return datas;
    }
}

         * 
         * 
         */
    }
}
