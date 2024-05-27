namespace PolarisContacts.CrossCutting.Helpers.Exceptions
{
    public static class CustomExceptions
    {
        public class InvalidIdException() : Exception(message: $"O ID utilizado é inválido!");

        public class ContatoNotFoundException() : Exception(message: $"Contato não identificado!");

        public class EnderecoNotFoundException() : Exception(message: $"Endereço não identificado!");

        public class CelularNotFoundException() : Exception(message: $"Celular não identificado!");

        public class TelefoneNotFoundException() : Exception(message: $"Telefone não identificado!");

        public class EmailNotFoundException() : Exception(message: $"E-mail não identificado!");

        public class UsuarioNotFoundException() : Exception(message: $"Usuário não identificado!");

        public class RegiaoNotFoundException() : Exception(message: $"Região não identificada!");

        public class LoginVazioException() : Exception(message: $"Por favor, preencha o campo \"Login\"!");
        public class SenhaVaziaException() : Exception(message: $"Por favor, preencha o campo \"Senha\"!");

        public class SenhaIncorretaException() : Exception(message: $"A senha é incorreta!");

        public class NomeObrigatorioException() : Exception(message: $"O nome do contato é obrigatório!");

        public class TelefoneInvalidoException() : Exception(message: $"O telefone não é válido!");

        public class CelularInvalidoException() : Exception(message: $"O celular não é válido!");

        public class EmailInvalidoException() : Exception(message: $"O e-mail não é válido!");

        public class EnderecoInvalidoException() : Exception(message: $"O endereço não é válido!");
    }
}
