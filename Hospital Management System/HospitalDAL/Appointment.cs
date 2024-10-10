using System;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace HospitalSystem
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int DoctorID { get; set; }

        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }

        private DataAccess linker = new DataAccess();



        public void InsertAppointment(Appointment Appointment)
        {
            string query =
$"Insert into Appointments(DoctorID,PatientId, AppointmentDate) values( @a,@b,@d)";
            SqlParameter p1 = new SqlParameter("a", Appointment.DoctorID);
            SqlParameter p2 = new SqlParameter("b", Appointment.PatientId);
            SqlParameter p3 = new SqlParameter("d", Appointment.AppointmentDate.Date);
            linker.ExecuteQuery(query, false, p1, p2, p3);
        }


        public List<Appointment> GetAllAppointmentsFromDatabase()
        {
            List<Appointment> appointments = new List<Appointment>();
            string query =
                "Select AppointmentId,DoctorID,PatientID,AppointmentDate from Appointments";
            SqlDataReader reader = linker.ExecuteQuery(query, true);

            while (reader.Read())
            {
                Appointment current = new Appointment
                {
                    AppointmentId = reader.GetInt32(0),
                    DoctorID = reader.GetInt32(1),
                    PatientId = reader.GetInt32(2),
                    AppointmentDate = reader.GetDateTime(3).Date

                };
                appointments.Add(current);

            }
            return appointments;

        }

        public void UpdateAppointmentInDatabase(Appointment appointment)
        {

            string query =
$"Update Appointments Set DoctorId = @a, PatientID = @b, AppointmentDate = @d where AppointmentId = @id";
            SqlParameter p1 = new SqlParameter("a", appointment.DoctorID);
            SqlParameter p2 = new SqlParameter("b", appointment.PatientId);
            SqlParameter p3 = new SqlParameter("d", appointment.AppointmentDate.Date);
            SqlParameter p4 = new SqlParameter("id", appointment.AppointmentId);
            linker.ExecuteQuery(query, false, p1, p2, p3, p4);


        }
        private Appointment GetObject(int id)
        {
            List<Appointment> appointments = GetAllAppointmentsFromDatabase();
            string query =
                "Select AppointmentID from Appointments";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            Appointment obj = new Appointment();
            int i = 0;
            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                if (currentId == id)
                {
                    obj = appointments[i];
                    break;
                }
                i++;
            }
            return obj;
        }

        public List<Appointment> GetObjectByForeignKey(int id)
        {
            List<Appointment> Appointments = GetAllAppointmentsFromDatabase();

            string query = $"Select * from Appointments where DoctorID = @id OR PatientID = @id";

            SqlParameter p = new SqlParameter("id", id);
            SqlDataReader reader = linker.ExecuteQuery(query, true, p);
            List<Appointment> filteredAppointments = new List<Appointment>();
            Appointment obj = new Appointment();
            while (reader.Read())
            {
                Appointment temp = new Appointment();
                temp.AppointmentId = reader.GetInt32(0);
                temp.DoctorID = reader.GetInt32(1);
                temp.PatientId = reader.GetInt32(2);
                temp.AppointmentDate = reader.GetDateTime(3);
                filteredAppointments.Add(temp);
            }

            return filteredAppointments;
        }

        public bool IsIDExists(int id)
        {
            List<Appointment> Appointments = GetAllAppointmentsFromDatabase();
            string query =
                "Select AppointmentID from Appointments";
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

        public bool IsAppointmentDateExists(DateTime date)
        {
            string query =
              "Select AppointmentDate from Appointments";
            SqlDataReader reader = linker.ExecuteQuery(query, true);
            while (reader.Read())
            {
                DateTime currentId = reader.GetDateTime(0);
                if (currentId == date)
                {
                    return true;
                }
            }
            return false;
        }
        public void DeleteAppointmentFromDatabase(int id)
        {


            string query = $"Delete From Appointments where AppointmentID = @id";
            SqlParameter parameter = new SqlParameter("id", id);
            linker.ExecuteQuery(query, false, parameter);
            History history = new History();
            history.SaveAppointmentsToJson(GetObject(id));


        }




    }
}
