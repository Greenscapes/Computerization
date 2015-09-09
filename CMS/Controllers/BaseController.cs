using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CMS.Controllers
{
    public class BaseController : ApiController
    {

        public ValidationResponse GetValidationResponse()
        {
            var errorList = new List<ValidationError>();

            foreach (var key in ModelState.Keys)
            {
                System.Web.Http.ModelBinding.ModelState mstate = null;
                if (ModelState.TryGetValue(key, out mstate))
                {

                    foreach (var error in mstate.Errors)
                    {
                        errorList.Add(new ValidationError()
                        {
                            Key = key,
                            Message = error.ErrorMessage
                        });
                    }
                }
            }

            var response = new ValidationResponse()
            {
                Type = "Validation",
                Message = "",
                Errors = errorList
            };
            return response;
        }
    }

    public class ValidationResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationResponse()
        {
            Errors = new List<ValidationError>();
        }


    }

    public class ValidationError
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }
}