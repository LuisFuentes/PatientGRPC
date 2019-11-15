using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PatientGrpcServer;
using PatientGrpcServer.Data;
using Microsoft.EntityFrameworkCore;

namespace PatientGrpcServer.Services
{
    public class PatientService : Patient.PatientBase
    {
        private readonly DataContext _context;

        public PatientService(DataContext context)
        {
            _context = context;
        }

        public override async Task<PatientModel> GetPatientInfo(
            PatientLookupModel request,
            ServerCallContext context)
        {

            // Function is called when the GRPC server is requested a patient's information
            // with the request containing a patient ID to search for.

            // Query for the patient by ID
            Models.Patient patient = await _context.Patient.FirstOrDefaultAsync(x => x.PatientId == request.PatientId);

            return await Task.FromResult(
                new PatientModel
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    ChartId = patient.ChartId,
                    FacilityId = patient.FacilityId,
                    EpisodeId = patient.EpisodeId
                }
            );
        }

        public override async Task GetAllPatientsInfo(
            AllPatientsRequest request,
            IServerStreamWriter<PatientModel> responseStream,
            ServerCallContext context)
        {
            // Function fetches all patients on the database and streams the data back
            // to the RPC client

            // Fetch for all the patients
            List<Models.Patient> patients = await _context.Patient.ToListAsync();

            foreach (Models.Patient patient in patients)
            {
                // Write to patient data to the stream
                await responseStream.WriteAsync(
                    new PatientModel
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        ChartId = patient.ChartId,
                        FacilityId = patient.FacilityId,
                        EpisodeId = patient.EpisodeId
                    }
                );
            }


        }
    }
}
