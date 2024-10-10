using System;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace HospitalSystem
{
    class History
    {

        public void SaveAppointmentsToJson(Appointment currentAppointment)
        {

            StreamWriter writer = new StreamWriter("DeletedAppointments.txt", append: true);
            string jsonProduct = JsonSerializer.Serialize(currentAppointment);
            DateTime currentTime = DateTime.Now;
            writer.Write(jsonProduct + " Last deleted : " + currentTime.ToString());
            writer.Close();
        }

        public void SaveDoctorsToJson(Doctor currentDoctor)
        {

            StreamWriter writer = new StreamWriter("DeletedDoctors.txt", append: true);
            string jsonProduct = JsonSerializer.Serialize(currentDoctor);
            DateTime currentTime = DateTime.Now;
            writer.Write(jsonProduct + " Last deleted : " + currentTime.ToString());
            writer.Close();
        }

        public void SavePatientsToJson(Patient currentPatient)
        {

            StreamWriter writer = new StreamWriter("DeletedPatients.txt", append: true);
            string jsonProduct = JsonSerializer.Serialize(currentPatient);
            DateTime currentTime = DateTime.Now;
            writer.Write(jsonProduct + " Last deleted : " + currentTime.ToString());
            writer.Close();
        }


    }
}
