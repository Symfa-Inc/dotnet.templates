namespace WebApiTemplate.Application.Error.Models
{
    public class Error
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public Error() { }

        public Error(int Code, string Description, string Value)
        {
            this.Code = Code;
            this.Description = Description;
            this.Value = Value;
        }
    }
}
