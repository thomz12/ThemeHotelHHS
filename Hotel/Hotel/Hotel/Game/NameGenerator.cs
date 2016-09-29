using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;
using System.IO;

namespace Hotel
{
    class NameGenerator
    {
        private string _femaleNamePath;
        private string _maleNamePath;
        private string _lastNamePath;

        private static Random _random = new Random();

        public NameGenerator()
        {

            _maleNamePath = @"Extra\MaleNames.txt";
            _femaleNamePath = @"Extra\FemaleNames.txt";
            _lastNamePath = @"Extra\LastNames.txt";
        }

        public string GenerateName(Gender gender)
        {
            string name = "";
            
            // Set a first name.
            if(gender == Gender.Female)
            {
                // Get a random female name
                string[] names = LoadNames(_femaleNamePath);
                name += names[_random.Next(0, names.Length)];
            }
            else if(gender == Gender.Male)
            {
                // Get a random male name
                string[] names = LoadNames(_maleNamePath);
                name += names[_random.Next(0, names.Length)];
            }
            else if(gender == Gender.Genderless)
            {
                // Get a random genderless name
                name += "God"; 
            }

            // Add a random surname
            string[] lastNames = LoadNames(_lastNamePath);
            name += " " + lastNames[_random.Next(0, lastNames.Length)];

            return name;
        }

        private string[] LoadNames(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
