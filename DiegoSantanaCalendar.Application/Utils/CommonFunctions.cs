using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.Utils
{
    public class CommonFunctions
    {

        public static string BuildFirstNameAndLastNameToUsername(
            string firstName,
            string lastName,
            Func<string, bool> isUsernameTaken)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Nome e sobrenome são obrigatórios.");

            string baseUsername = $"{firstName.Trim().ToLower()}_{lastName.Trim().ToLower()}";
            string username = baseUsername;
            int suffix = 1;

            while (isUsernameTaken(username))
            {
                username = $"{baseUsername}{suffix}";
                suffix++;
            }

            return username;
        }

        public static bool BeAValidCpfLengthAndFormat(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos (pontos e traços)
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verifica se tem 11 dígitos e se são todos numéricos
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            // Impede CPFs com todos os dígitos iguais, ex: 111.111.111-11
            if (cpf.All(c => c == cpf.First())) return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}