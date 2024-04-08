using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Clients
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private double _weight;
        private double _height;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First Name is required. Must not be empty or blank. ");
                }
                else
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last Name is required. Must not be empty or blank. ");
                }
                else
                {
                    _lastName = value;
                }
            }
        }

        public double Weight
        {
            get { return _weight; }
            set
            {
                
                    _weight = value;
                
            }
        }

        public double Height
        {
            get { return _height; }
            set
                {

                    _height = value;
               }
            }
        

        public Client() 
        {
            FirstName = "xx";
            LastName = "xx";
            Weight = 0;
            Height = 0;
        }

        public Client(string firstName, string lastName, double height, double weight)
        {
            FirstName = firstName;
            LastName = lastName;
            Weight = weight;
            Height = height;
        }

        public double BmiScore
        {
            get
            {
                // weight in pounds
                // height in inches
                double score = Weight / (Math.Pow(Height, 2)) * 703;
                return score;
            }
        }

        public string BmiStatus
        {
            get
            {
                double score = BmiScore;
                if (score <= 18.4)
                    return "Underweight";
                else if (score >= 18.5 && score <= 24.9)
                    return "Normal";
                else if (score >= 25.0 && score <= 39.9)
                    return "Overweight";
                else if (score >= 40)
                    return "Obese";
                else
                    return "";
            }
        }

        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
    }
}


