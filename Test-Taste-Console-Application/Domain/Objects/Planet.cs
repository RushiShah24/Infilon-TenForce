using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using System.Linq;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        public float AverageMoonGravity
        {
            get {
                if (Moons == null || Moons.Count == 0)
                    return 0.0f;

                //we could have used average method directly, but this is done to prevent unwanted errors in condition like null value etc.
                var validGravities = Moons
                    .Where(m => m != null)
                    .Select(m => m.Gravity)
                    .Where(g => !float.IsNaN(g) && !float.IsInfinity(g))
                    .ToList();

                if (!validGravities.Any())
                    return 0.0f;

                return validGravities.Average();
            }
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
