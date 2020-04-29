using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace JiraCloneBackend.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public byte[] NewPasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime UserCreated { get; set; }
        public List<Project> OwnedProjects { get; set; }
        public List<Issue> ReportedIssues { get; set; }

        
        public void generateSaltAndPassword(String passwordString)
        {
            //Generate salt and save
            this.NewPasswordSalt = generateNewSalt();
            //Generate hash and save
            this.PasswordHash = hashPassword(passwordString);
        }

        //return byte array for salt in encryption alg
        private byte[] generateNewSalt()
        {
            //128-bit salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string hashPassword(string passwordString)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordString,
                salt: this.NewPasswordSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedPassword;
        }

        //return true if good password
        public bool comparePassword(String passwordInput)
        {
            string hashToCompare = hashPassword(passwordInput);
            if (hashToCompare.Equals(this.PasswordHash))
            {
                return true;
            }
            return false;
        }
    }
}
