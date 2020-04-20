using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;    
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace((Text)))
            {
                sb.Append(new string(' ', indentSize * (indentSize + 1)));
                sb.AppendLine(Text);
            }

            foreach (var el in Elements)
            {
                sb.Append(el.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"/{i}<{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0); 
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement(String.Empty, String.Empty);

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = this.rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement (string.Empty, string.Empty){ Name = rootName};
        }
    }


    class Builder
    {
        static void Main1(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");


            Console.WriteLine(sb);

            var words = new[] {"hello", "word"};
            sb.Clear();
            sb.Append("<ul>");

            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("/<ul>");

            Console.WriteLine(sb);

            var bulder = new HtmlBuilder("ul");
            bulder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(bulder.ToString());
        }
    }
}
