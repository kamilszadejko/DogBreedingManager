namespace DogBreedingManager;

public class Male : Dog //dziedziczenie - klasa Male dziedziczy po Dog
{
    public Male() // polimorfizm - nadpisujemy właściwość Sex z klasy bazowej Dog
    {
        Sex = "Male";
    }
}
