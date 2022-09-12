using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections;
namespace ATM
{
    public class BankDatabase
    {
        public List<int> ids = new List<int> ();
        private int rndID()
        {
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            return r;
        }

        public int newCVV()
        {
            Random g = new Random();
            int r = g.Next(100, 999);
            return r;
        }
        private bool valid(int p)
        {
            foreach (int id in ids)
            {
                if (id == p) return false;
            }
            return true;
        }
        public int newID()
        {
            int p = rndID();
            if(valid(p)) ids.Add(p);
            return p;
        }

        public int newPin()
        {
            Random g = new Random();
            int r = g.Next(1000, 9999);
            return r;
        }

        
    }


    public class Card
    {
        public int id;
        int exp, cvv, funds;
        string pin,fname, lname, adress;
        BankDatabase db = new BankDatabase();
        public Card()
        {
            id = 0;
            exp = 0;
            cvv = 0;
            funds = 0;
            pin = "";
            fname = "";
            lname = "";
            adress = ""; 
            
        }

        ~Card()
        {

        }
        public void Create()
        {
      
            Console.WriteLine("It seems like you want to create a card.");
            Console.WriteLine("Tell me your first name.");
            while(fname == "" || (fname[0]>='a' && fname[0]<='z')) 
            {
                fname = Console.ReadLine();
                if (fname == "" || (fname[0] >= 'a' && fname[0] <= 'z')) Console.WriteLine("Invalid first name! Please enter it again!");
            }
            Console.WriteLine("Tell me your last name.");
            while(lname == "" || (lname[0] >= 'a' && lname[0] <= 'z'))
            {
                lname = Console.ReadLine();
                if (lname == "" || (lname[0] >= 'a' && lname[0] <= 'z')) Console.WriteLine("Invalid last name! Please enter it again!");
            }
            Console.WriteLine("Alright, can you tell me your adress?");
            adress = Console.ReadLine();
            Console.WriteLine("Creating the card...");
            id = db.newID();
            Console.WriteLine("Your cards iD is " + id);
            exp = 5;
            Console.WriteLine("It expires in 5 years");
            cvv = db.newCVV();
            Console.WriteLine("And your security code(CVV) is " + cvv);
            pin = Convert.ToString(db.newPin());
            Console.WriteLine("And your card's pin is: " + pin);
        }

        public bool security()
        {
            Console.WriteLine("Enter your credit card's pin: ");
            string k = Console.ReadLine();
            return (k == pin);
        }
        public void checkFunds()
        {
            Console.WriteLine("You have " + funds + "$ in your account.");
        }
        public void addFunds()
        {
            if (security() == true)
            {
                Console.WriteLine("Enter the sum of money you wish to add to your credit card.");
                int add = Convert.ToInt32(Console.ReadLine());
                funds += add;
                checkFunds();
            }
            else Console.WriteLine("Invalid pin!");
        }
        public void withdraw()
        {
            if (security() == true)
            {
                Console.WriteLine("Enter the sum of money you wish to withdraw from your credit card.");
                int add = Convert.ToInt32(Console.ReadLine());
                if (funds > add) funds -= add;
                else Console.WriteLine("An error occoured because you tried withdrawing more money than deposited");
                checkFunds();
            }
            else Console.WriteLine("Invalid pin!");
        }

        public void changePin()
        {
            if(security() == true)
            {
                Console.WriteLine("Enter the new pin: ");
                string p1 = Console.ReadLine();
                Console.WriteLine("Enter the new pin once again: ");
                string p2 = Console.ReadLine();
                if (p1 == p2 && p1.Length==4) pin = p1;
                else Console.WriteLine("Something didn't work properly.");
            }
            else Console.WriteLine("Invalid pin!");

        }

    
    }


    class Program
    {
        
        static void Main(string[] args)
        {
            Card[] cards = new Card[50];
            int ok = 0;
            int i = 0;
            while (ok == 0)
            {
                Console.Clear();
                Console.WriteLine("------------Welcome to TBK ATM's------------");
                Console.WriteLine("Press 1:...................Create a new card");
                Console.WriteLine("Press 2:..........................View funds");
                Console.WriteLine("Press 3:...........................Add funds");
                Console.WriteLine("Press 4:......................Withdraw funds");
                Console.WriteLine("Press 5:..........................Change pin");
                Console.WriteLine("Press 0:...........................Close ATM");
                int c = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (c)
                {
                    case 1: //create
                        {
                            cards[i] = new Card();
                            cards[i].Create();
                            i++;
                            Console.WriteLine("Do you wish to come back to the main screen?(true/false)");
                            bool a = Convert.ToBoolean(Console.ReadLine());
                            if (a == false) ok = 1;
                            break;
                        }
                    case 2: // view funds
                        {
                            Console.WriteLine("What is your credit card's ID?");
                            int n = Convert.ToInt32(Console.ReadLine());
                            int j = 0;
                            while(j<i)
                            {
                                if (cards[j].id == n)
                                {
                                    break;
                                }
                                j++;
                            }
                            //------------------------------------------------------------------------------
                            
                            if(cards[j].security() == true)
                            {
                                cards[j].checkFunds();
                            }
                            else Console.WriteLine("Invalid pin!");
                            //------------------------------------------------------------------------------
                            Console.WriteLine("Do you wish to come back to the main screen?(true/false)");
                            bool a = Convert.ToBoolean(Console.ReadLine());
                            if (a == false) ok = 1;
                            break;
                        }
                    case 3: //add funds
                        {
                            Console.WriteLine("What is your credit card's ID?");
                            int n = Convert.ToInt32(Console.ReadLine());
                            int j = 0;
                            while (j < i)
                            {
                                if (cards[j].id == n)
                                {
                                    break;
                                }
                                j++;
                            }
                            //------------------------------------------------------------------------------
                            cards[j].addFunds();
                            //------------------------------------------------------------------------------
                            Console.WriteLine("Do you wish to come back to the main screen?(true/false)");
                            bool a = Convert.ToBoolean(Console.ReadLine());
                            if (a == false) ok = 1;
                            break;
                        }
                    case 4: //withdraw funds
                        {
                            Console.WriteLine("What is your credit card's ID?");
                            int n = Convert.ToInt32(Console.ReadLine());
                            int j = 0;
                            while (j < i)
                            {
                                if (cards[j].id == n)
                                {
                                    break;
                                }
                                j++;
                            }
                            //------------------------------------------------------------------------------
                            cards[j].withdraw();
                            //------------------------------------------------------------------------------
                            Console.WriteLine("Do you wish to come back to the main screen?(true/false)");
                            bool a = Convert.ToBoolean(Console.ReadLine());
                            if (a == false) ok = 1;
                            break;
                        }
                    case 5: //change pin
                        {
                            Console.WriteLine("What is your credit card's ID?");
                            int n = Convert.ToInt32(Console.ReadLine());
                            int j = 0;
                            while (j < i)
                            {
                                if (cards[j].id == n)
                                {
                                    break;
                                }
                                j++;
                            }
                            //------------------------------------------------------------------------------
                            cards[j].changePin();
                            //------------------------------------------------------------------------------
                            Console.WriteLine("Do you wish to come back to the main screen?(true/false)");
                            bool a = Convert.ToBoolean(Console.ReadLine());
                            if (a == false) ok = 1;
                            break;
                        }
                    default:
                        {
                            ok = 1;
                            break;
                        }
                }
            }
            
        }
    }
}