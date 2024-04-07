using System;

/*
 * UI - html, console
 * BL - logika biznesowa
 * Infrastructure - I/O
 */

namespace LegacyApp
{
    /* Walidacja przy sprawdzaniu użytkownika jest zrobiona w bardzo chaotyczny sposób (pomieszana z logiką biznesową),
     * rozdzielę ją na elementy. */
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!InputValidation(firstName, lastName, email)) return false; // Walidacja sprawdzająca, czy mail jest mailem.

            if (!AgeValidation(dateOfBirth)) return false; // Walidacja (pomieszana z logiką biznesową), czy klient ma więcej niż 21 lat.

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (!ClientStatusValidation(client, user)) return false; // Walidacja (pomieszana z logiką biznesową), sprawdzanie po statusie.

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool ClientStatusValidation(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private static bool InputValidation(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private static bool AgeValidation(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}