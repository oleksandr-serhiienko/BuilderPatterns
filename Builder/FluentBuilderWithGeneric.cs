using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace Builder
{
    public class Person
    {
        public string Name;

        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new  Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)} : {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    // class foo : Bar<foo>
    public class PersonInfoBuilder<SELF> 
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>

    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>

    {
        public SELF WorksAs(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    public class FluentBuilderWithGeneric
    {
        public static void Main2(string[] args)
        {
            var me = Person.New
                .Called("Sasha")
                .WorksAs("Software")
                .Build();

            Console.WriteLine(me);
        }
    }

}
