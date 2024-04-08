using System;
using System.Data.SqlTypes;
using Clients;

Client myClient = new();
List<Client> listOfClients = [];

string previousFirstName = myClient.FirstName;
string previousLastName = myClient.LastName;
double previousWeight = myClient.Weight;
double previousHeight = myClient.Height;

string customPrompt = "";

bool loopAgain = true;


  while (loopAgain)
  {
    try 
    {
      DisplayMainMenu();
        string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
       if (mainMenuChoice == "N")
      {
         myClient = NewClient();
      }
      else if (mainMenuChoice == "S")
      {
        ShowClientInfo(myClient);
      }
      else if (mainMenuChoice == "E")
        {
            while (true)
              {
                DisplayEditMenu();
                  string editMenuChoice = Prompt("\nEnter an Edit Choice: ").ToUpper();
                  if (editMenuChoice == "F")
                    {
                    customPrompt = "Enter a New First Name:";
                     GetFirstName(myClient, customPrompt);
                     Console.WriteLine($"\nClient First Name has heen edited to {myClient.FirstName}. ");

                     customPrompt = " ";
                    }
                  else if (editMenuChoice == "L")
                  {
                    GetLastName(myClient);
                  }
                  else if (editMenuChoice == "W")
                  {
                     GetWeight(myClient);
                  }
                  else if (editMenuChoice == "H")
                  {
                     GetHeight(myClient);
                  }
                  else if (editMenuChoice == "R")  
                  {
                      throw new Exception("Returning to Main Menu");
                  }
                  else 
                  {
                    throw new Exception("Invalid Choice ");
                  }
                  
              }
        }
       else if (mainMenuChoice == "A")
       {
        //  AddClientToList(myClient, listOfClients);
        Console.WriteLine("You picked A!");
       }
       else if (mainMenuChoice == "F")
       {
        //  myClient = FindClientInList(listOfClients);
        Console.WriteLine("You picked F!");
       }
       else if (mainMenuChoice == "R")
       {
        //  RemoveClientFromList(myClient, listOfClients);
        Console.WriteLine("You picked R!");
       }
      else if (mainMenuChoice == "Q")
        {
        // SaveMemoryValuesToFile(listOfClients);
        loopAgain = false;
          throw new Exception("Bye, hope to see you again! ");
        }
        else 
        {
          throw new Exception("Invalid Choice ");
        }
    } catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }


void DisplayMainMenu()
{
  Console.WriteLine("\n===== Main Menu =====");
	Console.WriteLine("N) New Client ");
	Console.WriteLine("S) Show Client Info ");
	Console.WriteLine("E) Edit Client Info ");
	Console.WriteLine("A) Add Client to List ");
	Console.WriteLine("F) Find Client in List ");
	Console.WriteLine("R) Remove Client from List");
	Console.WriteLine("Q) Quit");

}

void DisplayEditMenu()
{
	Console.WriteLine("\n===== Edit Menu =====");
	Console.WriteLine("F) Edit First Name ");
	Console.WriteLine("L) Edit Last Name");
	Console.WriteLine("W) Edit Weight");
	Console.WriteLine("H) Edit Height");
	Console.WriteLine("R) Return to Main Menu");
}

Client NewClient()
  { 

    Client myClient = new();
    
    Console.WriteLine("Enter Client Details.");

    
    GetFirstName(myClient, "Enter Client First Name: ");
    GetLastName(myClient);
    GetWeight(myClient);
    GetHeight(myClient);

    Console.WriteLine("Client succesfully have been born. ");

    return myClient;
  }


static void ShowClientInfo(Client client)
    {
        if (client == null)
            throw new Exception($"No client in memory");

        Console.WriteLine("===== Client Info =====");
        Console.WriteLine($"Client Name: {client.FullName}");
        Console.WriteLine($"BMI Score: {client.BmiScore}");
        Console.WriteLine($"BMI Status: {client.BmiStatus}");
    }


void GetFirstName(Client client, string prompt)
{
    string myString = Prompt(prompt);
    client.FirstName = myString;
}

void GetLastName(Client client)
{
    string myString = Prompt($"Enter Client Last Name: ");
    client.LastName = myString;
}

void GetWeight(Client client)
{
    double myDouble = PromptDoubleBetweenMinMax($"Enter Weight in pounds: ", 0 , 500);
    client.Weight = myDouble;
}

void GetHeight(Client client)
{
    double myDouble = PromptDoubleBetweenMinMax($"Enter Height in inches: ", 0 , 200);
    client.Height = myDouble;
}


string Prompt(string prompt)
{
  string response = "";
  Console.Write(prompt);
  response = Console.ReadLine();
  return response;
}

double PromptDoubleBetweenMinMax(String msg, double min, double max)
{
  bool inValidInput = true;
  double num = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write($"{msg} between {min} and {max}: ");
      num = double.Parse(Console.ReadLine());
      if (num <= min || num >= max)
        throw new Exception($"Must be between {min} and {max} exclusive");
      inValidInput = false; 
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Invalid: {ex.Message}");
    }
  }
  return num;
}

  