using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HubooTechnical
{
    internal class Program
    {

        private static async Task ProcessRequest(string vehicleReg)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json+v6"));
            client.DefaultRequestHeaders.Add("x-api-key", "HybH0yr4Hj3eEgybT9pkn6B7PA769YDa8kt4wKdp");


            var response = await client.GetAsync("https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=" + vehicleReg);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Vehicle registration not found");
                }
                else
                {
                    throw new Exception("An unexpected error occurred");
                }
            }
            var motHistory = JsonSerializer.Deserialize<List<MotHistory>>(response.Content.ReadAsStream());

            foreach (var result in motHistory)
            {
                result.OutputVehicleDetails();
                result.OutputMotExpiry();
                result.OutputNumberOfFailures();
            }
        }

        static async Task Main(string[] args)
        {
            string vehicleReg = "";
            while (true)
            {
                Console.WriteLine("Enter reg no: ");
                vehicleReg = Console.ReadLine();
                vehicleReg = vehicleReg.Replace(" ", "");

                if (vehicleReg.ToLower() == "exit")
                {
                    break;
                }

                Console.WriteLine("Reg entered is " + vehicleReg);

                try
                {
                    if (vehicleReg != "")
                    {
                        await ProcessRequest(vehicleReg);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Thanks for playing");
        }
    }
}