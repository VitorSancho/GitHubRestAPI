using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHub.Business.Model
{
    public class ValidationDTO
    {
        public HttpStatusCode HttpStatusCode;
        public bool IsSucesfull;
        public string Message;
        public object Data;

        public ValidationDTO(HttpStatusCode httpStatusCode,bool isSucesfull, string message, object data=null)
        {
            HttpStatusCode= httpStatusCode;
            IsSucesfull= isSucesfull;
            Message = message;

            if(data != null)
                Data = data;
        }
    }
}
