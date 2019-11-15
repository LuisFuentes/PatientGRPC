using Grpc.Core;
using Grpc.Net.Client;
using PatientGrpcServer;
using System;
using System.Threading.Tasks;

namespace PatientGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Client used to communicate with Patient GRPC Server

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            // Fetch patient ID #1
            var input = new PatientLookupModel {  PatientId = 1};
            var client = new Patient.PatientClient(channel);

            var patient = await client.GetPatientInfoAsync(input);
            Console.WriteLine($"Patient { patient.FirstName } { patient.LastName }. Chart ID { patient.ChartId }");

            // Fetch all patients on database
            Console.WriteLine("All patients: ");
            
            using (var patients = client.GetAllPatientsInfo(new AllPatientsRequest()))
            {
                while (await patients.ResponseStream.MoveNext())
                {
                    var pt = patients.ResponseStream.Current;
                    Console.WriteLine($"Patient { pt.FirstName } { pt.LastName }. Chart ID { pt.ChartId }");


                }
            }
            
            Console.ReadLine();
        }
    }
}
