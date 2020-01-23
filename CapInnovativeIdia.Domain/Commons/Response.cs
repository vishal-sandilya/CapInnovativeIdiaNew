using System;
using System.Collections.Generic;
using System.Text;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.Domain.Commons
{
    public class Response
    {
        public string ResponseMessage { get; set; }
        public ResponseType ResponseType { get; set; }
    }
}
