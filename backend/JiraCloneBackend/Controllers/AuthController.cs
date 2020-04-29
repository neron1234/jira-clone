using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JiraCloneBackend.Data;
using JiraCloneBackend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace JiraCloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        string secretStr = "J6k2eVCTXDp5b97u6gNH5GaaqHDxCmzz2wv3PRPFRsuW2UavK8LGPRauC4VSeaetKTMtVmVzAC8fh8Psvp8PFybEvpYnULHfRpM8TA2an7GFehrLLvawVJdSRqh2unCnWehhh2SJMMg5bktRRapA8EGSgQUV8TCafqdSEHNWnGXTjjsMEjUpaxcADDNZLSYPMyPSfp6qe5LMcd5S9bXH97KeeMGyZTS2U8gp3LGk2kH4J4F3fsytfpe9H9qKwgjb";

        private readonly JiraContext _context;

        public AuthController(JiraContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult<String> PostLogin(LoginHelper l)
        {
            var user = _context.Users
                .Single(u => u.Username == l.Username);
                
            if (user.comparePassword(l.Password))
            {
                var header = new
                {
                    alg = "HS256",
                    typ = "JWT"
                };
                var payload = new
                {
                    userId = user.UserId,
                    name = "Tyler Simmons",
                    iss = "https://localhost:44387"
                };

                //start with json strings
                string headerJsonString = JsonConvert.SerializeObject(header);
                string payloadJsonString = JsonConvert.SerializeObject(payload);
                string hAndP = headerJsonString + '.' + payloadJsonString;
                
                byte[] hAndPByte = Encoding.Default.GetBytes(hAndP);
                string headerAndPayloadB64Url = WebEncoders.Base64UrlEncode(hAndPByte);

                //convert concatenated to byte array
                byte[] headerAndPayloadBytes = WebEncoders.Base64UrlDecode(headerAndPayloadB64Url);

                //Convert secret to bytes
                var secretBytes = Encoding.Default.GetBytes(secretStr);

                //Hash out the sig
                KeyedHashAlgorithm alg = new HMACSHA256(secretBytes);
                byte[] hashBytes = alg.ComputeHash(headerAndPayloadBytes);

                //Convert sig base64url
                string sigBase64Url = WebEncoders.Base64UrlEncode(hashBytes);

                //put together jwt
                string finishedToken = WebEncoders.Base64UrlEncode(Encoding.Default.GetBytes(headerJsonString)) + '.' +
                  WebEncoders.Base64UrlEncode(Encoding.Default.GetBytes(payloadJsonString)) + '.' + sigBase64Url;
                

                return "Successful authentication";
                
            } else
            {
                return "Failed authentication";
            }
        }
    }
}