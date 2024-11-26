using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Common
{
    public static class EntityValidationConstans
    {
        public static class Service
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength= 500;
            public const string PriceValidation = "decimal(18,2)";
        }

        public static class User
        {
            public const int UsernameMaxLength = 50;
            public const int EmailMaxLength = 50;
            public const int PhoneMaxLength = 15;
            public const string DefaultRole = "Client";
        }

        public static class Review
        {
            public const int CommentMaxLength = 500;
            public const int DefaultRating = 1;
        }

        public static class Employee
        {
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const int PhoneMaxLength = 15;
            public const int EmailMaxLength = 50;
            public const int SpezializationMaxLength = 100;
            public const string HourlyRateValidation = "decimal(18,2)";
        }

        public static class Client
        {
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const bool PreferredTimeValidation = false;
        }

        public static class Appointment
        {
            public const string TotalPriceValidation = "decimal(18,2)";
            public const int StatusMaxLength = 50;
            public const string StatusDefaultValue = "Pending";

        }



    }
}
