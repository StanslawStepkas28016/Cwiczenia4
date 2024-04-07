using System;

namespace LegacyApp;

/** Klasa dodana, służąca do walidacji klienta (oddzielenie logiki biznesowej od działania samego AddUser. */
public class UserValidator
{
    public static bool ClientStatusValidation(Client client, User user)
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
                creditLimit *= 2;
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

    public static bool InputValidation(string firstName, string lastName, string email)
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

    public static bool AgeValidation(DateTime dateOfBirth)
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