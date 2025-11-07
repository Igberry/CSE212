/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    // We'll use a real FIFO queue behavior here
    private readonly List<Person> _people = new();

    public int Length => _people.Count;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Add(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left.  Note that a turns value of 0 or less means the 
    /// person has an infinite number of turns.  An error exception is thrown 
    /// if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.Count == 0)
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        // FIFO: take first person
        var person = _people[0];
        _people.RemoveAt(0);

        // Return a copy so we don’t modify the test’s reference
        var result = new Person(person.Name, person.Turns);

        if (person.Turns <= 0)
        {
            // Infinite turns, stays forever
            _people.Add(person);
        }
        else if (person.Turns > 1)
        {
            // Decrement remaining turns and re-add
            person.Turns -= 1;
            _people.Add(person);
        }
        // If turns == 1, do not re-add (they’re done)

        return result;
    }

    public override string ToString()
    {
        return string.Join(", ", _people.Select(p => $"{p.Name}({p.Turns})"));
    }
}
