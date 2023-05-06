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
        public int StatusCode;

        public object Data;
        
        public ValidationDTO(HttpStatusCode httpStatusCode,bool isSucesfull, int statusCode, object data)
        {
            HttpStatusCode= httpStatusCode;
            IsSucesfull= isSucesfull;
            StatusCode= statusCode; 
            Data= data;
        }
    }
}
