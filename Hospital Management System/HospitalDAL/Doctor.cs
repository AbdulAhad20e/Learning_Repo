using System;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace HospitalSystem
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        private DataAccess linker = new DataAccess();

    
        public void InsertDoctor(Doctor doctor)
        {

            string query =
$"Insert into Doctors(Name,Specialization) values( @n, @s)";
            SqlParameter p1 = new SqlParameter("n", doctor.Name);
            SqlParameter p2 = new SqlParameter("s", doctor.Specialization);
            linker.ExecuteQuery(query, false, p1, p2);


        }

        public List<Doctor> GetAllDoctorsFromDatabase()
        {
            List<Doctor> Doctors = new List<Doctor>();
            string query =
                "Select DoctorId,Name,Specialization from Doctors";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            if(reader.HasRows == false)
            {
                return null;
            }
            while (reader.Read())
            {
                Doctor current = new Doctor
                {
                    DoctorId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Specialization = reader.GetString(2)

                };
                Doctors.Add(current);

            }
            return Doctors;

        }

        public void UpdateDoctorInDatabase(Doctor doctor)
        {
           
                string query =
    $"Update Doctors Set Name = @n, Specialization = @s where doctorId = @id";
                SqlParameter p1 = new SqlParameter("n", doctor.Name);
                SqlParameter p2 = new SqlParameter("s", doctor.Specialization);
                SqlParameter p3 = new SqlParameter("id", doctor.DoctorId);
                linker.ExecuteQuery(query, false, p1, p2, p3);

        }

        private Doctor GetObject(int id)
        {
            List<Doctor> Doctors = GetAllDoctorsFromDatabase();
            string query =
                "Select doctorID from Doctors";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            Doctor obj = new Doctor();
            int i = 0;
            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                if (currentId == id)
                {
                    obj = Doctors[i];
                    break;
                }
                i++;
            }
            return obj;
        }

        public bool IsIDExists(int id)
        {
            List<Doctor> Doctors = GetAllDoctorsFromDatabase();
            string query =
                "Select doctorID from Doctors";
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
        public void DeleteDoctorFromDatabase(int id)
        {

            if (IsIDExists(id))
            {
                History history = new History();
                history.SaveDoctorsToJson(GetObject(id));
                string query = $"Delete From Doctors where doctorID = @id";
                SqlParameter p = new SqlParameter("id", id);
                linker.ExecuteQuery(query, false, p);
            }

        }


        public List<Doctor> SearchDoctorBySpecs(string specs)
        {
            List<Doctor> filteredData = new List<Doctor>();
            string query = $"Select DoctorID,Name,Specialization from Doctors where Specialization = @s";
            SqlParameter p = new SqlParameter("s", specs);
           
            SqlDataReader reader = linker.ExecuteQuery(query, true, p);

            while (reader.Read())
            {
                Doctor newP = new Doctor
                {
                    DoctorId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Specialization = reader.GetString(2)
                };
                
                filteredData.Add(newP);

            }
            return filteredData;
        }



    }
}
