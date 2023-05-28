namespace AspDotNetLab2
{
    public class RandomService
    {
        private readonly Random _random;

        public RandomService()
        {
            _random = new Random();
        }

        public int GetRandomNumber()
        {
            return _random.Next();
        }
    }
}
