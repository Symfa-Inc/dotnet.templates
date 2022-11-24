namespace WebApiTemplate.Domain.Errors
{
    public class ErrorResponseItem
    {
        public string Item { get; set; }
        public string Value { get; set; }

        public ErrorResponseItem(string item, string value)
        {
            Item = item;
            Value = value;
        }
    }
}
