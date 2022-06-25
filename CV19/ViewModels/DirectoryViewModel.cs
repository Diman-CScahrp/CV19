using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CV19.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    var directories = _DirectoryInfo
                        .EnumerateDirectories()
                        .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                    return directories;
                }
                catch(UnauthorizedAccessException e)
                {
                    Debug.Write(e.Message);
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                        .EnumerateFiles()
                        .Select(file_info => new FileViewModel(file_info.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryItems =>
            SubDirectories.Cast<object>().Concat(Files.Cast<object>());
        public string Name => _DirectoryInfo.Name; 
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string path)
        {
            _DirectoryInfo = new DirectoryInfo(path);
        }
    }
    class FileViewModel : ViewModel
    {
        private readonly FileInfo _FileInfo;
        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;
        public FileViewModel(string path)
        {
            _FileInfo = new FileInfo(path);
        }
    }
}
