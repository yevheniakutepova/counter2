namespace Counter.Models
{
    public class CounterModel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public CounterModel(string name)
        {
            Name = name;
            Value = 0;
        }

        public void Increment() => Value++;
        public void Decrement() => Value--;
    }
}
