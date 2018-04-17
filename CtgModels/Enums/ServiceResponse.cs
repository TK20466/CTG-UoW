using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.ServiceModels;

namespace CtgModels.Enums
{
    public class ServiceResponse
    {
        public ServiceResponseType Result { get; }

        public ServiceResponse(ServiceResponseType result)
        {
            Result = result;
        }

    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; }

        public ServiceResponse(T data, ServiceResponseType result) : base(result)
        {
            Data = data;
        }

    }

    public enum ServiceResponseType
    {
        NoChange = 0,
        Updated = 1,
        Created = 2,
        Deleted = 3
    }
}
