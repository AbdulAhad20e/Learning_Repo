using System;
using HospitalSystem;

namespace HospitalSystemConsoleApp
{
    class Validation
    {

        public bool IsInputValid(Appointment appointment, bool isWriting = false, int id = -1)
        {
            if (isWriting)
            {
                Doctor tempDoc = new Doctor();
                Patient tempPat = new Patient();
                if (!appointment.IsIDExists(appointment.AppointmentId)
                    && appointment.AppointmentDate.Date > DateTime.Now.Date
                    && appointment.DoctorID > 0
                    && appointment.PatientId > 0
                    && !appointment.IsAppointmentDateExists(appointment.AppointmentDate)
                    && tempDoc.IsIDExists(appointment.DoctorID)
                    && tempPat.IsIDExists(appointment.PatientId))
                {
                    return true;
                }
            }
            else
            {
                if (id != -1)
                {
                    if (appointment.IsIDExists(id))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;

                }
            }
            return false;
        }

        public bool IsInputValid(Patient patient, bool isWriting = false, int id = -1)
        {
            if (isWriting)
            {
                if (patient.Email.Contains("@") && patient.Email.Contains(".") && patient.Name != null)
                {
                    return true;
                }
            }
            else
            {
                if (id != -1)
                {
                    if (patient.IsIDExists(id))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;

                }
            }
            return false;
        }

        public bool IsInputValid(Doctor doctor, bool isWriting = false, int id = -1)
        {
            if (isWriting)
            {
                if (!doctor.IsIDExists(doctor.DoctorId) && doctor.Name != null && doctor.Specialization != null)
                {
                    return true;
                }
            }
            else
            {
                if (id != -1)
                {
                    if (doctor.IsIDExists(id))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;

                }
            }
            return false;
        }


    }
}
