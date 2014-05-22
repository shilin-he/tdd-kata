using System.IO;

namespace max.web.razor
{
    public class RazorFileTemplate : IContainTemplateInfo
    {
        IProvideAppBaseDirectory provide_app_base_directory;
        string razor_template_file_path;

        public RazorFileTemplate(string razor_template_file_path, IProvideAppBaseDirectory provide_app_base_directory)
        {
            this.razor_template_file_path = razor_template_file_path;
            this.provide_app_base_directory = provide_app_base_directory;
        }

        public string content
        {
            get
            {
                var path = Path.Combine(provide_app_base_directory(), razor_template_file_path);
                return File.ReadAllText(path);
            }
        }
    }
}