using System;
using System.Security.Cryptography;

namespace Cosmos.Application.Utilities;

public static class PasswordEncrypter
{
    private static readonly int SaltSize = 8;
    private static readonly int HashSize = 16;
    private static readonly int Iterations  = 100000;

    private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;

    public static string GenerateHash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, AlgorithmName, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public static bool ValidateHash(string password, string passwordHash)
    {
        string[] hashParts = passwordHash.Split("-");
        byte[] hash = Convert.FromHexString(hashParts[0]);
        byte[] getSalt = Convert.FromHexString(hashParts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, getSalt, Iterations, AlgorithmName, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash); //make comparison based on arrays sizes and not the time passed
    }
}
