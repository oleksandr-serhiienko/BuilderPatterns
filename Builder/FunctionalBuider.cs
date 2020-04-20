using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace Builder
{
    public class FunctionalBuider
    {
        public static void Main3(string[] args)
        {
            var person = new PersonBuilderOther()
                .Called("Sasha")
                .WorkAS("SoftwareDeveloper")
                .Build();
        }
    }

    public class PersonOther
    {
        public string Name, Position;
    }


    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<PersonOther, PersonOther>> actions
            = new List<Func<PersonOther, PersonOther>>();

        public TSelf Do(Action<PersonOther> action)
            => AddAction(action);

        public PersonOther Build() => actions.Aggregate(new PersonOther(), (p, f) => f(p));

        private TSelf AddAction(Action<PersonOther> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilderOther
        : FunctionalBuilder<PersonOther, PersonBuilderOther>
    {
        public PersonBuilderOther Called(string name)
            => Do(p => p.Name = name);
    }


    //public sealed class PersonBuilderOther
    //{
    //    private  readonly List<Func<PersonOther, PersonOther>> actions 
    //        = new List<Func<PersonOther, PersonOther>>();

    //    public PersonBuilderOther Called(string name)
    //        => Do(p => p.Name = name);

    //    public PersonBuilderOther Do(Action<PersonOther> action) 
    //        => AddAction(action);

    //    public PersonOther Build() => actions.Aggregate(new PersonOther(), (p, f) => f(p));

    //    private PersonBuilderOther AddAction(Action<PersonOther> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }
    //}

    public static class PersonOtherBuilderExtension
    {
        public static PersonBuilderOther WorkAS
            (this PersonBuilderOther builder, string position)
            => builder.Do(p => p.Position = position);
    }
}
