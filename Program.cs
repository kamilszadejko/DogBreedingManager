using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("5. Wyszukaj psa po imieniu");
            Console.WriteLine("0. Zakończ program");
            Console.WriteLine("================================");
            Console.Write("Wybierz opcję: ");
            
            string choice = Console.ReadLine();
            Console.Clear(); //czyści menu po wyborze opcji
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
                
                case "5":
                    Console.WriteLine("Wybrano: Wyszukaj psa po imieniu");
                    SearchDogByName(dogs);
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

//========================================================================= Menu 5

static void SearchDogByName(List<Dog> dogs)
{
    Console.Write("Podaj imię psa: ");
    string name = Console.ReadLine();

    var results = dogs
        .Where(d => d.Name.ToLower() == name.ToLower()) //tworzy zapytanie i ignoruje wielkość liter
        .ToList();

    if (results.Any())
    {
        foreach (var dog in results)
        {
            dog.DisplayInfo();
        }
    }
    else
    {
        Console.WriteLine("Nie znaleziono psa o takim imieniu.");
    }
}

//========================================================================= metoda czyszcząca ekran

static void PauseAndClear() //metoda zatrzymuje ekran i czysci konsole
{
    Console.WriteLine();
    Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...");
    Console.ReadKey();
    Console.Clear();
}

}
