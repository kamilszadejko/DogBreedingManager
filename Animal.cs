namespace DogBreedingManager;

public abstract class Animal  //klasa abstrakcyjna
{
    private DateTime _dateOfBirth;

    public string Name { get; set; }

    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set
        {
            //zabezpieczenie przed wpisaniem daty z przyszłości
            if (value <= DateTime.Now)
            {
                _dateOfBirth = value;
            }
        }
    }

    public bool IsAlive { get; set; } = true;

    //metoda abstrakcyjna – każda klasa pochodna będzie ją mieć
    public abstract void DisplayInfo();
}