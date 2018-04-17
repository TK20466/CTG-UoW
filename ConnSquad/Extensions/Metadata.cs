using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CtgModels.ServiceModels;
using ConnSquad.Extensions;

namespace ConnSquad.Extensions
{
    public class CollectionData<T> where T : class
    {

        //todo expand with paging?
       public int TotalResults { get; set; }
       public int ReturnedResults { get; set; }
       public IEnumerable<T> Results { get; set; }
 
       public CollectionData(IEnumerable<T> collection)
       {
           Results = collection;
       }
    }

    public class MetaData<T> where T : BaseModel
    {
        public T Data { get; set; }
        public string Link { get; set; }

        public MetaData(HttpResponseMessage httpResponse)
        {
            if (httpResponse.Content == null || !httpResponse.IsSuccessStatusCode) return;
            T responseObj;
            httpResponse.TryGetContentValue(out responseObj);
            Data = responseObj;
            Link = responseObj.GetLink();
        }

        public MetaData(T model)
        {
            Data = model;
            Link = model.GetLink();
        }
    }

    public static class ModelLinkExtensions
    {
        public const string Prefix = "/api";

        public static string GetLink(this BaseModel model)
        {
            if (model is Event) return GetLink((Event) model);
            return null;
        }

        public static string GetLink(this Event model)
        {
            return $"{Prefix}/events/{model.Id}";
        }


    }

    public class MetadataHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
             task =>
             {
                 if (!ResponseIsValid(task.Result)) return task.Result;
                 object responseObject;
                 task.Result.TryGetContentValue(out responseObject);
                 if (responseObject is BaseModel)
                 {
                     ProcessObject(responseObject as BaseModel, task.Result);
                 }
                 else if (responseObject is IEnumerable && SameType(responseObject.GetType().GenericTypeArguments, typeof(BaseModel)))
                 {
                     ProcessObjectCollection(responseObject as IEnumerable<BaseModel>, task.Result);
                 }
                 return task.Result;
             }, cancellationToken);
        }

        private void ProcessObject<T>(T responseObject, HttpResponseMessage response) where T : BaseModel
        {
            var metadata = new MetaData<T>(response);
            response.Content = new ObjectContent<MetaData<T>>(metadata, GlobalConfiguration.Configuration.Formatters[0]);
        }



        private void ProcessObjectCollection<T>(IEnumerable<T> responseObject, HttpResponseMessage response) where T : BaseModel
        {
           var wrapped = responseObject.Select(x => new MetaData<T>(x));
            var collectionData = new CollectionData<MetaData<T>>(wrapped);
            response.Content = new ObjectContent<CollectionData<MetaData<T>>>(collectionData, GlobalConfiguration.Configuration.Formatters[0]);
        }

        public bool SameType(Type[] derived, Type b)
        {
            return derived.Any(t => t.IsSubclassOf(b) || t == b);
        }

        private bool ResponseIsValid(HttpResponseMessage response)
        {
            if (response == null || response.StatusCode != HttpStatusCode.OK || !(response.Content is ObjectContent)) return false;
            return true;
        }
    }
}