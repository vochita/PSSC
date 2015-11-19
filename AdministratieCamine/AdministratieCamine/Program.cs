using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace AdministratieCamine
{
    public static class Globals
    {
        public static String Tabela1 = " Camin";
        public static String Tabela2 = " Caminintermediere";
        public static String selectALL = "Select * from";
        public static String selectALLFromCamin = "Select * from Camin";
        public static String selectALLFromCaminintermediere = "Select * from Caminintermediere";
        public static String selectALLFromCaminintermedierebyStudent = "Select * from Caminintermediere where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";
        public static String selectALLFromCaminbyNr_Camin = "Select * from Camin where Nr_Camin=@Nr_Camin ";
        public static String addINTOCamin = "INSERT INTO Camin(Nr_Camin,Barem,Administrator) VALUES (@Nr_Camin,@Barem,@Administrator)";
        public static String addINTOCaminintermediere = "INSERT INTO Caminintermediere(Nr_Camin,Nume_Student,Prenume_Student,Nota,Administrator) VALUES (@Nr_Camin,@Nume_Student,@Prenume_Student,@Nota,@Administrator)";
        public static String updateCaminbyNr_Camin = "UPDATE Camin SET Barem=@Barem,Administrator=@Administrator where Nr_Camin=@Nr_Camin";
        public static String updateCaminintermedierebyNr_Camin = "UPDATE Caminintermediere SET Nume_Student=@Nume_Student,Prenume_Student=@Prenume_Student,Administrator=@Administrator where Nr_Camin=@Nr_Camin";
        public static String updateCaminintermedierebyStudent = "UPDATE Caminintermediere SET Nota=@Nota where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";
        public static String deleteFromCaminbyAdministrator = "Delete from Camin where Administrator=@Administrator";
        public static String deleteFromCaminintermedierebyNota = "Delete from Caminintermediere where Nota=@Nota";
        public static String deleteFromCaminintermedierebyStudent = "Delete from Caminintermediere where Nume_Student=@Nume_Student and Prenume_Student=@Prenume_Student";

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
    public interface IAddStudentifromSecretariatIntermediere
    {
        void Adaugarestudentipentrucazare(string comanda, string condition, string condition2);
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
    public class OwnerAdministratie : User,
                                      IAddDataInDB,
                                      IUpdateDataInDB1Condition,
                                      IDeleteDataInDB,
                                      IViewDataFromDB,
                                      IFindDataFromDB1Condition,
                                      IFindDataFromDB2Condotion

    {
        private int Nr_Camin { get; set; }
        private int Barem { get; set; }
        private string Administrator { get; set; }

        public OwnerAdministratie()
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
        public void UpdateDB(string comanda, string condition)
        {

        }
        public void DeleteDB(string comanda, string condition)
        {

        }
        public void AddDB(string comanda)
        {

        }

    }
    public class Administrator:User,
                            IViewDataFromDB,
                            IAddDataInDB,
                            IUpdateDataInDB1Condition,
                            IDeleteDataInDB,
                            IDeleteDataInDB2Condition,
                            IFindDataFromDB1Condition,
                            IFindDataFromDB2Condotion,
                            IUpdateDataInDB2Condition,
                            IAddStudentifromSecretariatIntermediere
    {
        private int Nr_Camin { get; set; }
        private string Nume_Student { get; set; }
        private string Prenume_Student { get; set; }
        private int Medie { get; set; }
        private string AdministratorCamin { get; set; }
        public Administrator(string administrator)
        {
            AdministratorCamin = administrator;
            login();
        }
        public override bool login()
        {
            return true;
        }
        public void DeleteDB(string comanda, string condition)
        {
           
        }
        public void DeleteDB(string comanda, string condition, string condition2)
        {
            
        }
        public void UpdateDB(string comanda, string condition)
        {
          
        }
        public void UpdateDB(string comanda, string condition, string condition2)
        {
            
        }
        public void AddDB(string comanda)
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
         public void Adaugarestudentipentrucazare(string comanda, string condition, string condition2)
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
                    Console.WriteLine("1-Vizualizare nivel Caminintermediere");
                    Console.WriteLine("2-Vizualizare Nivel Caminin");
                    Console.WriteLine("3-Find anything in Caminintermediere by Nota ");
                    Console.WriteLine("4-Find anything in Caminin by Nr_Camin ");
                    Console.WriteLine("5-Find anything in Caminintermediere by Nr_Camin ");
                    Console.WriteLine("6-Find anything in Caminintermediere by student ");
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            {
                                Console.WriteLine("Vizualizare nivel Caminintermediere");
                                break;
                            }
                        case 2: Console.WriteLine("Vizualizare Nivel Caminin");
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
        static void owners()
        {
            Console.WriteLine("OWNER");
            int selector = 0;
            int maxItem = 14;
            OwnerAdministratie owner = new OwnerAdministratie();
            if (owner.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Owner\n");
                    Console.WriteLine("1-Vizualizare nivel Camin");
                    Console.WriteLine("2-Vizualizare Nivel CaminIntermediar");
                    Console.WriteLine("3-Add Date nivel Camin");
                    Console.WriteLine("4-Update Date nivel Camin by Administrator");
                    Console.WriteLine("5-Update Date nivel Camin by Nr_Camin");
                    Console.WriteLine("6-Update Date nivel Camin by Barem");
                    Console.WriteLine("7-Delete Date nivel Camin by Administrator");
                    Console.WriteLine("8-Delete Date nivel Camin by Nr_Camin");
                    Console.WriteLine("9-Delete Date nivel Camin by Barem");
                    Console.WriteLine("10-Find anything in Camin by Administrator ");
                    Console.WriteLine("11-Find anything in Camin by Nr_Camin ");
                    Console.WriteLine("12-Find anything in Camin by Barem");
                    Console.WriteLine("13-Exit");
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
                Console.WriteLine("Eroare la autentificare!");
            }
        }
        static void administrator()
        {
            int selector = 0;
            int maxItem = 13;
            Console.WriteLine("Administrator");
            string admin;
            Console.WriteLine("Introduceti numele dumneavoastra:\n");
            admin = Console.ReadLine();
            Administrator prof = new Administrator(admin);
            if (prof.login() == true)
            {
                do
                {
                    Console.WriteLine("Optiuni Administrator\n");
                    Console.WriteLine("1-Vizualizare nivel Camin");
                    Console.WriteLine("2-Vizualizare Nivel Caminintermediar");
                    Console.WriteLine("3-Adaugare date nivel Caminintermediar");
                    Console.WriteLine("4-Update date nivel Caminintermediar by student(nume,prenume)");
                    Console.WriteLine("5-Update date nivel Caminintermediar by Nr_camin");
                    Console.WriteLine("6-Delete date nivel Caminintermediar by student(nume,prenume)");
                    Console.WriteLine("7-Delete date nivel Caminintermediar by Nr_camin");
                    Console.WriteLine("8-Find anything in Caminintermediar by Student ");
                    Console.WriteLine("9-Find anything in Caminintermediar by Nr_camin ");
                    Console.WriteLine("10-Find anything in Caminintermediar by Nota ");
                    Console.WriteLine("11-Request studenti from secretariat ");
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
                        default: break;
                    }


                } while (selector != maxItem);
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
                Console.WriteLine("Gestiune Universitate\n");
                Console.WriteLine("1-student");
                Console.WriteLine("2-administrator");
                Console.WriteLine("3-owner");
                selector = Convert.ToInt32(Console.ReadLine());


                switch (selector)
                {
                    case 1:
                        {
                            studenti();
                            break;
                        }
                    case 2:
                        {
                            administrator();
                            break;
                        }
                    case 3:
                        {
                            owners();
                            break;
                        }
                    case 4: break;
                }
            } while (selector != 4);
        }
    }
}
