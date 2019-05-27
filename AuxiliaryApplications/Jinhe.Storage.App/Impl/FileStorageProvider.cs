using Jinhe.Storage.Dto;
using System;
using System.IO;

namespace Jinhe.Storage.Impl
{
    public class FileStorageProvider : IFileStorageProvider
    {
        private readonly string _basePath;
        private readonly string _uploadPath;
        private PathResolver _pathResolver = new PathResolver();

        public FileStorageProvider(string basePath, string uploadPath)
        {
            _basePath = basePath;
            _uploadPath = uploadPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archive"></param>
        /// <returns>6位ZONE，6位日期，3位类型索引，32位MD5</returns>
        public string GetStoragePhysicalPath(IStoredFile archive)
        {
            var targetPath = Path.Combine(_basePath, _uploadPath, archive.Path);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"targetPath:{targetPath}");
            Console.ForegroundColor = ConsoleColor.White;
            return targetPath;
        }

        private Stream _fileStream;
        public Stream GetFileStream(IStoredFile archive)
        {
            var path = GetStoragePhysicalPath(archive);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"path:{path}");
            Console.ForegroundColor = ConsoleColor.White;
            if (_fileStream != null)
            {
                return _fileStream;
            }
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            return _fileStream;
        }

        /// <summary>
        /// 获取指定类型文件
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public string GetStoragePhysicalPath(IStoredFile archive, FileType fileType)
        {
            string fileName = archive.Path.Substring(0, archive.Path.LastIndexOf('.')) + ".pdf";
            var targetPath = Path.Combine(_basePath, _uploadPath, fileName);
            string type = string.Empty;
            switch (fileType)
            {
                case FileType.pdf:
                    type = ".pdf";
                    break;
                case FileType.doc:
                    type = ".doc";
                    break;
                case FileType.docx:
                    type = ".docx";
                    break;
                case FileType.xls:
                    type = ".xls";
                    break;
                case FileType.xlsx:
                    type = ".xlsx";
                    break;
                case FileType.txt:
                    type = ".txt";
                    break;
                default:
                    throw new Exception("暂时不支持获取该类型文件！");
            }
            return targetPath.ToLower().Contains(type) ? targetPath : string.Empty;
        }

        /// <summary>
        /// 保存文件为Pdf格式
        /// </summary>
        /// <param name="path">文件路径</param>
        public void SaveFileToPdf(Impl.StoredFile archive, string path)
        {
            //string prefix = path.Substring(0, path.LastIndexOf('.'));
            string suffix = path.Substring(path.LastIndexOf('.'));
            string prefix = Path.Combine(_basePath, "upload", "pdf", archive.FileId);
            string targetPath = prefix + ".pdf";
            FileType type = GetFileType(suffix);
            switch (type)
            {
                case FileType.pdf:
                case FileType.txt:
                    return;
                case FileType.doc:
                case FileType.docx:
                    SaveWordFile(path, targetPath);
                    break;
                case FileType.xls:
                case FileType.xlsx:
                    SaveExcelFile(path, targetPath);
                    break;
            }
        }

        /// <summary>
        /// 保存Excel文件
        /// </summary>
        /// <param name="path">文件路径</param>
        private void SaveExcelFile(string sourcePath, string tagetPath)
        {
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(sourcePath);
            if (!Directory.Exists(Path.GetDirectoryName(tagetPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(tagetPath));
            workbook.Save(tagetPath, Aspose.Cells.SaveFormat.Pdf);
        }

        /// <summary>
        /// 保存Word文件
        /// </summary>
        /// <param name="path">文件路径</param>
        private void SaveWordFile(string sourcePath, string tagetPath)
        {
            Aspose.Words.Document document = new Aspose.Words.Document(sourcePath);
            document.Save(tagetPath, Aspose.Words.SaveFormat.Pdf);
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        private FileType GetFileType(string suffix)
        {
            switch (suffix.ToLower())
            {
                case ".pdf":
                    return FileType.pdf;
                case ".doc":
                    return FileType.doc;
                case ".docx":
                    return FileType.docx;
                case ".xls":
                    return FileType.xls;
                case ".xlsx":
                    return FileType.xlsx;
                case ".text":
                    return FileType.txt;
                default:
                    return FileType.txt;
            }
        }
    }
}
