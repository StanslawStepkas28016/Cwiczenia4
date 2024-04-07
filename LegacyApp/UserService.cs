using System;

/*
 * UI - html, console
 * BL - logika biznesowa
 * Infrastructure - I/O
 */

namespace LegacyApp
{
    /** Walidacja przy sprawdzaniu użytkownika jest zrobiona w bardzo chaotyczny sposób (pomieszana z logiką biznesową),
     * rozdzielę ją na elementy. */
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!UserValidator.InputValidation(firstName, lastName, email))
                return false; // Walidacja sprawdzająca, czy mail jest mailem.

            if (!UserValidator.AgeValidation(dateOfBirth))
                return false; // Walidacja (pomieszana z logiką biznesową), czy klient ma więcej niż 21 lat.

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

            if (!UserValidator.ClientStatusValidation(client, user))
                return false; // Walidacja (pomieszana z logiką biznesową), sprawdzanie po statusie.

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}