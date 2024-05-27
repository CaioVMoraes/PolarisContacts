using PolarisContacts.Domain;
using System.Text.RegularExpressions;

namespace PolarisContacts.CrossCutting.Helpers
{
    public static class Validacoes
    {
        public static bool IsValidTelefone(string telefone)
        {
            var regex = new Regex(@"^\d{4}-\d{4}$");
            return regex.IsMatch(telefone);
        }

        public static bool IsValidCelular(string celular)
        {
            var regex = new Regex(@"^\d{5}-\d{4}$");
            return regex.IsMatch(celular);
        }

        public static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public static bool IsValidEndereco(Endereco endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco.CEP))
                return false;

            if (!Regex.IsMatch(endereco.CEP, @"^\d{5}-\d{3}$"))
                return false;

            if (string.IsNullOrEmpty(endereco.Logradouro))
                return false;

            if (string.IsNullOrEmpty(endereco.Cidade))
                return false;

            if (string.IsNullOrEmpty(endereco.Estado))
                return false;

            return true;
        }
    }
}