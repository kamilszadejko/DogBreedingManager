/*

using System;
using System.Collections.Generic;

namespace DogBreedingManager;

public class Program
{
    public static void Main(string[] args)
    {
        List<Dog> dogs = DogDataService.LoadDogs(); //wczytywanie psów z json przy startcie
        
        bool isRunning = true;
        
        while (isRunning) //program działa dopóki ten warunek jest true
        {
            Console.WriteLine();
            Console.WriteLine("===== DOG BREEDING MANAGER =====");
            Console.WriteLine("1. Pokaż listę psów");
            Console.WriteLine("2. Wyświetl informacje o psie");
            Console.WriteLine("3. Edytuj psa");
            Console.WriteLine("4. Dodaj psa");
            Console.WriteLine("0. Zakończ program");
            Console.WriteLine("================================");
            Console.Write("Wybierz opcję: ");
            
            string choice = Console.ReadLine();
            
            switch (choice) //======================================== MENU
            {
                case "1":
                    Console.WriteLine("Wybrano: Pokaż listę psów");
                    ShowDogList(dogs);
                    PauseAndClear();
                    break;

                case "2":
                    Console.WriteLine("Wybrano: Wyświetl informacje o psie");
                    ShowDogDetails(dogs);
                    PauseAndClear();
                    break;

                case "3":
                    Console.WriteLine("Wybrano: Edytuj psa");
                    EditDog(dogs);
                    PauseAndClear();
                    break;

                case "4":
                    Console.WriteLine("Wybrano: Dodaj psa");
                    AddDog(dogs);
                    PauseAndClear();
                    break;

                case "0":
                    Console.WriteLine("Zamykanie programu...");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }
        Console.WriteLine("Naciśnij dowolny klawisz, aby zamknąć program...");
        Console.ReadKey();
    }//koniec Main
    
    //========================================================================= Menu 1
    static void ShowDogList(List<Dog> dogs) //metoda wyświetla listę psów
    {
        if (dogs.Count == 0) //czy lista jest pusta
        {
            Console.WriteLine("Brak psów w bazie.");
            return;
        }

        Console.WriteLine("===== LISTA PSÓW =====");

        for (int i = 0; i < dogs.Count; i++) //pętla z indeksami psów
        {
            Console.WriteLine(i + ". " + dogs[i].Name + " (" + dogs[i].Sex + ")");
        }

        Console.WriteLine("======================");
    }
    //========================================================================= Menu 2
    static void ShowDogDetails(List<Dog> dogs) //metoda wyświetla dane o psie
    {
        Console.Write("Podaj indeks psa: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index)) //czy użytkownik podał liczbę
        {
            if (index >= 0 && index < dogs.Count) //czy index istnieje na liście
            {
                dogs[index].DisplayInfo(); //wywołuje metodę na danym psie
            }
            else
            {
                Console.WriteLine("Nie ma psa o takim indeksie.");
            }
        }
        else
        {
            Console.WriteLine("Niepoprawny numer.");
        }
    }
    
//========================================================================= Menu 3
static void EditDog(List<Dog> dogs) //metoda edycji informacji o psie
{
    Console.Write("Podaj indeks psa do edycji: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out int index))
    {
        if (index >= 0 && index < dogs.Count)
        {
            Dog dog = dogs[index];
            
            Console.WriteLine("1. Zmień status szczepienia");
            Console.WriteLine("2. Zmień status życia");
            Console.Write("Wybierz opcję: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    dog.IsVaccinated = !dog.IsVaccinated; //odwraca wartości true/false
                    Console.WriteLine("Zmieniono status szczepienia.");
                    break;

                case "2":
                    dog.IsAlive = !dog.IsAlive;
                    Console.WriteLine("Zmieniono status życia.");
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
            
            DogDataService.SaveDogs(dogs); //po każdej edycji jest zapis do json
        }
        else
        {
            Console.WriteLine("Nie ma psa o takim indeksie.");
        }
    }
    else
    {
        Console.WriteLine("Niepoprawny numer.");
    }
}
//========================================================================= Menu 4
static void AddDog(List<Dog> dogs) //metoda dodaje nowego psa do listy
{
    Console.Write("Podaj imię psa: ");
    string name = Console.ReadLine();

    Console.Write("Podaj rasę psa: ");
    string breed = Console.ReadLine();

    Console.Write("Podaj płeć (M/F): ");
    string sexInput = Console.ReadLine();

    Dog newDog; //twoorzymy nowy obiekt na podstawie płci
    if (sexInput.ToUpper() == "M")
    {
        newDog = new Male();
    }
    else
    {
        newDog = new Female();
    }

    newDog.Name = name;
    newDog.Breed = breed;

    Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
    DateTime.TryParse(Console.ReadLine(), out DateTime dob);
    newDog.DateOfBirth = dob;

    Console.Write("Podaj imię matki: ");
    newDog.MotherName = Console.ReadLine();

    Console.Write("Podaj imię ojca: ");
    newDog.FatherName = Console.ReadLine();

    newDog.IsVaccinated = true;
    newDog.IsAlive = true;

    dogs.Add(newDog); //dodaje do listy

    DogDataService.SaveDogs(dogs); //zapis do json

    Console.WriteLine("Dodano nowego psa.");
}

static void PauseAndClear() //metoda zatrzymuje ekran i czysci konsole
{
    Console.WriteLine();
    Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
    Console.ReadKey();
    Console.Clear();
}

}
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
namespace DogBreedingManager;

public class Female : Dog //dziedziczenie - po klasie Dog
{
    public Female()  //polimorfizm - nadpisujemy Sex z klasy bazowej Dog
    {
        Sex = "Female"; 
    }
}
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
namespace DogBreedingManager;

public class Male : Dog //dziedziczenie - klasa Male dziedziczy po Dog
{
    public Male() // polimorfizm - nadpisujemy właściwość Sex z klasy bazowej Dog
    {
        Sex = "Male";
    }
}

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
using System;
using System.Collections.Generic; //listy
using System.IO; //odczyt zapis pliku
using System.Text.Json; //praca z json

namespace DogBreedingManager;

public static class DogDataService //odczyt i zapis danych JSON
{
    private static readonly string filePath = "dogs.json";
    
    public static List<Dog> LoadDogs() //wczytywanie danych
    {
        if (!File.Exists(filePath))
        {
            return new List<Dog>(); //jeśli nie ma pliku to zwraca pustą listę
        }
        
        string json = File.ReadAllText(filePath); //zczytujemy zawartość pliku do stringa

        //zamienia JSON na listę obiektów Dog
        //w razie niepowodzenia zwraca pustą listę
        return JsonSerializer.Deserialize<List<Dog>>(json) ?? new List<Dog>();
    }
    
    public static void SaveDogs(List<Dog> dogs) //metoda zapisuje listę psów do pliku json
    {
        var options = new JsonSerializerOptions //opcja zapisu w formacie human friendly
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(dogs, options); //zamiana listy na json

        File.WriteAllText(filePath, json); //zapis do pliku
    }
}
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
using System;
namespace DogBreedingManager;

public class Dog //klasa bazowa
{
    
    private DateTime dateOfBirth; // hermetyzacja danych
    private string sex;
    
    public string Sex
    {
        get { return sex; }
        protected set { sex = value; } //chroniony setter
    }
    
    public string Name { get; set; } //podstawowe dane o obiektach
    public string Breed { get; set; }
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
    public bool IsVaccinated { get; set; } //czy pies jest szczepiony
    
    public bool IsAlive { get; set; } = true; //czy pies żyje

    public string MotherName { get; set; } = "Brak informacji"; //imiona rodziców
    
    public string FatherName { get; set; } = "Brak informacji";
    
    public void DisplayInfo()  //metoda wyświetlająca wszystkie informacje o psie
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
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
*/