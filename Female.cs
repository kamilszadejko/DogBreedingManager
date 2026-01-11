namespace DogBreedingManager;

public class Female : Dog //dziedziczenie - po klasie Dog
{
    public Female()  //polimorfizm - nadpisujemy Sex z klasy bazowej Dog
    {
        Sex = "Female"; 
    }
}