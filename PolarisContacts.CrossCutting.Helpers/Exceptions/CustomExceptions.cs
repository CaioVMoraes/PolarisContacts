namespace PolarisContacts.CrossCutting.Helpers.Exceptions
{
    public static class CustomExceptions
    {
        public class PessoaNotFoundException() : Exception(message: $"Pessoa não identificada!");
    }
}
