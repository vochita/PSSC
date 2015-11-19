using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Universitate
{
    public static class Globals
    {
        public static String Tabela1= " Universitate";
        public static String Tabela2= " UniversitateIntermediere";
        public static String selectALL = "Select * from";
        public static String selectALLFromUniversitate = "Select * from Universitate";
        public static String selectALLFromUniversitateForProf = "Select * from UniversitateIntermediere";
        public static String selectALLFromUniversitateForProfbyProfesor = "Select * from UniversitateIntermediere where Profesor=@Profesor";
        public static String addINTOUniversitate = "INSERT INTO Universitate(Facultate,Materie,Profesor) VALUES (@Facultate,@Materie,@Profesor)";
        public static String addINTOUniversitateForProf = "INSERT INTO UniversitateIntermediere(Profesor,Curs,Nume_Student,Prenume_Student,Nota) VALUES (@Profesor,@Curs,@Nume_Student,@Prenume_Student,@Nota)";
        public static String updateUniversitatebyProfesor = "UPDATE Universitate SET Facultate=@Facultate,Materie=@Materie,Profesor=@Profesor where Profesor=@Profesor";
        public static String updateUniversitatebyMaterie = "UPDATE Universitate SET Facultate=@Facultate,Materie=@Materie,Profesor=@Profesor where Materie=@Materie";
        public static String updateUniversitatebyFacultate = "UPDATE Universitate SET Facultate=@Facultate,Materie=@Materie,Profesor=@Profesor where Facultate=@Facultate";
        public static String updateUniversitateForProfbyStudent = "UPDATE UniversitateIntermediere SET Nota=@Nota where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student and Profesor=@Profesor";
        public static String deleteFromUniversitatebyProfesor = "Delete from Universitate where Profesor=@Profesor";
        public static String deleteFromUniversitatebyCurs = "Delete from Universitate where Materie=@Materie";
        public static String deleteFromUniversitatebyStudent = "Delete from UniversitateIntermediere where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student and Profesor=@Profesor";
        public static String profesorCondition = " where Profesor=";
        public static String addtable2 = "INSERT INTO UniversitateIntermediere(Profesor,Curs,Nume_Student,Prenume_Student,Nota) VALUES (@Profesor,@Curs,@Nume_Student,@Prenume_Student,@Nota)";
        public static String addtable1 = "INSERT INTO Universitate(Facultate,Materie,Profesor) VALUES (@Facultate,@Materie,@Profesor)";
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
    public abstract  class User:ConnectDB
    {
        protected string password { get; set; }
        protected string userId { get; set; }
        abstract public bool login();
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
        void DeleteDB(string comanda, string condition , string condition2);
    }
    public interface IUpdateDataInDB1Condition
    {
        void UpdateDB(string comanda,string condition);
    }
    public interface IUpdateDataInDB2Condition
    {
        void UpdateDB(string comanda, string condition , string condition2);
    }
    public interface IViewDataFromDB
    {
        void ViewDB(string comanda);
    }
    public interface IFindDataFromDB1Condition
    {
        void FindDB(string comanda,string condition);
    }
    public interface IFindDataFromDB2Condotion
    {
        void FindDB(string comanda, string condition , string condition2);
    }
    public interface IRequestFromSecretariat
    {
        void requestStudentifromSecretariat(string comanda, string condition, string condition2);
        //copiere in tabela UniversitateIntermediere totii elevii inscrisi la materia profesorului de la secretariat
    }

    public class Student: User ,
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
        public void FindDB(string comand , string cond)
        {

        }
        public void FindDB(string comand , string cond1, string cond2)
        {

        }
        public void ViewDB(string comand)
        {

        }
    }
    public class Profesor : User,
                            IViewDataFromDB,
                            IAddDataInDB,
                            IUpdateDataInDB1Condition,
                            IDeleteDataInDB,
                            IDeleteDataInDB2Condition,
                            IFindDataFromDB1Condition,
                            IFindDataFromDB2Condotion,
                            IUpdateDataInDB2Condition,
                            IRequestFromSecretariat
    {
        private string Profesr { get; set; }
        private string Nume_Student { get; set; }
        private string Prenume_Student { get; set; }
        private string Curs { get; set; }
        private int Nota { get; set; }

        public Profesor(string profesor)
        {
            Profesr = profesor;
            login();
        }
        public override bool login()
        {
            return true;
        }
        public void requestStudentifromSecretariat(string comanda,string condition1,string condition2)
        {

        }
        public void DeleteDB(string comanda, string condition)
        {
            string sqlCommand = Globals.deleteFromUniversitatebyCurs;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public void DeleteDB(string comanda, string condition , string condition2)
        {
            string sqlCommand = Globals.deleteFromUniversitatebyCurs;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public void UpdateDB(string comanda, string condition)
        {
            string sqlCommand = Globals.updateUniversitateForProfbyStudent;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public void UpdateDB(string comanda, string condition,string condition2)
        {
            string sqlCommand = Globals.updateUniversitateForProfbyStudent;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public  void AddDB(string comanda)
        {

        }
        public void ViewDB(string comanda)
        {
            
        }
        public void FindDB(string comand, string cond)
        {

        }
        public void FindDB(string comand, string cond1, string cond2)
        {

        }


    }
    public class Owner : User,
                        IAddDataInDB,
                        IUpdateDataInDB1Condition,
                        IDeleteDataInDB,
                        IViewDataFromDB,
                        IFindDataFromDB1Condition,
                        IFindDataFromDB2Condotion,
                        IDeleteDataInDB2Condition,
                        IUpdateDataInDB2Condition
    {
        private string facultate { get; set; }
        private string curs { get; set; }
        private string profesor { get; set; }
        public Owner()
        {
            login();
        }
        public override bool login()
        {
            return true;
        }
        public void ViewDB(string comanda)
        {
            
        }
        public void FindDB(string comand, string cond)
        {

        }
        public void FindDB(string comand, string cond1, string cond2)
        {

        }
        public void AddDB(string comanda)
        {
        }
        public void DeleteDB(string comanda , string condition)
        {
            
        }
        public void DeleteDB(string comanda, string condition , string condition2)
        {

        }
        public void UpdateDB(string comanda , string condition)
        {

        }
        public void UpdateDB(string comanda, string condition, string condition2)
        {

        }
    }
    class Program
    {
        static void studenti ()
        {
            int selector = 0;
            int maxItem=6;
            Console.WriteLine("STUDENT");
            Student student = new Student();
            if (student.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Student\n");
                    Console.WriteLine("1-Vizualizare nivel Universitate");
                    Console.WriteLine("2-Vizualizare Nivel studenti");
                    Console.WriteLine("3-Find anythink in Universitate by prof ");
                    Console.WriteLine("4-Find anythink in Universitate by materie ");
                    Console.WriteLine("5-Find anythink in Universitate by facultate ");
                    Console.WriteLine("6-Find anythink in UniversitateIntermediere by student ");
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            {
                                Console.WriteLine("Vizualizare nivel Universitate");
                                student.ViewDB(Globals.selectALLFromUniversitate);
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
        static void profesori()
        {
            int selector = 0;
            int maxItem = 13;
            Console.WriteLine("PROFESOR");
            string profesor;
            Console.WriteLine("Introduceti numele dumneavoastra:\n");
            profesor=Console.ReadLine();
            Profesor prof = new Profesor(profesor);
            if (prof.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Profesor\n");
                    Console.WriteLine("1-Vizualizare nivel Universitate");
                    Console.WriteLine("2-Vizualizare Nivel studenti");
                    Console.WriteLine("3-Adaugare date nivel studenti");
                    Console.WriteLine("4-Update date nivel studenti by student(nume,prenume)");
                    Console.WriteLine("5-Update date nivel studenti by student(nume)");
                    Console.WriteLine("6-Delete date nivel studenti by student(nume,prenume)");
                    Console.WriteLine("7-Delete date nivel studenti by student(nume)");
                    Console.WriteLine("8-Find anythink in Universitate by prof ");
                    Console.WriteLine("9-Find anythink in Universitate by materie ");
                    Console.WriteLine("10-Find anythink in Universitate by facultate ");
                    Console.WriteLine("11-Find anythink in UniversitateIntermediere by student ");
                    Console.WriteLine("13-Request studenti from secretariat ");
                    Console.WriteLine("12-Exit");
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
                        default: break;
                    }


                } while (selector != maxItem);
            }
            else
            {
                Console.WriteLine("Eroare la autentificare");
            }
        }
        static void owners()
        {
            Console.WriteLine("OWNER");
            int selector = 0;
            int maxItem = 14;
            Owner owner = new Owner();
            if (owner.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Owner\n");
                    Console.WriteLine("1-Vizualizare nivel Universitate");
                    Console.WriteLine("2-Vizualizare Nivel studenti");
                    Console.WriteLine("3-Add Date nivel Universitate");
                    Console.WriteLine("4-Update Date nivel Universitate by prof");
                    Console.WriteLine("5-Update Date nivel Universitate by facultate");
                    Console.WriteLine("6-Update Date nivel Universitate by curs");
                    Console.WriteLine("7-Delete Date nivel Universitate by prof");
                    Console.WriteLine("8-Delete Date nivel Universitate by facultate");
                    Console.WriteLine("9-Delete Date nivel Universitate by curs");
                    Console.WriteLine("10-Find anythink in Universitate by prof ");
                    Console.WriteLine("11-Find anythink in Universitate by materie ");
                    Console.WriteLine("12-Find anythink in Universitate by facultate ");
                    Console.WriteLine("13-Find anythink in UniversitateIntermediere by student ");
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


                } while (selector != maxItem);
            }
            else
            {
                Console.WriteLine("Eroare la autentificare!");
            }
        }
     
        static void Main(string[] args)
        {
            ConnectDB db = new ConnectDB();
            int selector = 0;
            do
            {
            Console.WriteLine("Gestiune Universitate\n");
            Console.WriteLine("1-student");
            Console.WriteLine("2-profesor");
            Console.WriteLine("3-owner");
            selector=Convert.ToInt32(Console.ReadLine());
            
               
                switch(selector)
                {
                    case 1:
                        {
                            studenti();
                            break;
                        }
                    case 2:
                        {
                            profesori();
                            break;
                        }
                case 3:
                        {
                            owners();
                            break;
                        }
                case 4: break;
                }
            }while (selector != 4) ;
            
        }
    }
}
