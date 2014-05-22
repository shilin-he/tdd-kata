using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Razor;
using Microsoft.CSharp;

namespace max.web.razor
{
  public class MaxRazorTemplateRenderEngine : IRenderTemplates
  {
    const string DefaultRazorNamespace = "__RazorOutput";
    const string DefaultRazorClasName = "__Template";
    IList<string> referenced_assemblies;

    public MaxRazorTemplateRenderEngine()
    {
      referenced_assemblies = new List<string>
      {
        "System.dll",
        "System.Core.dll",
        "Microsoft.CSharp.dll",
        "mscorlib.dll",
        typeof(RazorEngineHost).Assembly.Location,
        GetType().Assembly.Location,
        Assembly.GetCallingAssembly().Location
      };
    }

    static RazorTemplateEngine setup_razor_engine()
    {
      var host = new RazorEngineHost(new CSharpRazorCodeLanguage())
      {
        DefaultBaseClass = typeof(RazorTemplateBase).FullName,
        DefaultNamespace = DefaultRazorNamespace,
        DefaultClassName = DefaultRazorClasName
      };

      host.NamespaceImports.Add("System");

      return new RazorTemplateEngine(host);
    }

    RazorTemplateBase compile(string template)
    {
      var razor_engine = setup_razor_engine();
      GeneratorResults generator_results;
      using (var reader = new StringReader(template))
      {
        generator_results = razor_engine.GenerateCode(reader);
      }

      var c_sharp_code_provider = new CSharpCodeProvider();

      var compiler_options = new CompilerParameters(referenced_assemblies.ToArray())
      {
        GenerateInMemory = true
      };

      CompilerResults compiler_results = c_sharp_code_provider.CompileAssemblyFromDom(
        compiler_options, generator_results.GeneratedCode);

      if (compiler_results.Errors.HasErrors)
      {
        CompilerError err = compiler_results.Errors
          .OfType<CompilerError>().First(ce => !ce.IsWarning);
        throw new Exception(String.Format("Error Compiling Template: ({0}, {1}) {2}",
                                      err.Line, err.Column, err.ErrorText));
      }

      var type = compiler_results.CompiledAssembly.GetType(string.Format("{0}.{1}", DefaultRazorNamespace, DefaultRazorClasName));
      return Activator.CreateInstance(type) as RazorTemplateBase;
    }

    public string render<ViewModel>(IContainTemplateInfo template, ViewModel view_model)
    {
      var razor_template = compile(template.content);
      return razor_template.render(view_model);
    }
  }
}