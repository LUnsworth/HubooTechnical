using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HubooTechnical
{
    public class MotResults
    {
        public string? completedDate { get; set; }
        public string? testResult { get; set; }
        public string? expiryDate { get; set; }
        public string? odometerValue { get; set; }
        public string? odometerUnit { get; set; }
        public string? odometerResultType { get; set; }
        public string? motTestNumber { get; set; }
        //rfrAndComments ommitted as out of scope
    }
    public class MotHistory
    {
        public string? registration { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public string? firstUsedDate { get; set; }
        public string? fuelType { get; set; }
        public string? primaryColour { get; set; }
        public string? vehicleId { get; set; }
        public string? registrationDate { get; set; }
        public string? manufactureDate { get; set; }
        public string? engineSize { get; set; }
        public List<MotResults> motTests { get; set; }

        public void OutputVehicleDetails()
        {
            Console.WriteLine("Make: " + make);
            Console.WriteLine("Model: " + model);
            Console.WriteLine("Colour: " + primaryColour);
        }
        
        public void OutputTestResults()
        {
            foreach (var results in motTests)
            {
                Console.WriteLine(results.completedDate);
                Console.WriteLine(results.testResult);
                Console.WriteLine(results.expiryDate);
                Console.WriteLine(results.odometerValue);
                Console.WriteLine(results.odometerUnit);
                Console.WriteLine(results.odometerResultType);
                Console.WriteLine(results.motTestNumber);
                Console.WriteLine("");
            }
        }
        
        public void OutputMotExpiry()
        {
            if (motTests != null)
            {
                //DateTime expiry = new DateTime();
                if (motTests[0].testResult == "PASSED")
                {
                    Console.WriteLine("MOT Expires on: " + motTests[0].expiryDate);
                    //expiry = TryParse(motTests[0].expiryDate);
                }
                else
                {
                    foreach (var result in motTests)
                    {
                        if (result.testResult == "PASSED")
                        {
                            Console.WriteLine("MOT is expired. MOT expired on " + result.expiryDate);
                            break;
                        }    
                    }
                }
            }
        }

        public void OutputNumberOfFailures()
        {
            int failures = 0;
            foreach (var result in motTests)
            {
                if (result.testResult != "PASSED")
                {
                    failures++;
                }
            }
            Console.WriteLine("Number of failures: " + failures.ToString());
        }
    }
}
