namespace Entity_Relations_Advanced_.Models
{
    public class License
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Resource Resource { get; set; }
    }
}
