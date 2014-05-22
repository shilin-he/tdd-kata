 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using max.web.razor;

namespace max.web.specs
{   

    [Subject(typeof(RazorFileTemplate))]
    public class RazorFileTemplateSpecs
    {
        public abstract class concern : Observes<RazorFileTemplate>
        {
        
        }

        public class when_get_content : concern
        {
            Establish c = () =>
            {
                template_path = @"stubs\test_razor.cshtml";
                depends.on(template_path);
                depends.on<IProvideAppBaseDirectory>(() => AppDomain.CurrentDomain.BaseDirectory);
                file_content = "test template";
            };

            Because b = () =>
                result = sut.content;

            It reads_content_from_the_file_specifed_by_the_path = () =>
                result.ShouldEqual(file_content);

            static string result;
            static string file_content ;
            static string template_path;
        }
    }
}
