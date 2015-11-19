using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Secretariat
{
    public static class Globals
    {
        public static String Tabela1 = " Secretariat";
        public static String Tabela2 = " Secretariatintermediere";
        public static String selectALL = "Select * from";
        public static String selectALLFromSecretariat = "Select * from Secretariat";
        public static String selectALLFromSecretariatintermediere = "Select * from Secretariatintermediere";
        public static String selectALLFromSecretariatintermedierebyFacultate = "Select * from Secretariatintermediere where Facultate=@Facultate";
        public static String selectALLFromSecretariatbyStudent = "Select * from Secretariat where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";
        public static String addINTOSecretariat = "INSERT INTO Secretariat(Facultate,Nume_Student,Prenume_Student,Materie,Nota) VALUES (@Facultate,@Nume_Student,@Prenume_Student,@Materie,@Nota)";
        public static String addINTOUniversitateForProf = "INSERT INTO Secretariatintermediere(Nume_Student,Prenume_Student,Facultate,Medie,Bursa) VALUES (@Nume_Student,@Prenume_Student,@Facultate,@Medie,@Bursa)";
        public static String updateSecretariatbyProfesor = "UPDATE Secretariat SET Facultate=@Facultate,Nume_Student=@Nume_Student,Prenume_Student=@Prenume_Student,Materie=@Materie,Nota=@Nota where Profesor=@Profesor";
        public static String updateSecretariatbyMaterie = "UPDATE Secretariat SET Facultate=@Facultate,Nume_Student=@Nume_Student,Prenume_Student=@Prenume_Student,Materie=@Materie,Nota=@Nota where Materie=@Materie";
        public static String updateSecretariatbyFacultate = "UPDATE Secretariat SET Facultate=@Facultate,Nume_Student=@Nume_Student,Prenume_Student=@Prenume_Student,Materie=@Materie,Nota=@Nota where Facultate=@Facultate";
        public static String updateSecretariatintermediereMediebyStudentsiFac = "UPDATE UniversitateIntermediere SET Medie=@Medie where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student and Facultate=@Facultate";
        public static String updateSecretariatintermediereBursabyStudentsiFac = "UPDATE UniversitateIntermediere SET Bursa=@Bursa where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student and Facultate=@Facultate";
        public static String deleteFromSecretariatbyProfesor = "Delete from Secretariat where Profesor=@Profesor";
        public static String deleteFromSecretariatbyCurs = "Delete from Secretariat where Materie=@Materie";
        public static String deleteFromSecretariatbyStudent = "Delete from Secretariat where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";
        public static String deleteFromSecretariatintermedierebyStudent = "Delete from Secretariatintermediere where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";
        
    }
    public interface IAddDataInDB
    {
        void AddDB(string comanda);
    }
    public interface IDeleteDataInDB
    {
        void DeleteDB(string comanda, string condition);
    }
    public interface IDeleteDataInDB2Condition
    {
        void DeleteDB(string comanda, string condition, string condition2);
    }
    public interface IUpdateDataInDB1Condition
    {
        void UpdateDB(string comanda, string condition);
    }
    public interface IUpdateDataInDB2Condition
    {
        void UpdateDB(string comanda, string condition, string condition2);
    }
    public interface IViewDataFromDB
    {
        void ViewDB(string comanda);
    }
    public interface IFindDataFromDB1Condition
    {
        void FindDB(string comanda, string condition);
    }
    public interface IFindDataFromDB2Condotion
    {
        void FindDB(string comanda, string condition, string condition2);
    }
    public interface IRequestFromUniversitateIntermediere
    {
        void requestStudentifromProfesor(string comanda, string condition, string condition2);
        //copiere in tabela Secretariat toate notele corespunzatoare fiecarui student din tabela Universitateintermediere
    }
    public interface IAddStudentsInSecretariatIntermediereByFacultate
    {
        void AddstudentipentrCalcurareBurse(string comanda, string condition, string condition2);
        //copiere in tabela Secretariat toate notele corespunzatoare fiecarui student din tabela Universitateintermediere
    }
    public class ConnectDB
    {
        protected OleDbConnection connection;
        private string provider = "Microsoft.ACE.OLEDB.12.0";
        private string location = "E:\\schoolWork\\PSSC\\Database1.accdb";
        public ConnectDB()
        {
            try
            {
                connection = new OleDbConnection("Provider=" + provider + ";Data Source=" + location);
            }
            catch (Exception DBConnection)
            {
                throw new ApplicationException("Connection failled :", DBConnection);
            }

        }
    }
    
    public abstract class User : ConnectDB
    {
        protected string password { get; set; }
        protected string userId { get; set; }
        abstract public bool login();
    }
    public class Secretariat : User,
                                IViewDataFromDB,
                                IFindDataFromDB1Condition,
                                IFindDataFromDB2Condotion,
                                IUpdateDataInDB1Condition,
                                IUpdateDataInDB2Condition,
                                IDeleteDataInDB,
                                IDeleteDataInDB2Condition,
                                IRequestFromUniversitateIntermediere,
                                IAddStudentsInSecretariatIntermediereByFacultate,
                                IAddDataInDB
    {
        private string Nume_Student { get; set; }
        private string Prenume_Student { get; set; }
        private string Facultate { get; set; }
        private string Materie { get; set; }
        private string Bursa { get; set; }
        private int Nota { get; set; }
        private double Medie { get; set; }
        public Secretariat()
        {
            login();
        }
        public override bool login()
        {
            return true;
        }
        public void AddstudentipentrCalcurareBurse(string comanda, string condition, string condition2)
        {

        }
        public void requestStudentifromProfesor(string comanda, string condition, string condition2)
        {

        }
        public void FindDB(string comanda, string condition, string condition2)
        {

        }
        public void FindDB(string comanda, string condition)
        {

        }
        public void ViewDB(string comanda)
        {

        }
        public void UpdateDB(string comanda, string condition, string condition2)
        {

        }
        public void UpdateDB(string comanda, string condition)
        {

        }
        public void DeleteDB(string comanda, string condition, string condition2)
        {

        }
        public void DeleteDB(string comanda, string condition)
        {

        }
        public void AddDB(string comanda)
        {

        }
    }
    public class Student : User,
                          IViewDataFromDB,
                          IFindDataFromDB1Condition,
                          IFindDataFromDB2Condotion
    {
        public Student()
        {
            login();
        }
        public override bool login()
        {
            return true;
        }
        public void FindDB(string comand, string cond)
        {

        }
        public void FindDB(string comand, string cond1, string cond2)
        {

        }
        public void ViewDB(string comand)
        {

        }
    }
    class Program
    {
        static void studenti()
        {
            int selector = 0;
            int maxItem = 6;
            Console.WriteLine("STUDENT");
            Student student = new Student();
            if (student.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Student\n");
                    Console.WriteLine("1-Vizualizare nivel Secretariat");
                    Console.WriteLine("2-Vizualizare Nivel Secretariat intermediar");
                    Console.WriteLine("3-Find anything in Secretariat by Student ");
                    Console.WriteLine("4-Find anything in Secretariat Intermediar by Student ");
                    Console.WriteLine("5-Find anything in Secretariat by facultate ");
                    Console.WriteLine("6-Find anything in Secretariat Intermediere  by Facultate ");
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            {
                                Console.WriteLine("Vizualizare nivel Secretariat");
                                student.ViewDB(Globals.selectALLFromSecretariat);
                                break;
                            }
                        case 2: Console.WriteLine("Vizualizare Nivel studenti");
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        default: break;
                    }


                } while (selector != maxItem);
            }
            else
            {
                Console.WriteLine("Eroare la autentificare");
            }
        }
        static void secretariat()
        {
            int selector = 0;
            int maxItem = 14;
            Console.WriteLine("Secretariat");
            Secretariat secretar = new Secretariat();
            if(secretar.login()==true)
            {
                do
                {
                    Console.WriteLine("Optiuni Secretariat\n");
                    Console.WriteLine("1-Vizualizare nivel Secretariat");
                    Console.WriteLine("2-Vizualizare Nivel Secretariat Intermediar");
                    Console.WriteLine("3-Adaugare date nivel Secretariat");
                    Console.WriteLine("4-Update date nivel Secretariat by student(nume,prenume)");
                    Console.WriteLine("5-Update date nivel Secretariat by facultate");
                    Console.WriteLine("6-Delete date nivel Secretariat Intermediar by student(nume,prenume)");
                    Console.WriteLine("7-Delete date nivel Secretariat Intermediar by facultate");
                    Console.WriteLine("8-Find anything in Secretariat by Student ");
                    Console.WriteLine("9-Find anything in Secretariat Intermediar by Student ");
                    Console.WriteLine("10-Find anything in Secretariat by facultate ");
                    Console.WriteLine("11-Find anything in Secretariat Intermediere  by Facultate ");
                    Console.WriteLine("12-Request studenti from secretariat ");
                    Console.WriteLine("13-Adaugare date nivel Secretariat Intermediar");
                    Console.WriteLine("14-Exit");

                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                        case 10:
                            break;
                        case 11:
                            break;
                        case 12:
                            break;
                        case 13:
                            break;
                        case 14:
                            break;
                        default: break;
                    }
                } while (maxItem != 14);
            }
            else
            {
                Console.WriteLine("Eroare la autentificare");
            }
        }
        static void Main(string[] args)
        {
            ConnectDB db = new ConnectDB();
            int selector = 0;
            do
            {
                Console.WriteLine("Gestiune Secretariat\n");
                Console.WriteLine("1-student");
                Console.WriteLine("2-secretar");
                selector=Convert.ToInt32(Console.ReadLine());


                switch (selector)
                {
                    case 1:
                        {
                            studenti();
                            break;
                        }
                    case 2:
                        {
                            secretariat();
                            break;
                        }
                }
            } while (selector != 2);
        }
    }
}
