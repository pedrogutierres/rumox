using System;

namespace Rumox.API.ResponseType
{
    public class ResponseSuccess<T>
    {
        public T Data { get; set; }
        public Guid? Id { get; set; }
    }
}
