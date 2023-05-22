using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleAPI.QueryModels;
using ConsoleAPI.SessionModels;
using Newtonsoft.Json;

namespace ConsoleAPI
{
    static class Program
    {
        static readonly string BaseAddress = "https://api-test.onloupe.com/";     // your Loupe URL if self hosted
        static readonly string UserName = "LoupeUserName";
        static readonly string Password = "LoupeUserPassword";
        static readonly string Tenant = "eSymmetrix";

        private static HttpClient client;

        static async Task Main(string[] args)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);

                await Login();
                //await SetTenant();
                await GetSessions();

                Console.ReadLine();
            }


            static async Task Login()
            {
                var authTokenString = UserName + ":" + Password;
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(authTokenString));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + authToken);

                Console.Write("Logging in ...");

                using (var loginResponse = await client.GetAsync("api/auth/token"))
                {
                    loginResponse.EnsureSuccessStatusCode();

                    var responseString = await loginResponse.Content.ReadAsStringAsync();
                    LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(responseString);

                    var accessToken = response.access_token;

                    // remove existing authorization and replace with new access token
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authorization", "Session " + accessToken);

                    Console.WriteLine("done");
                }
            }

            static async Task GetSessions()
            {
                Console.WriteLine("Getting sessions for yesterday");

                var queryModel = GetQuery();
                var query = JsonConvert.SerializeObject(queryModel);
                var content = new StringContent(query, Encoding.UTF8, "application/json");

                // for a single-tenant system, use "api/Sessions/SessionsFiltered"
                using (var loginResponse = await client.PostAsync($"Customers/{Tenant}/api/Sessions/SessionsFiltered", content))
                {
                    loginResponse.EnsureSuccessStatusCode();

                    var responseString = await loginResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GridResult<SessionSummaryModel>>(responseString);

                    var data = response.Data;

                    Console.WriteLine("Sessions");
                    Console.WriteLine("Application\tStart\tEnd\tUser");

                    foreach (var session in data)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                            session.Application.Badge.Title,
                            session.StartDateTime, session.EndDateTime,
                            session.UserName);
                    }

                    Console.WriteLine("Done");
                }
            }

            static SessionQueryModel GetQuery()
            {
                var now = DateTime.Now;
                var yesterday = now.AddDays(-1);
                var yesterdayStart = yesterday.Date;
                var yesterdayEnd = yesterday.Date.AddDays(1).AddTicks(-1);

                return new SessionQueryModel
                {
                    Query = new Query
                    {
                        Filters = new Filter[]
                        {
                            new Filter
                            {
                                Column = "StartDateTime",
                                Operator = Operator.Between,
                                Value1 = yesterdayStart,
                                Value2 = yesterdayEnd
                            },
                            new Filter
                            {
                                Column = "EndDateTime",
                                Operator = Operator.Between,
                                Value1 = yesterdayStart,
                                Value2 = yesterdayEnd
                            },
                        }
                    },
                    Skip = 0,
                    Take = 10
                };
            }
        }


    }
}
