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
       // public static String updateUniversitateForProfbyPrenume = "UPDATE UniversitateIntermediere SET Nota=@Nota where Prenume_Student=@Prenume_Student and Profesor=@Profesor";
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

    public interface IAddDataInDB
    {
        void AddDB();
    }
    public interface IDeleteDataInDB
    {
        void DeleteDB(string table);
    }
    public interface IUpdateDataInDB
    {
        void UpdateDB();
    }
    public interface IViewDataFromDB
    {
        void ViewDB(string comanda, string table);
    }
    public interface IFindDataFromDB
    {
        void FindDB(string comanda, string table,string conditie);
    }

    public class Student: ConnectDB , IViewDataFromDB,IFindDataFromDB
    {
        
        public Student()
        {

        }
        public void FindDB(string comandWithoutTable, string table, string where )
        {

        }
        public void ViewDB(string comandWithoutTable,string table)
        {
            string sqlCommand = comandWithoutTable + table;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
           // command.Parameters.Add();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine("==========================================================\n");
            for (int j = 0; j < reader.FieldCount; j++)
            {
                Console.Write(reader.GetName(j));
                Console.Write("\t \t ");
            }
            Console.Write("\n");
            Console.WriteLine("==========================================================\n");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Nota")
                    {
                        Console.Write(reader.GetInt32(i));
                    }
                    else
                    {
                        Console.Write(reader.GetString(i).ToString());
                    }

                    Console.Write("\t \t \t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("==========================================================\n");
            Console.ReadLine();
            reader.Close();
            connection.Close();
        }
    }
    public class Profesor : ConnectDB,IViewDataFromDB,IAddDataInDB,IUpdateDataInDB,IDeleteDataInDB
    {
        private string Profesr="NUL";
        private string Nume_Student = "Null";
        private string Prenume_Student= "Null";
        private string Curs = "Null";
        private string conditie1 = "";
        private string conditie2 = "";
        private int Nota;

        public Profesor(string profesor)
        {
            Profesr = profesor;
        }
        void conditie()
        {
            Console.WriteLine("introduceti dntificatorul pentru update\n");
            Console.WriteLine("1->Nume");
            Console.WriteLine("2->Prenume");
            Console.WriteLine("3->Curs");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number == 1)
            {
                conditie1 = "Nume_Student";
                Console.WriteLine("introduceti numele studentului");
                conditie2 = Console.ReadLine();
            }
            else if (number == 2)
            {
                conditie1 = "Prenume_Student";
                Console.WriteLine("introduceti prenumele studentului");
                conditie2 = Console.ReadLine();
            }
            else if (number == 3)
            {
                conditie1 = "Curs";
                Console.WriteLine("introduceti cursul ");
                conditie2 = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nu ati introdus o valoare valida!");
            }
        }
        public void DeleteDB(string table)
        {
            conditie();
            string sqlCommand = "Delete from "+table+" where '" + conditie1 + "'='" + conditie2 + "'and Profesor='"+Profesr+"'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public void UpdateDB()
        {

            conditie();
            scriere();
            string sqlCommand = "UPDATE" + Globals.Tabela2 + " SET Nume_Student='" + Nume_Student + "',Prenume_Student='" + Prenume_Student + "',Nota='" + Nota + "' where " + conditie1 + "='" + conditie2 + "'and Profesor='"+Profesr+"'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();

            //"UPDATE UniversitateIntermediere SET(Nume_Student='asd',Prenume_Student='asasf',Nota='5') where Nume_Student='cata'";
        }
        public  void AddDB()
        {
            scriere();
            
            string sqlCommand = "INSERT INTO" + Globals.Tabela2 + "(Profesor,Curs,Nume_Student,Prenume_Student,Nota) VALUES ('" + Profesr + "','" + Curs + "','" + Nume_Student + "','" + Prenume_Student + "','" + Nota + "')";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        void scriere()
        {
            Console.WriteLine("Introduceti numele Studentului:\n");
            Nume_Student = Console.ReadLine();
            Console.WriteLine("Introduceti Prenumele Studentului:\n");
            Prenume_Student = Console.ReadLine();
            Console.WriteLine("Introduceti Curs :\n");
            Curs = Console.ReadLine();
            Console.WriteLine("Introduceti Nota Studentului:\n");
            Nota =int.Parse(Console.ReadLine());

        }

        public void ViewDB(string comandWithoutTable, string table)
        {
            string sqlCommand = comandWithoutTable + table+Globals.profesorCondition+"'"+Profesr+"'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();	        
		    OleDbDataReader reader;
            reader = command.ExecuteReader();
            
            Console.WriteLine("==========================================================\n");
            for (int j = 0; j < reader.FieldCount; j++)
            {
                Console.Write(reader.GetName(j));
                Console.Write("\t \t ");
            }
            Console.Write("\n");
            Console.WriteLine("==========================================================\n");
            while (reader.Read())
            {

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    
                    if(reader.GetName(i)=="Nota")
                    {
                        Console.Write(reader.GetInt32(i));
                    }
                    else
                    {
                        Console.Write(reader.GetString(i).ToString());
                    }
                    
                    Console.Write("\t \t \t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("==========================================================\n");
            Console.ReadLine();
            reader.Close();
            connection.Close();
        }


    }
    public class Owner : ConnectDB,IAddDataInDB,IUpdateDataInDB,IDeleteDataInDB,IViewDataFromDB
    {
        private string facultate;
        private string curs;
        private string profesor;
        public void ViewDB(string comandWithoutTable, string table)
        {
            string sqlCommand = comandWithoutTable + table;
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine("==========================================================\n");
            for (int j = 0; j < reader.FieldCount; j++)
            {
                Console.Write(reader.GetName(j));
                Console.Write("\t \t ");
            }
            Console.Write("\n");
            Console.WriteLine("==========================================================\n");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Nota")
                    {
                        Console.Write(reader.GetInt32(i));
                    }
                    else
                    {
                        Console.Write(reader.GetString(i).ToString());
                    }

                    Console.Write("\t \t \t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("==========================================================\n");
            Console.ReadLine();
            reader.Close();
            connection.Close();
        }
        void scriere()
        {
            Console.WriteLine("Introduceti numele Facultatii:\n");
            facultate = Console.ReadLine();
            Console.WriteLine("Introduceti materia:\n");
            curs = Console.ReadLine();
            Console.WriteLine("Introduceti profesor :\n");
            profesor = Console.ReadLine();

        }
        public void AddDB()
        {
            Console.WriteLine("ADDFunction!\n");
            scriere();

            string sqlCommand = "INSERT INTO" + Globals.Tabela1 + "(Facultate,Materie,Profesor) VALUES ('" + facultate + "','" + curs + "','" + profesor + "'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        void conditie()
        {
            Console.WriteLine("introduceti numele profesorului dupa care vreti sa stergeti\n");
            profesor = Console.ReadLine();
           
        }
        public void DeleteDB(string table)
        {
            Console.WriteLine("DeleteFunction!\n");
            conditie();
            string sqlCommand = "Delete from " + table + " where Profesor='" + profesor + "'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();
        }
        public void UpdateDB()
        {
            Console.WriteLine("UpdateFunction!\n");
            scriere();
            string sqlCommand = "UPDATE" + Globals.Tabela2 + " SET Facultate='" + facultate + "',Materie='" + curs + "',Profesor='" + profesor + "'";
            OleDbCommand command = new OleDbCommand(sqlCommand, connection);
            connection.Open();
            OleDbDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();

        }
    }
    class Program
    {
        static void studenti ()
        {
            int selector = 0;
            int maxItem=2;
            Console.WriteLine("STUDENT");
            Student student = new Student();
            do
            {
                Console.WriteLine("Optiuni Student\n");
                Console.WriteLine("1-Vizualizare nivel Universitate");
                Console.WriteLine("2-Vizualizare Nivel studenti");
                selector = Convert.ToInt32(Console.ReadLine());
                switch(selector)
                {
                    case 1: student.ViewDB(Globals.selectALL, Globals.Tabela1);
                        break;
                    case 2: student.ViewDB(Globals.selectALL, Globals.Tabela2);
                        break;
                    default: break;
                }


            } while (selector != maxItem);
            
        }
        static void profesori()
        {
            int selector = 0;
            int maxItem = 6;
            Console.WriteLine("PROFESOR");
            string profesor;
            Console.WriteLine("Introduceti numele dumneavoastra:\n");
            profesor=Console.ReadLine();
            Profesor prof = new Profesor(profesor);
            do
            {
                Console.WriteLine("Optiuni Profesor\n");
                Console.WriteLine("1-Vizualizare nivel Universitate");
                Console.WriteLine("2-Vizualizare Nivel studenti");
                Console.WriteLine("3-Adaugare date nivel studenti");
                Console.WriteLine("4-Update date nivel studenti");
                Console.WriteLine("5-Delete date nivel studenti");
                Console.WriteLine("6-Exit");
                selector = Convert.ToInt32(Console.ReadLine());
                switch (selector)
                {
                    case 1: prof.ViewDB(Globals.selectALL, Globals.Tabela1);
                        break;
                    case 2: prof.ViewDB(Globals.selectALL, Globals.Tabela2);
                        break;
                    case 3: prof.AddDB();
                        break;
                    case 4: prof.UpdateDB();
                        break;
                    case 5: prof.DeleteDB(Globals.Tabela2);
                        break;
                    case 6: break;
                    default: break;
                }


            } while (selector != maxItem);

        }
        static void owners()
        {
            Console.WriteLine("PROFESOR");
            int selector = 0;
            int maxItem = 6;
            Owner owner = new Owner();
            do
            {
                Console.WriteLine("Optiuni Student\n");
                Console.WriteLine("1-Vizualizare nivel Universitate");
                Console.WriteLine("2-Vizualizare Nivel studenti");
                Console.WriteLine("3-Add Date nivel Universitate");
                Console.WriteLine("4-Update Date nivel Universitate");
                Console.WriteLine("5-Delete Date nivel Universitate");
                Console.WriteLine("6-Exit");
                selector = Convert.ToInt32(Console.ReadLine());
                switch (selector)
                {
                    case 1: owner.ViewDB(Globals.selectALL, Globals.Tabela1);
                        break;
                    case 2: owner.ViewDB(Globals.selectALL, Globals.Tabela2);
                        break;
                    case 3: owner.AddDB();
                        break;
                    case 4: owner.UpdateDB();
                        break;
                    case 5: owner.DeleteDB(Globals.Tabela1);
                        break;
                    case 6: break;
                    default: break;
                }


            } while (selector != maxItem);

        }
     
        static void Main(string[] args)
        {
            ConnectDB db = new ConnectDB();
            //Student student = new Student();
            //student.ViewDB(Globals.selectALL, Globals.Tabela1);
            //string profesor;
            //Console.WriteLine("Profesor:\n");
            //profesor=Console.ReadLine();
            //Profesor prof = new Profesor(profesor);
            //prof.AddDB();
            //prof.ViewDB(Globals.selectALL,Globals.Tabela2);
            //prof.ViewDB(Globals.selectALL, Globals.Tabela2);
           // prof.DeleteDB(Globals.Tabela2);
            int selector = 0;
            do
            {
                 //Console.Clear();
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
