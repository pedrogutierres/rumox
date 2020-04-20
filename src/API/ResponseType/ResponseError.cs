using System.Collections.Generic;

namespace Rumox.API.ResponseType
{
    public class ResponseError
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
