using System;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour {

    [SerializeField]
    private string DefaultText;

    private InputField _inputField;

    public void Start()
    {
        _inputField = gameObject.GetComponent<InputField>();
        _inputField.text = DefaultText.Replace("\\n", "\n").Replace("\\t", "\t");       
    }

    public void Compile()
    {
        CompilerParameters CompilerParams = new CompilerParameters();
        string outputDirectory = Directory.GetCurrentDirectory();

        CompilerParams.GenerateInMemory = true;
        CompilerParams.TreatWarningsAsErrors = false;
        CompilerParams.GenerateExecutable = false;
        CompilerParams.CompilerOptions = "/optimize";

        string[] references = { "System.dll" };
        CompilerParams.ReferencedAssemblies.AddRange(references);

        CSharpCodeProvider provider = new CSharpCodeProvider();
        CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, _inputField.text);

        if (compile.Errors.HasErrors)
        {
            string text = "Compile error: ";
            foreach (CompilerError ce in compile.Errors)
            {
                text += "rn" + ce.ToString();
            }
            throw new Exception(text);
        }

        //ExpoloreAssembly(compile.CompiledAssembly);

        Module module = compile.CompiledAssembly.GetModules()[0];
        Type mt = null;
        MethodInfo methInfo = null;

        if (module != null)
        {
            mt = module.GetType("TestClass");
        }

        if (mt != null)
        {
            methInfo = mt.GetMethod("EchoStuff");
        }

        if (methInfo != null)
        {
            Debug.Log(methInfo.Invoke(null, new object[] { "Here's some stuff!" }));
        }

    }
}
