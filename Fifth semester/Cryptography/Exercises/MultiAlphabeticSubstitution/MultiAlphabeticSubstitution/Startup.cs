﻿namespace MultiAlphabeticSubstitution
{
    using System;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        // This program is an algorithm for multi alphabetic substitution - example 2.4 - page 19 from the guide
        public static void Main()
        {
            var bulgarianAlphabet = Enumerable.Range('А', 'Я' - 'А' + 1)
                .Select(i => ((char)i).ToString())
                .Where(i => i != "Ы" && i != "Э")
                .ToArray();
            string[] specialSymbols = { " " };

            var allowedSymbols = string.Join("", bulgarianAlphabet) + "" + string.Join("", specialSymbols);

            Console.Write("Write input text for encryption: ");
            var inputText = Console.ReadLine();

            Console.Write("Write crypto key: ");
            var cryptoKey = Console.ReadLine();

            try
            {
                CheckForValidCryptoKey(allowedSymbols, cryptoKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (inputText.Length > 300)
            {
                Console.WriteLine("The input text should not be more than 300 symbols.");
                return;
            }

            var encryptedText = new StringBuilder();
            var decryptedText = new StringBuilder();

            try
            {
                encryptedText = EncryptMessage(allowedSymbols, inputText, cryptoKey, encryptedText);
                Console.WriteLine($"Encrypted text: {encryptedText}");

                decryptedText = DecryptMessage(allowedSymbols, cryptoKey, encryptedText, decryptedText);
                Console.WriteLine($"Decrypted text: {decryptedText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static StringBuilder EncryptMessage(string allowedSymbols, string inputText, string cryptoKey, StringBuilder encryptedText)
        {
            for (int i = 0; i < inputText.Length; i++)
            {
                var doesItExist = allowedSymbols.Any(x => x == inputText[i]);

                if (doesItExist)
                {
                    var currentLetter = allowedSymbols.First(x => x == inputText[i]);
                    var currentLetterIndex = allowedSymbols.IndexOf(currentLetter) + 1;

                    var cryptoKeyIndex = CheckIfKeyIsLooped(allowedSymbols, cryptoKey, i);

                    var encryptedIndex = cryptoKeyIndex + currentLetterIndex;

                    if (encryptedIndex > allowedSymbols.Length)
                    {
                        encryptedIndex -= allowedSymbols.Length;
                    }

                    encryptedText.Append(allowedSymbols[encryptedIndex - 1].ToString());
                }
                else
                {
                    throw new ArgumentException($"The letter {inputText[i]} in the input text does not exist in the allowed symbols.");
                }
            }

            return encryptedText;
        }

        private static StringBuilder DecryptMessage(string allowedSymbols, string cryptoKey, StringBuilder encryptedText, StringBuilder decryptedText)
        {
            for (int i = 0; i < encryptedText.Length; i++)
            {
                var currentLetterIndex = allowedSymbols.IndexOf(encryptedText[i]) + 1;

                var cryptoKeyIndex = CheckIfKeyIsLooped(allowedSymbols, cryptoKey, i);

                var decryptedIndex = currentLetterIndex - cryptoKeyIndex;

                if (decryptedIndex <= 0)
                {
                    decryptedIndex = allowedSymbols.Length - Math.Abs(decryptedIndex);
                }

                decryptedText.Append(allowedSymbols[decryptedIndex - 1].ToString());
            }

            return decryptedText;
        }

        private static void CheckForValidCryptoKey(string allowedSymbols, string cryptoKey)
        {
            for (int i = 0; i < cryptoKey.Length; i++)
            {
                var isCryptoKeyValid = allowedSymbols.Contains(cryptoKey[i]);

                if (!isCryptoKeyValid)
                {
                    throw new ArgumentException("Crypto key is not valid. It should consist letters from allowed ones.");
                }
            }
        }

        private static int CheckIfKeyIsLooped(string allowedSymbols, string cryptoKey, int i)
        {
            var cryptoKeyIndex = 0;

            if (i >= cryptoKey.Length)
            {
                cryptoKeyIndex = allowedSymbols.IndexOf(cryptoKey[i % cryptoKey.Length]) + 1;
            }
            else
            {
                cryptoKeyIndex = allowedSymbols.IndexOf(cryptoKey[i]) + 1;
            }

            return cryptoKeyIndex;
        }
    }
}
