using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

public class CompileHelper
{
    public static CompilerResults CompileCodeFromString(string code, string[] references = null, CompilerParameters CompilerParams = null)
    {

        if (CompilerParams == null)
        {
            CompilerParams = new CompilerParameters();
            CompilerParams.GenerateInMemory = true;
            CompilerParams.TreatWarningsAsErrors = false;
            CompilerParams.GenerateExecutable = false;
            CompilerParams.CompilerOptions = "/optimize";
        }

        if (references != null)
        {
            CompilerParams.ReferencedAssemblies.AddRange(references);
        }

        CSharpCodeProvider provider = new CSharpCodeProvider();
        return  provider.CompileAssemblyFromSource(CompilerParams, code);
    }
	
}
