using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class Persona
    {
        public string StreetAdress, PostCode, City;

        public string CompanyName, Position;
        public int AnnualINcotme;

        public override string ToString()
        {
            return
                $"{nameof(StreetAdress)}: {StreetAdress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualINcotme)}: {AnnualINcotme}";
        }
    }

    public class PersonaBuilder // facade
    {
        // reference:
        protected Persona persona = new Persona();
         
        public  PersonaJobBuilder works => new PersonaJobBuilder(persona);
        public  PersonaAdressBuilder lives => new PersonaAdressBuilder(persona);

        public static implicit operator Persona(PersonaBuilder pb)
        {
            return pb.persona;
        }
    }

    public class PersonaAdressBuilder : PersonaBuilder
    {
        public PersonaAdressBuilder( Persona persona)
        {
            this.persona = persona;
        }

        public PersonaAdressBuilder At(string streetAddress)
        {
            persona.StreetAdress = streetAddress;
            return this;
        }

        public PersonaAdressBuilder WithPostcode(string postcode)
        {
            persona.PostCode = postcode;
            return this;
        }

        public PersonaAdressBuilder In(string city)
        {
            persona.City = city;
            return this;
        }

    }

    public class PersonaJobBuilder : PersonaBuilder
    {
        public PersonaJobBuilder(Persona persona)
        {
            this.persona = persona;
        }

        public PersonaJobBuilder At(string companyName)
        {
            persona.CompanyName = companyName;
            return this;
        }

        public PersonaJobBuilder AsA(string position)
        {
            persona.Position = position;
            return this;
        }

        public PersonaJobBuilder Earning(int amount)
        {
            persona.AnnualINcotme = amount;
            return this;
        }
    }


    public class FacetedBuilder
    {
        public static void Mainsss()
        {
            var pf = new PersonaBuilder();
            Persona pes = pf.works.At("ddd")
                .AsA("smb")
                .Earning(33)
                .lives.At("33R").In("kIRVOGRGAD").WithPostcode("youMom");

            Console.WriteLine(pes);


        }
    }
}
