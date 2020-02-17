using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarsWars_IG
{
    public partial class StarShipModel
    {
        
        public string name { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
        public string length { get; set; }
        public string crew { get; set; }
        public string passengers { get; set; }
        public string cargo_capacity { get; set; }
        public string consumables { get; set; }
        public string hyperdrive_rating { get; set; }
        public string MGLT { get; set; }
        public string starship_class { get; set; }
        public List<string> pilots { get; set; }
        public List<string> films { get; set; }
        public string created { get; set; }
        public string edited { get; set; }
        public string url { get; set; }

        public StarShipModel() { }



    }
    public partial class JsonModel
    {
        private readonly string EndPoint = "https://swapi.co/api/starships/";
        public string next { get; set; }
        public List<StarShipModel> results { get; set; }
        public JsonModel jsonModel { get; set; }

        public JsonModel()
        {
            results = new List<StarShipModel>();
        }

        /// <summary>
        /// Returns one generic object.
        /// </summary>
        /// <param name="endPoint">The end point is the url request that will be used to get the result.</param>
        /// <returns>A 32-bit positive integer, representing the sum of the two specified numbers.</returns>
        /// <exception cref="NullReferenceException">
        /// When <paramref name="endPoint"/> is null.
        /// </exception>
        public T SendRequest<T>(string endPoint)
        {
            try
            {
                //create the web request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
                request.ContentType = "application/json";
                request.Method = "GET";
                using (var response = (HttpWebResponse)(request.GetResponse()))
                {
                    //check the status of the request from web server
                    if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                    {
                        //use streamreader to read
                        using (StreamReader read = new StreamReader(response.GetResponseStream()))
                        {
                            var result = read.ReadToEnd();
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = false,
                            };
                            //use of json deserialize to return the model tooked by the response from web api
                            return JsonSerializer.Deserialize<T>(result, options);

                        }
                    }
                    else
                    {
                        Console.WriteLine("Status Response: {0}", response.StatusCode);
                        return default(T);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                return default(T);
            }
        }
        /// <summary>
        /// Returns a collection of all the star ships and the total amount of stops required to make the distance between the planets in string format.
        /// </summary>
        /// <param name="MGLT">The distance between the planets.</param>
        /// <returns>A string, representing the distance between the planets.</returns>

        public string GetAllStarships(int MGLT)
        {
            try
            {
                //instance the object model
                JsonModel model = new JsonModel();
                this.jsonModel = new JsonModel();
                //send request to web api using the generic method SendRequest which accepts a string as endPoint for the call
                this.jsonModel = this.SendRequest<JsonModel>(this.EndPoint);

                //while the next page is not null, send the request to the web api to get the elements
                while (!string.IsNullOrEmpty(this.jsonModel.next))
                {
                    model = this.SendRequest<JsonModel>(this.jsonModel.next);
                    this.jsonModel.next = model.next;
                    //add the collection of starships to the existing list
                    this.jsonModel.results.AddRange(model.results);
                }

                StringBuilder str = new StringBuilder();

                //check if the collection of starships has almost an element
                if (this.jsonModel.results.Count() > 0)
                {
                    //set as sum the MGLT result of the first element
                    var sum = int.Parse(this.jsonModel.results[0].MGLT);

                    //add the output to string builder until it completes the condition that the sum of MGLT elements is less than the input 
                    for (int i = 0; sum < MGLT; i++)
                    {
                        if (i > 0)
                            sum += int.Parse(this.jsonModel.results[i].MGLT);
                        Console.WriteLine("{0}: {1}", this.jsonModel.results[i].name, this.jsonModel.results[i].MGLT);
                        str.AppendLine(string.Format("{0}: {1}", this.jsonModel.results[i].name, this.jsonModel.results[i].MGLT));
                    }
                }
                //return the result
                return str.ToString();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                return string.Empty;
            }
        }
    }
}
