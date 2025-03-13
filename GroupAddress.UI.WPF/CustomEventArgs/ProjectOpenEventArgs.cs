using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI.WPF.CustomEventArgs
{
    public class ProjectOpenEventArgs : EventArgs
    {
        public Project Project { get; set; }

        public FileInfo? ProjectFile { get; set; }

        public ProjectOpenEventArgs(Project project, FileInfo? fileInfo)
        {
            Project = project;
            ProjectFile = fileInfo;
        }

    }
}
