using System;
namespace DogBreedingManager;

public class Dog : Animal //klasa bazowa dzedzicząca po abstrakcyjnej
{
    
    //dodane potem w klasie abstrakcyjnej>> private DateTime dateOfBirth; // hermetyzacja danych

    private string sex;
    
    public string Sex
    {
        get { return sex; }
        
        set { sex = value; } //chroniony setter
    }
    
    //dodane potem w klasie abstrakcyjnej>> public string Name { get; set; } //podstawowe dane o obiektach
    public string Breed { get; set; }
    /*
     //dodane potem w klasie abstrakcyjnej>>
     
     public DateTime DateOfBirth 
    {
        
        get { return dateOfBirth; } //getter pozwala odczytać datę
        
        set //setter pozwala zapisać datę
        {
            if (value <= DateTime.Now) //sprawdzamy czy data NIE jest z przyszłości
            {
                dateOfBirth = value;
            }
        }
    }
    */
    public bool IsVaccinated { get; set; } //czy pies jest szczepiony
    
    //dodane potem w klasie abstrakcyjnej>> public bool IsAlive { get; set; } = true; //czy pies żyje

    public string MotherName { get; set; } = "Brak informacji"; //imiona rodziców
    
    public string FatherName { get; set; } = "Brak informacji";
    
    public override void DisplayInfo()  //metoda wyświetlająca wszystkie informacje o psie
    {
        Console.WriteLine("----- INFORMACJE O PSIE -----");

        Console.WriteLine("Imię: " + Name);
        Console.WriteLine("Rasa: " + Breed);
        
        Console.WriteLine("Płeć: " + Sex); //polimorfizm - płeć pochodzi z właściwości Sex
        
        Console.WriteLine("Data urodzenia: " + DateOfBirth.ToShortDateString());
        
        Console.WriteLine("Szczepienia: " + (IsVaccinated ? "Tak" : "Nie"));
        
        Console.WriteLine("Żyje: " + (IsAlive ? "Tak" : "Nie"));
        
        Console.WriteLine("Matka: " + MotherName);
        Console.WriteLine("Ojciec: " + FatherName);

        Console.WriteLine("-----------------------------");
    }

}