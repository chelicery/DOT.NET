using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//DOT_VALIDATOR
namespace DOT.NET.OwnHelpers
{ 
    public class NipValidatorAttriubute : ValidationAttribute
    {


        public string GetErrorMessage1() =>
        $"Nip niepoprawny, Nip musi mieć co najmniej 10 znaków.";
        public string GetErrorMessage2() =>
        $"Nip niepoprawny, Nieprawidłowa cyfra kontrolna";

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var input = (string)value;
            int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            bool result = false;
            if (input.Length != 10)
            {
                return new ValidationResult(GetErrorMessage1());
            }
            int controlSum = CalculateControlSum(input, weights);
            int controlNum = controlSum % 11;
            if (controlNum == 10)
            {
                return new ValidationResult(GetErrorMessage2()); ;
            }
            int lastDigit = int.Parse(input[input.Length-1].ToString());
            result = controlNum == lastDigit;
            return ValidationResult.Success;
        }


        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }
    }      
}