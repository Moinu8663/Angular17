using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Signup_and_signin.Exception;
using Signup_and_signin.Model;
using Signup_and_signin.Repository;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Signup_and_signin.Services
{
    public class Service:Iservice
    {
        private readonly IRepo repo;
        public Service(IRepo repo)
        {
            this.repo = repo;
        }

        public User LoginUser(User user)
        {
            if(user != null)
            {
                return repo.LoginUser(user);
            }
            else
            {
                throw new UserNotFoundException("User Not Found");
            }
            
        }

        public void RegisterUser(User user)
        {
            if(user != null) {
                repo.RegisterUser(user);
            }
            else {
                throw new UserAlreadyExistsException("User already exists");
            } 
        }

        public User GetUserByMobileNo(string Mobile_No)
        {
            if (Mobile_No != null)
            {
               return repo.GetUserByMobileNo(Mobile_No);
                
            }
            else
            {
                throw new UserNotFoundException("User Not Found");
            }
        }

        public List<User> Getall()
        {
            return repo.Getall();
        }

        public void UpdateUser(string Mobile_No, User user)
        {
            var us = GetUserByMobileNo(Mobile_No);
            if (us == null)
            {
                throw new UserNotFoundException($"user with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.UpdateUser(Mobile_No,user);
            }
        }

        public void DeleteUser(string Mobile_No)
        {
            var us = GetUserByMobileNo(Mobile_No);
            if(us == null)
            {
                throw new UserNotFoundException($"user with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.DeleteUser(Mobile_No);
            }
        }
    }
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration configuration;
        public string GenerateToken(User user)
        {
            //1. Create the claims
            var claims = new[] 
            {
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Role,"Admin"),
            }; //payload

            //2. Create ur secret key, and also the Hashing Algorithm (Signing Credentials)
            var secret = "Moinuddinshaikhmainproject";
            var key = Encoding.UTF8.GetBytes(secret);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            //3. Create the token
            var token = new JwtSecurityToken(
                issuer: "authapiMoinuddin",
                audience: "userapi",
                claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(30)
                );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
