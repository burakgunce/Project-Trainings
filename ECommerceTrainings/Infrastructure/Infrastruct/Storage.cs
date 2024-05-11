using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Infrastruct
{
    internal class Storage
    {
        /*
         * public class Storage
{
    // Dosyanın var olup olmadığını kontrol etmek için kullanılacak delegenin tanımı
    protected delegate bool HasFile(string pathOrcontainerName, string fileName);

    // Dosya yeniden adlandırma işlemini gerçekleştiren asenkron metot
    protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
    {
        // Yeniden adlandırılmış dosya adını döndürecek olan Task<string> türündeki iş parçacığı başlatılır
        string newFileName = await Task.Run<string>(async () =>
        {
            // Dosya uzantısı alınır
            string extension = Path.GetExtension(fileName);
            // Yeni dosya adı için bir değişken oluşturulur
            string newFileName = string.Empty;

            // İlk adım kontrol edilir
            if (first)
            {
                // Dosyanın adı alınır ve karakter düzenlemesi yapılır
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                // Yeniden adlandırma işlemi
                newFileName = fileName;
                int indexNo1 = newFileName.IndexOf("-");
                
                // "-" karakteri bulunamazsa dosya adı "-2" ile tamamlanır
                if (indexNo1 == -1)
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                else
                {
                    // "-" karakteri bulunursa, dosya numarası artırılır
                    int lastIndex = 0;
                    while (true)
                    {
                        lastIndex = indexNo1;
                        indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                        if (indexNo1 == -1)
                        {
                            indexNo1 = lastIndex;
                            break;
                        }
                    }

                    int indexNo2 = newFileName.IndexOf(".");
                    string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                    if (int.TryParse(fileNo, out int _fileNo))
                    {
                        _fileNo++;
                        newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                                            .Insert(indexNo1 + 1, _fileNo.ToString());
                    }
                    else
                        // Eğer dosya numarası dönüştürülemezse, dosya adı "-2" ile tamamlanır
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";

                }
            }

            // Dosyanın var olup olmadığı kontrol edilir
            if (hasFileMethod(pathOrContainerName, newFileName))
                // Var ise, yeni dosya adı için tekrar yeniden adlandırma işlemi yapılır
                return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
            else
                // Yok ise, yeni dosya adı döndürülür
                return newFileName;
        });

        // Yeniden adlandırılmış dosya adı döndürülür
        return newFileName;
    }
}

         * 
         * 
         */
    }
}
