using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace CytometrixWebAPI.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetErrorsMessage(this ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();

            foreach (var modelStateKey in modelState.Keys)
            {
                var modelStateVal = modelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var key = modelStateKey;
                    var errorMessage = error.ErrorMessage;
                    if (string.IsNullOrEmpty(errorMessage)) errorMessage = "Bad Value";

                    errors.Add($" {key}: {errorMessage}");
                }
            }

            return string.Join(',', errors).TrimStart();
        }
    }
}
