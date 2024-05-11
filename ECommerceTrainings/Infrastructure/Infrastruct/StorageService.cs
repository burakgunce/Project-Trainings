using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Infrastruct
{
    internal class StorageService
    {
        /*
         * public class StorageService : IStorageService
{
    // IStorage arayüzünden bir nesne alır
    readonly IStorage _storage;

    // Depolama servisi, bir IStorage uygulamasını alır ve enjekte eder
    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    // Depolama adını alır
    public string StorageName { get => _storage.GetType().Name; }

    // Belirtilen dosyayı siler
    public async Task DeleteAsync(string pathOrContainerName, string fileName)
        => await _storage.DeleteAsync(pathOrContainerName, fileName);

    // Belirtilen yol veya konteynerdeki dosyaların listesini alır
    public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

    // Belirtilen yol veya konteynerde belirtilen dosyanın olup olmadığını kontrol eder
    public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

    // Belirtilen yol veya konteynere dosya yükler
    public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => _storage.UploadAsync(pathOrContainerName, files);
}

         * 
         */
    }
}
