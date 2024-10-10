using System;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace HospitalSystem
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Disease { get; set; }

        private DataAccess linker = new DataAccess();



        public void InsertPatient(Patient patient)
        {

            string query =
$"Insert into Patients(Name,Email,Disease) values( @n,@em, @d)";
            SqlParameter parameter1 = new SqlParameter("n", patient.Name);
            SqlParameter parameter2 = new SqlParameter("em", patient.Email);
            SqlParameter parameter3 = new SqlParameter("d", patient.Disease);
            linker.ExecuteQuery(query, false, parameter1, parameter2, parameter3);

        }

        public List<Patient> GetAllPatientsFromDatabase()
        {

            List<Patient> patients = new List<Patient>();
            string query ="Select PatientId, Name, Email, Disease from Patients";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            if (reader.HasRows == false)
            {
                return null;
            }
            while (reader.Read())
            {
                Patient current = new Patient
                {
                    PatientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Disease = reader.GetString(3)

                };
                patients.Add(current);

            }

            return patients;

        }

        public void UpdatePatientInDatabase(Patient patient)
        {
            string query =
    "Update Patients Set Name = @n, Email = @em, Disease = @d where PatientId = @id";
            SqlParameter p1 = new SqlParameter("n", patient.Name);
            SqlParameter p2 = new SqlParameter("em", patient.Email);
            SqlParameter p3 = new SqlParameter("d", patient.Disease);
            SqlParameter p4 = new SqlParameter("id", patient.PatientId);
            linker.ExecuteQuery(query, false, p1, p2, p3, p4);

        }

        private Patient GetObject(int id)
        {
            List<Patient> Patients = GetAllPatientsFromDatabase();
            string query =
                "Select PatientID from Patients";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            Patient obj = new Patient();
            int i = 0;
            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                if (currentId == id)
                {
                    obj = Patients[i];
                    break;
                }
                i++;
            }
            return obj;
        }

        public bool IsIDExists(int id)
        {
            List<Patient> Patients = new List<Patient>();
            if (GetAllPatientsFromDatabase() != null)
            {
                Patients = GetAllPatientsFromDatabase();
            }
            if (Patients.Count < 1)
            {
                return false;
            }
            string query =
                "Select PatientID from Patients";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                if (currentId == id)
                {

                    return true;
                }
            }

            return false;
        }
        public void DeletePatientFromDatabase(int id)
        {

            History history = new History();
            history.SavePatientsToJson(GetObject(id));
            string query = $"Delete From Patients where PatientID = @id";
            SqlParameter p = new SqlParameter("id", id);
            linker.ExecuteQuery(query, false, p);
        }

        public List<Patient> SearchPatientByName(string name)
        {
            List<Patient> filteredData = new List<Patient>();
            string query = $"Select * from Patients where Name = @n";
            SqlParameter p = new SqlParameter("n", name);
            SqlDataReader reader = linker.ExecuteQuery(query, true, p);
            while (reader.Read())
            {
                Patient newP = new Patient
                {
                    PatientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Disease = reader.GetString(3)
                };
                filteredData.Add(newP);

            }
            return filteredData;
        }



    }
}
