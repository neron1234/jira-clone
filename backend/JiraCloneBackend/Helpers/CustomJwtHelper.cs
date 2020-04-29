using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Helpers
{
    public class CustomJwtHelper
    {

        private Object _payloadObject;
        private Object headerObj;

        public CustomJwtHelper(Object payloadObject)
        {
            _payloadObject = payloadObject;
            headerObj = new
            {
                alg = "HS256",
                typ = "JWT"
            };
        }

        public string getToken()
        {

            return "token";
        }


    }
}
