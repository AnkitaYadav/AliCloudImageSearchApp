using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSearch
{
    public class IncrementMetaFile
    {
       [JsonProperty("operator")]
        public string Operator { get; set; }
        public string item_id { get; set; }
        public int cat_id { get; set; }
        public string cust_content { get; set; }
        public Array pic_list { get; set; }

    }
}
