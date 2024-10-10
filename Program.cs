using System;

namespace HospitalSystemConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hospital Management System!");
            
            int input = 0;

            do
            {
                Console.Write('\n');
                Console.WriteLine("1-Add a Patient");
                Console.WriteLine("2-Update a Patient");
                Console.WriteLine("3-Delete a Patient");
                Console.WriteLine("4-Search a Patient by name");
                Console.WriteLine("5-View all Patients");
                Console.WriteLine("6-Add a Doctor");
                Console.WriteLine("7-Update a Doctor");
                Console.WriteLine("8-Delete a Doctor");
                Console.WriteLine("9-Search a Doctor by Specialization");
                Console.WriteLine("10-View all Doctors");
                Console.WriteLine("11-Book an Appointment");
                Console.WriteLine("12-View all Appointments");
                Console.WriteLine("13-Search Appointement by doctor or patient");
                Console.WriteLine("14-Cancel an Appointment");
                Console.WriteLine("15-View History of Deleted records");
                Console.WriteLine("16-Exit Application");
                Console.Write('\n');

                Menu menu = new Menu();
                input = int.Parse(Console.ReadLine());
                switch (input)
                {

                    case 1:
                        menu.AddPatient();
                        break;
                    case 2:
                        menu.UpdatePatient();
                        break;
                    case 3:
                        menu.DeletePatient();
                        break;
                    case 4:
                        menu.SearchByName();
                        break;
                    case 5:
                        menu.DisplayAllPatients();
                        break;
                    case 6:
                        menu.AddDoctor();
                        break;
                    case 7:
                        menu.UpdateDoctor();
                        break;
                    case 8:
                        menu.DeleteDoctor();
                        break;
                    case 9:
                        menu.SearchBySpecs();
                        break;
                    case 10:
                        menu.DisplayAllDoctors();
                        break;
                    case 11:
                        menu.BookAppointment();
                        break;
                    case 12:
                        menu.DisplayAllAppointments();
                        break;
                    case 13:
                        menu.SearchAppointment();
                        break;
                    case 14:
                        menu.CancelAppointment();
                        break;
                    case 15:
                        menu.DisplayDeletedRecord();
                        break;
                }
            }
            while (input != 16);
        }
    }
}
