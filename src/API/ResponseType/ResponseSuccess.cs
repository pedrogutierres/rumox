using System;

namespace Rumox.API.ResponseType
{
    public class ResponseSuccess
    {
        public Guid? Id { get; set; }
    }

    public class ResponseSuccess<T> : ResponseSuccess
    {
        public T Data { get; set; }
    }
}
