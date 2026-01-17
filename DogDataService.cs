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