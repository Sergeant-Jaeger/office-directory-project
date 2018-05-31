using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDirectory {

    class Person : IComparable {

        private string name;
        private string officeNumber;
        private string telephoneNumber;

        public Person(string name, string officeNumber, string telephoneNumber) {

            this.name = name;
            this.officeNumber = officeNumber;
            this.telephoneNumber = telephoneNumber;
        }

        public string Name {

            get {

                return this.name;
            }

            set {

                this.name = value;
            }
        }

        public string OfficeNumber {

            get {

                return this.officeNumber;
            }

            set {

                this.officeNumber = value;
            }
        }

        public string TelephoneNumber {

            get {

                return this.telephoneNumber;
            }

            set {

                this.telephoneNumber = value;
            }
        }

        public int CompareTo(Object obj) {

            Person otherPerson = obj as Person;

            return this.name.CompareTo(otherPerson.name);
        }
    }

    class OfficeDirectoryProgram {

        private static Person[] people = new Person[20];

        private static int inUse = 0;

        private static char option;

        private static int searchResult;

        static void Main(string[] args) {

            FillList();

            SelectionLoop();
        }

        private static void FillList() {

            using (StreamReader input = new StreamReader("data.txt")) {

                string tempName;
                string tempOffice;
                string tempTelephone;

                while (!input.EndOfStream) {

                    tempName = input.ReadLine();
                    tempOffice = input.ReadLine();
                    tempTelephone = input.ReadLine();

                    people[inUse] = new Person(name: tempName, officeNumber: tempOffice, telephoneNumber: tempTelephone);

                    inUse++;
                }
            }
        }

        private static void SelectionLoop() {

            do {

                Menu();

                SelectionSwitch();                
            }
            while (Char.ToLower(option) != 'h');
        }

        private static void Menu() {

            Console.Clear();
            Console.WriteLine("a. Print the list");
            Console.WriteLine("b. Add an entry");
            Console.WriteLine("c. Search for Name");
            Console.WriteLine("d. Search for an office number");
            Console.WriteLine("e. Search for a telephone number");
            Console.WriteLine("f. Change an office number");
            Console.WriteLine("g. Sort the list (by name)");
            Console.WriteLine("h. Quit");

            Console.Write("\nSelect an option: ");
            option = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
        }

        private static void SelectionSwitch() {

            switch (Char.ToLower(option)) {
                
                case 'a':
                    
                    PrintList();

                    PrintReturn();

                    break;
                
                case 'b':
                    
                    AddEntry();

                    PrintReturn();

                    break;
                
                case 'c':
                    
                    string sName;

                    sName = StringInput("Enter name to search for: ");

                    int nameResult = SearchName(sName);

                    PrintSearch(nameResult, "The name", sName);

                    PrintReturn();

                    break;

                case 'd':

                    string sOffice;

                    sOffice = StringInput("Enter office number to search for: ");

                    int officeResult = SearchOffice(sOffice);

                    PrintSearch(officeResult, "The office number", sOffice);

                    PrintReturn();

                    break;

                case 'e':

                    string sTelephone;

                    sTelephone = StringInput("Enter telephone number to search for: ");

                    int telephoneResult = SearchTelephone(sTelephone);

                    PrintSearch(telephoneResult, "The telephone number", sTelephone);

                    PrintReturn();

                    break;

                case 'f':

                    ChangeOfficeNumber();

                    PrintReturn();

                    break;

                
                case 'g':

                    SortDirectory();

                    PrintReturn();

                    break;
                
                case 'h':

                    Console.WriteLine("\nPress ENTER key to exit");

                    Console.ReadLine();

                    break;
                
                default:

                    Console.WriteLine("\nThat was not a choice");

                    PrintReturn();

                    break;
            }
        }

        private static void PrintList() {

            Console.Write("Name".PadRight(18, ' '));
            Console.Write("Office Number".PadRight(18, ' '));
            Console.Write("Telephone Number".PadRight(18, ' '));
            Console.Write("\n");

            for (int sub = 0; sub < inUse; sub++) {

                Console.Write(people[sub].Name.PadRight(18, ' '));
                Console.Write(people[sub].OfficeNumber.PadRight(18, ' '));
                Console.Write(people[sub].TelephoneNumber.PadRight(18, ' '));
                Console.Write("\n");
            }
        }

        private static void AddEntry() {

            string tempName, tempOffice, tempTele;

            Console.Write("Enter a name: ");
            tempName = Console.ReadLine();

            if (SearchName(tempName) > -1) {

                Console.WriteLine("\nName is already being used.");

            } else {

                Console.Write("Enter a office number: ");
                tempOffice = Console.ReadLine();

                Console.Write("Enter a telephone number: ");
                tempTele = Console.ReadLine();

                people[inUse] = new Person(tempName, tempOffice, tempTele);

                inUse++;

                Console.WriteLine("\nDone.\n");
            }
        }

        private static int SearchName(string sName) {

            searchResult = -1;

            for (int i = 0; i < inUse; i++) {

                if (people[i].Name == sName) {

                    searchResult = i;
                }
            }

            return searchResult;
        }

        private static int SearchOffice(string sOffice) {

            searchResult = -1;

            for (int i = 0; i < inUse; i++) {

                if (people[i].OfficeNumber == sOffice) {

                    searchResult = i;
                }                   
            }

            return searchResult;
        }

        private static int SearchTelephone(string sTelephone) {

            searchResult = -1;

            for (int i = 0; i < inUse; i++) {

                if (people[i].TelephoneNumber == sTelephone) {

                    searchResult = i;
                }
            }

            return searchResult;
        }

        private static void ChangeOfficeNumber() {

            string cName = StringInput("Enter the name associated with the office number: ");

            int cResult = SearchName(cName);

            if (cResult == -1) {

                Console.WriteLine("The name " + cName + " was not found.\n");
            } else {

                string cOffice = StringInput("Enter the new office number: ");

                people[cResult].OfficeNumber = cOffice;

                Console.WriteLine("\nDone.");
            }
        }

        private static void SortDirectory() {

            Array.Sort(people, 0, inUse);

            Console.WriteLine("\nDone");
        }

        private static void PrintReturn() {

            Console.WriteLine("\nPress ENTER to return to menu");
            Console.ReadLine();
        }

        private static void PrintSearch(int sResult, string sType, string sString) {

            if (sResult == -1) {

                Console.WriteLine(sType + " " + sString + " was not found.\n");
            } else {

                Console.WriteLine(people[sResult].Name + " " + people[sResult].OfficeNumber + "\n");
            }
        }

        private static string StringInput(string userInstruction) {

            Console.WriteLine(userInstruction);

            return Console.ReadLine();
        } 
    }
}
