namespace PolarisContacts.CrossCutting.Helpers.Exceptions
{
    public static class CustomExceptions
    {
        public class InvalidIdException() : Exception(message: $"O ID utilizado é inválido!");

        public class ContatoNotFoundException() : Exception(message: $"Contato não identificada!");

        public class EnderecoNotFoundException() : Exception(message: $"Endereço não identificado!");

        public class CelularNotFoundException() : Exception(message: $"Celular não identificado!");

        public class TelefoneNotFoundException() : Exception(message: $"Telefone não identificado!");

        public class EmailNotFoundException() : Exception(message: $"E-mail não identificado!");

        public class UsuarioNotFoundException() : Exception(message: $"Usuário não identificado!");

        public class RegiaoNotFoundException() : Exception(message: $"Região não identificada!");
    }
}
