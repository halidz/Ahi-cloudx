using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.BusinessImplementation
{
    public class LookupFacade : ILookupFacade
    {
        //public string GetLookup(string key)
        //{
        //    using (StreamReader r = new StreamReader("lookups.json"))
        //    {
        //        var json = r.ReadToEnd();
        //        var items = JsonConvert.DeserializeObject<List<SchemaInfo>>(json);
        //        foreach (var item in items)
        //        {
        //            // Console.WriteLine("{0} {1}", item.temp, item.vcc);
        //        }
        //    }
        //    Product deserializedProduct = JsonConvert.DeserializeObject<Product>(lookups.json);
        //}
        public List<JsonView> GetLookup(string key)
        {
            var list = new List<JsonView>();
            using (StreamReader r = new StreamReader("lookups.json"))
            {

                string json = r.ReadToEnd();
                Console.WriteLine(json);
                JObject rss = JObject.Parse(json);

               
                var element = rss[key];

                if (element == null)
                    return null;
                if (element.Type ==JTokenType.Array)
                {
                    foreach(var elm in element)
                    {
                        var elmObj = (JObject)elm;
                        var properties = elmObj.Properties();

                        var jview = new JsonView();
                        int i = 1;
                        foreach (var prop in properties)
                        {

                            if (i == 1)
                            {
                                var c = prop.Name;
                                var z = elm[c].ToString();
                                jview.Value = z;
                            }
                            else
                            {
                                var c = prop.Name;
                                var z = elm[c].ToString();
                                jview.Text = z;
                            }
                            i++;
                        }
                       
                      
                      
                        list.Add(jview);

                      
                    }

                }
                else
                {
                    var eltemp = (JObject)element;

                    var properties = eltemp.Properties();
                    foreach (var prop in properties)
                    {
                        var jview = new JsonView();
                        var c = prop.Name;
                        var z = element[c].ToString();
                        jview.Text = c;
                        jview.Value = z;
                        list.Add(jview);

                    }



                }




            }
            return list;
        }
    }
}
