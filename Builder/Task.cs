using System;
using System.Collections.Generic;
using System.Text;

namespace Coding.Exercise
{

    public class ClassRepresentation
    {
        public string Name { get; set; }
        public List<FieldRepresentation> FieldRepresentations { get; set; } = new List<FieldRepresentation>();
    }

    public class FieldRepresentation
    {
        public string FieldName { get; set; }

        public string TypeName { get; set; }
    }



    public class CodeBuilder
    {
        ClassRepresentation classRepresentation = new ClassRepresentation();

        public CodeBuilder(string className)
        {
            
            classRepresentation.Name = className;
        }

        public CodeBuilder AddField(string fieldName, string typeName)
        {
            FieldRepresentation fiedFieldRepresentation 
                = new FieldRepresentation() 
                    { FieldName = fieldName, TypeName = typeName};

            classRepresentation.FieldRepresentations.Add(fiedFieldRepresentation);
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public class {classRepresentation.Name}");
            sb.AppendLine("{");
            foreach (var field in classRepresentation.FieldRepresentations)
            {
                sb.Append("\t").AppendLine($"public {field.TypeName} {field.FieldName};");
            }
            sb.AppendLine("}");
            
            return sb.ToString();
        }

        public static void Main()
        {
            var dd = new CodeBuilder("Check").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(dd);
        }
    }
}