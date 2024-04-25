using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using Clients;

Client myClient = new();
List<Client> listOfClients = [];

string customPrompt = "";

bool loopAgain = true;

LoadFileValuesToMemory(listOfClients);


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
      else if (mainMenuChoice == "D")
      {
        DisplayAllClientInList(listOfClients);
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
                     Console.WriteLine($"\nClient's First Name has heen edited to {myClient.FirstName}. ");

                     customPrompt = " ";
                    }
                  else if (editMenuChoice == "L")
                  {
                    customPrompt = "Enter a New Last Name:";
                    GetLastName(myClient, customPrompt);
                    Console.WriteLine($"\nClient's Last Name has heen edited to {myClient.LastName}. ");

                     customPrompt = " ";
                  }
                  else if (editMenuChoice == "W")
                  {
                    customPrompt = "Enter a New Weight Name:";
                     GetWeight(myClient, customPrompt);
                     Console.WriteLine($"\nClient's Weight has heen edited to {myClient.Weight}. ");

                     customPrompt = " ";
                  }
                  else if (editMenuChoice == "H")
                  {
                    customPrompt = "Enter a New Height Name:";
                     GetHeight(myClient, customPrompt);
                     Console.WriteLine($"\nClient's Height has heen edited to {myClient.Height}. ");

                     customPrompt = " ";
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
         AddClientToList(myClient, listOfClients);
       }
       else if (mainMenuChoice == "F")
       {
          myClient = FindClientInList(listOfClients);
       }
       else if (mainMenuChoice == "R")
       {
         RemoveClientFromList(myClient, listOfClients);
       }
      else if (mainMenuChoice == "Q")
        {
        SaveMemoryValuesToFile(listOfClients);
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
  Console.WriteLine("D) Display All Client Info");
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
    GetLastName(myClient, "Enter Client Last Name: ");
    GetWeight(myClient, "Enter Client Weight Name: ");
    GetHeight(myClient, "Enter Client Height Name: ");

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

void GetLastName(Client client, string prompt)
{
    string myString = Prompt($"Enter Client Last Name: ");
    client.LastName = myString;
}

void GetWeight(Client client, string prompt)
{
    double myDouble = PromptDoubleBetweenMinMax($"Enter Weight in pounds: ", 0 , 500);
    client.Weight = myDouble;
}

void GetHeight(Client client, string prompt)
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

void AddClientToList(Client client, List<Client> listOfClients)
{
  listOfClients.Add(myClient);
  Console.WriteLine("Client succesfully added to List of Clients. ");
}

void DisplayAllClientInList(List<Client> listOfClients)
{
  foreach(Client client in listOfClients)
      ShowClientInfo(client);
}

Client FindClientInList(List<Client> listofClients){
    string myString = Prompt($"Enter Partial Client Name: ");
    foreach(Client client in listofClients)
    {
      if(client.FirstName.Contains(myString))
      {
            return client;
      }
    }
    Console.WriteLine($"No Clients Match");
	  return null;
}


void RemoveClientFromList(Client myClient, List<Client> listOfPets)
{
	
	if(myClient == null)
		throw new Exception($"No Client provided to remove from list");
	listOfClients.Remove(myClient);
	Console.WriteLine($"Client Removed");
}


void LoadFileValuesToMemory(List<Client> listOfClients)
{
  
  string fileName = "client-list.csv";
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
	string[] csvFileInput = File.ReadAllLines(filePath);
	for(int i = 0; i < csvFileInput.Length; i++)
	{
		string[] items = csvFileInput[i].Split(',');
		
				Client myClient = new(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
				listOfClients.Add(myClient);
		
	}
  Console.WriteLine($"Load complete. {fileName} has {listOfClients.Count} data entries");
}

void SaveMemoryValuesToFile(List<Client> listOfPets)
{
  string fileName = "client-list.csv";
  string filePath = $"data/{fileName}";
  string[] csvLines = new string[listOfClients.Count];
  List<string> clientsToFile = [];
  foreach (Client client in listOfClients) {
    clientsToFile.Add($"{client.FirstName}, {client.LastName}, {client.Weight}, {client.Height}");
  }
  File.WriteAllLines(filePath, clientsToFile);
  Console.WriteLine($"Save complete. {fileName} has {listOfPets.Count} entries.");

}
  