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

            // var input = new HelloRequest { Name = "Luis" };
            // var client = new Greeter.GreeterClient(channel);

            var input = new PatientLookupModel {  PatientId = 1};
            var client = new Patient.PatientClient(channel);


            var patient = await client.GetPatientInfoAsync(input);

            Console.WriteLine($"Patient { patient.FirstName } { patient.LastName }. Chart ID { patient.ChartId }");
            
            Console.ReadLine();


        }
    }
}
