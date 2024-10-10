using System;
using System.Collections.Generic;
using System.Text.Json;
using HospitalSystem;
using System.IO;

namespace HospitalSystemConsoleApp
{
    class Menu
    {
        Validation validator;
        DataAccess access;

        public Menu()
        {
            validator = new Validation();
            access = new DataAccess();
        }

        public void AddPatient()
        {
            Console.WriteLine("Provide Patient Name");
            string name = Console.ReadLine();
            Console.WriteLine("Provide Patient Email");
            string email = Console.ReadLine();
            Console.WriteLine("Provide Patient Disease");
            string disease = Console.ReadLine();
            Patient patient = new Patient
            {
                Name = name,
                Email = email,
                Disease = disease
            };
            try
            {
                if (validator.IsInputValid(patient, true))
                {
                    patient.InsertPatient(patient);
                    Console.WriteLine("\nRecord Successfully Added!\n");
                  
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }
        }
        public void AddDoctor()
        {
            Console.WriteLine("Provide Doctor Name");
            string name = Console.ReadLine();
            Console.WriteLine("Provide Doctor Specialization");
            string specialization = Console.ReadLine();
            Doctor doctor = new Doctor
            {
                Name = name,
                Specialization = specialization
            };
            try
            {
                if (validator.IsInputValid(doctor, true))
                {
                    doctor.InsertDoctor(doctor);
                    Console.WriteLine("\n Doctor Successfully Added\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }
        }


        public void BookAppointment()
        {
            Console.WriteLine("Provide Doctor ID");
            int doctorId = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide Patient ID");
            int patientId = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide Appointment Date");
            string dateData = Console.ReadLine();
            Appointment appointment = new Appointment
            {
                DoctorID = doctorId,
                PatientId = patientId,
                AppointmentDate = DateTime.Parse(dateData).Date
            };
            try
            {
                if (validator.IsInputValid(appointment, true))
                {
                    appointment.InsertAppointment(appointment);
                    Console.WriteLine("\nAppointment Successfully Booked\n");

                }
                else
                {
                    Console.WriteLine("there is a problem in input or Patient or Doctor Id not exist, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error: " + e);
            }
        }
        public void UpdatePatient()
        {
            Console.WriteLine("Provide Patient ID");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide Patient Name");
            string name = Console.ReadLine();
            Console.WriteLine("Provide Patient Email");
            string email = Console.ReadLine();
            Console.WriteLine("Provide Patient Disease");
            string disease = Console.ReadLine();
            Patient patient = new Patient
            {
                PatientId = id,
                Name = name,
                Email = email,
                Disease = disease
            };
            try
            {
                if (validator.IsInputValid(patient))
                {
                    patient.UpdatePatientInDatabase(patient);
                    Console.WriteLine("\nRecord Successfully Updated\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }
        }

        public void UpdateDoctor()
        {
            Console.WriteLine("Provide Doctor ID");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide Doctor Name");
            string name = Console.ReadLine();
            Console.WriteLine("Provide Doctor Specialization");
            string specs = Console.ReadLine();


            Doctor doctor = new Doctor
            {
                DoctorId = id,
                Name = name,
                Specialization = specs
            };
            try
            {
                if (validator.IsInputValid(doctor))
                {
                    doctor.UpdateDoctorInDatabase(doctor);
                    Console.WriteLine("\nRecord Successfully Updated\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }
        }

        public void DeletePatient()
        {
            Console.WriteLine("Provide Patient ID");
            int id = int.Parse(Console.ReadLine());
            Patient patient = new Patient
            {
                PatientId = id
            };
            try
            {
                if (validator.IsInputValid(patient, false, id))
                {
                    patient.DeletePatientFromDatabase(patient.PatientId);
                    Console.WriteLine("\nPatient Successufully Deleted!!\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }

        }

        public void DeleteDoctor()
        {
            Console.WriteLine("Provide Doctor ID");
            int id = int.Parse(Console.ReadLine());
            Doctor doctor = new Doctor
            {
                DoctorId = id
            };
            try
            {
                if (validator.IsInputValid(doctor, false, id))
                {
                    doctor.DeleteDoctorFromDatabase(doctor.DoctorId);
                    Console.WriteLine("\nDoctor Successfully Deleted\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }

        }

        public void CancelAppointment()
        {
            Console.WriteLine("Provide Appointment ID");
            int id = int.Parse(Console.ReadLine());
            Appointment appointment = new Appointment
            {
                AppointmentId = id
            };
            try
            {
                if (validator.IsInputValid(appointment))
                {
                    appointment.DeleteAppointmentFromDatabase(appointment.AppointmentId);
                    Console.WriteLine("\nAppointment Successfully Cancelled\n");
                }
                else
                {
                    Console.WriteLine("there is a problem in input, Try again!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program terminated with error :" + e);
            }

        }
        public void SearchByName()
        {
            Console.WriteLine("Please Enter the name");
            string name = Console.ReadLine();
            Patient patient = new Patient();
            try
            {
                if (validator.IsInputValid(patient))
                {
                    List<Patient> patients = patient.SearchPatientByName(name);
                    if(patients.Count == 0)
                    {
                        Console.WriteLine("\nNo Data Found\n");
                    }
                    if (patients.Count > 0)
                    {
                        for (int i = 0; i < patients.Count; i++)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(patients[i]));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Problem with input");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program Terminated with error :" + e);
            }

        }

        public void SearchBySpecs()
        {
            Console.WriteLine("Please Enter the specialization");
            string name = Console.ReadLine();
            Doctor doctor = new Doctor();
            List<Doctor> doctors = new List<Doctor>();

            try
            {
                if (validator.IsInputValid(doctor))
                {
                       doctors =  doctor.SearchDoctorBySpecs(name);
                    if (doctors.Count > 0)
                    {
                        for (int i = 0; i < doctors.Count; i++)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(doctors[i]));
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n No Data Found\n");
                    }
                }
                else
                {
                    Console.WriteLine("Problem with input");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program Terminated with error :" + e);
            }

        }

        public void SearchAppointment()
        {
            Console.WriteLine("Please Enter the Doctor Id or Patient Id");
            int currentId = int.Parse(Console.ReadLine());
            Appointment appointment = new Appointment();
            List<Appointment> apps = appointment.GetObjectByForeignKey(currentId);
            if (apps.Count > 0)
            {
                for (int i = 0; i < apps.Count; i++)
                {
                    Console.WriteLine(JsonSerializer.Serialize(apps[i]));
                }
            }
            else
            {
                Console.WriteLine("No Data Found");
            }
        }

        public void DisplayAllPatients()
        {
            try
            {
                Patient patient = new Patient();
                List<Patient> patients = patient.GetAllPatientsFromDatabase();
                if (patients.Count > 0)
                {
                    for (int i = 0; i < patients.Count; i++)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(patients[i]));
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program Terminated with error :" + e);
            }
        }


        public void DisplayAllDoctors()
        {
            try
            {
                Doctor doctor = new Doctor();
                List<Doctor> doctors = doctor.GetAllDoctorsFromDatabase();
                if (doctors !=null)
                {
                    if (doctors.Count > 0)
                    {
                        for (int i = 0; i < doctors.Count; i++)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(doctors[i]));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program Terminated with error :" + e);
            }
        }

        public void DisplayAllAppointments()
        {
            try
            {
                Appointment app = new Appointment();
                List<Appointment> apps = app.GetAllAppointmentsFromDatabase();
                if (apps.Count > 0)
                {
                    for (int i = 0; i < apps.Count; i++)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(apps[i]));
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Program Terminated with error :" + e);

            }
        }

        public void DisplayDeletedRecord()
        {
            StreamReader reader = new StreamReader("DeletedPatients.txt");
            string currentLine = reader.ReadLine();
            if (currentLine != null)
            {
                Console.WriteLine("Deleted Patients are :");
                Console.WriteLine(currentLine);
                while (currentLine != null)
                {
                    Console.WriteLine(currentLine);
                    currentLine = reader.ReadLine();
                }
            }
            reader.Close();
            reader = new StreamReader("DeletedDoctors.txt");
            currentLine = reader.ReadLine();
            if (currentLine != null)
            {
                Console.WriteLine("Deleted Doctors are :");
                Console.WriteLine(currentLine);
                while (currentLine != null)
                {
                    Console.WriteLine(currentLine);
                    currentLine = reader.ReadLine();
                }
            }

            reader.Close();
            reader = new StreamReader("DeletedAppointments.txt");
            currentLine = reader.ReadLine();
            if (currentLine != null)
            {
                Console.WriteLine("Deleted Appointments are :");
                Console.WriteLine(currentLine);
                while (currentLine != null)
                {
                    Console.WriteLine(currentLine);
                    currentLine = reader.ReadLine();
                }
            }
            reader.Close();

        }

    }
}
