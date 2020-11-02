using System;
using System.Collections.Generic;
using System.Text;

namespace EdFi.Roster.Models
{
    public class ExtendedInfoResponse<T> where T : new()
    {
        public ExtendedInfoResponse()
        {
            this.Pages = new List<ExtendedInfoResponsePage<T>>();
            this.FullDataSet = new T();
        }
        public List<ExtendedInfoResponsePage<T>> Pages { get; set; }
        public T FullDataSet { get; set; }
    }
    public class ExtendedInfoResponsePage<T>
    {
        public T Data { get; set; }
        public Uri ResponseUri { get; set; }
    }
}
