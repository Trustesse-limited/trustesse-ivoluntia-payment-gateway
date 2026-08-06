using Microsoft.AspNetCore.Mvc.ModelBinding;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Helper
{
    public class ResponseHelper
    {
        public static GlobalRequestReponse<T> BuildResponse<T>(string message, int reponseCode, T data, bool requestStatus = false
                                                      , ModelStateDictionary errs = null)
        {
            var listOfErrorItems = new List<ErrorItem>();

            if (errs != null)
            {
                foreach (var err in errs)
                {
                    var key = err.Key;
                    var errValues = err.Value;
                    var errList = new List<string>();
                    foreach (var errItem in errValues.Errors)
                    {
                        errList.Add(errItem.ErrorMessage);
                        listOfErrorItems.Add(new ErrorItem { Key = key, ErrorMessages = errList });
                    }
                }
            }

            var res = new GlobalRequestReponse<T>
            {
                isSuccessfull = requestStatus,
                Message = message,
                Data = data,
                ResponseCode = reponseCode,
                Errors = listOfErrorItems
            };
            return res;
        }
    }
}
