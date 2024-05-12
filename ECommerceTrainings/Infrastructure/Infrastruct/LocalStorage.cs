using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Infrastruct
{
    internal class LocalStorage
    {
        /*
         * public class LocalStorage : Storage, ILocalStorage
{
    // Web ana bilgisayar ortamı arayüzü
    private readonly IWebHostEnvironment _webHostEnvironment;

    // Constructor: Web ana bilgisayar ortamı arayüzünü enjekte eder
    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    // Dosyayı siler
    public async Task DeleteAsync(string path, string fileName)
        => File.Delete($"{path}\\{fileName}");

    // Belirli bir dizindeki dosyaları listeler
    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    // Belirli bir dizinde belirli bir dosyanın olup olmadığını kontrol eder
    public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

    // Dosyayı kopyalar
    async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            // Dosya akışını oluşturur ve dosyayı kopyalar
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Hata durumunda loglama yapar ve hatayı yeniden fırlatır
            // TODO: Loglama işlemi yapılabilir
            throw ex;
        }
    }

    // Dosyayı yükler
    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        // Yükleme yolunu oluşturur
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        // Yükleme dizini yoksa oluşturur
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Yüklenen dosyaların bilgilerini tutar
        List<(string fileName, string path)> datas = new();
        // Her bir dosya için yükleme işlemi yapar
        foreach (IFormFile file in files)
        {
            // Dosya ismini yeniden adlandırır
            string newFileName = await FileRenameAsync(uploadPath, file.Name, HasFile);

            // Dosyayı kopyalar ve yüklenen dosyanın bilgilerini listeye ekler
            await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
            datas.Add((newFileName, $"{uploadPath}\\{newFileName}"));
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
