using System;

namespace Cosmos.Application.Utilities;

public static class ValidateEmailCPF
{
    public static bool ValidateCPF(string CPF)
    {
        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		string tempCpf;
		string digit;
		int sum;
		int rest;

		string cleanCpf = new string(CPF.Trim().Where(char.IsDigit).ToArray());

		if (cleanCpf.Length != 11 || cleanCpf.Distinct().Count() == 1)
            return false;

		tempCpf = cleanCpf.Substring(0, 9);
		sum = 0;

		for(int i=0; i<9; i++)
		    sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

		rest = sum % 11;
		if ( rest < 2 )
		    rest = 0;
		else
		   rest = 11 - rest;

		digit = rest.ToString();

		tempCpf = tempCpf + digit;

		sum = 0;
		for(int i=0; i<10; i++)
		    sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

		rest = sum % 11;
		if (rest < 2)
		   rest = 0;
		else
		   rest = 11 - rest;

		digit = digit + rest.ToString();

		return cleanCpf.EndsWith(digit);
    }


    public static bool ValidateEmail(string Email)
    {
        string emailMatch = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

        return System.Text.RegularExpressions.Regex.IsMatch(Email, emailMatch);
    }
}
