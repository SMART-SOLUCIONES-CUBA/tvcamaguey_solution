using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    [Route("api/[controller]")]
    public class SecurityController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private const string TOKEN_PROVIDER = "Phone";

        private readonly SignInManager<Persistence.Entities.TerminalUser> signInManager;
        private readonly IOptions<Options.JwtOptions> jwtOptions;
        private readonly IOptions<Options.MailSendOptions> mailSendOptions;
        private readonly IEntityRepository<PersonIdentity, System.Guid> personIdentityRepository;

        public SecurityController(IEntityRepository<PersonIdentity, System.Guid> personIdentityRepository, IOptions<Options.MailSendOptions> mailSendOptions, IOptions<Options.JwtOptions> jwtOptions, SignInManager<Persistence.Entities.TerminalUser> signInManager, Microsoft.Extensions.Localization.IStringLocalizer<SecurityController> localizer, ILogger<SecurityController> logger) : base(localizer, logger)
        {
            this.signInManager = signInManager;
            this.jwtOptions = jwtOptions;
            this.mailSendOptions = mailSendOptions;
            this.personIdentityRepository = personIdentityRepository;
        }

        [HttpPost("authenticate")]
        public async System.Threading.Tasks.Task<IActionResult> AuthenticateAsync([FromBody]AuthenticationRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await signInManager.UserManager.FindByNameAsync(model.Login);

                if (await signInManager.UserManager.VerifyUserTokenAsync(user, TOKEN_PROVIDER, model.Purpose, model.Token))
                {
                    await signInManager.SignInAsync(user, true);

                    var token = GenerateJwtToken(user);

                    var identity = new PersonIdentityDisplayModel();

                    identity.Import(personIdentityRepository.Get(user.IdentityId));

                    return Json(new AuthenticationResponseModel
                    {
                        Token = token,
                        Type = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
                        Identity = identity
                    });
                }
            }

            return BadRequest();
        }

        [HttpPost("register")]
        public async System.Threading.Tasks.Task<IActionResult> RegisterAsync([FromBody]RegistrationRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await signInManager.UserManager.FindByNameAsync(model.Login);

                if (user == null)
                {
                    user = new Persistence.Entities.TerminalUser { UserName = model.Login, Email = model.Login };

                    user.Identity.Name = model.Login;

                    var createResult = await signInManager.UserManager.CreateAsync(user);

                    if (!createResult.Succeeded) throw new System.Exception();
                }

                try
                {
                    var purpose = await signInManager.UserManager.GenerateChangePhoneNumberTokenAsync(user, model.Login);

                    var token = await signInManager.UserManager.GenerateUserTokenAsync(user, TOKEN_PROVIDER, purpose);

                    using (var smtpClient = new System.Net.Mail.SmtpClient(mailSendOptions.Value.Host, mailSendOptions.Value.Port))
                    {
                        smtpClient.EnableSsl = mailSendOptions.Value.UseSSL;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential(mailSendOptions.Value.User, mailSendOptions.Value.Password);

                        var message = new System.Net.Mail.MailMessage
                        {
                            IsBodyHtml = true,
                            Subject = string.Format(mailSendOptions.Value.SubjectTemplate, model.Login),
                            Body = string.Format(mailSendOptions.Value.BodyTemplate, model.Login, token)
                        };

                        message.From = new System.Net.Mail.MailAddress(mailSendOptions.Value.User);

                        message.To.Add(model.Login);

                        await smtpClient.SendMailAsync(message);
                    }

                    //logger.LogInformation("#*# Sended token by Email: {0}", token);

                    return Json(purpose);
                }
                catch (System.Exception exception)
                {
                    throw exception;
                }
            }

            return BadRequest();
        }

        private string GenerateJwtToken(Persistence.Entities.TerminalUser user)
        {
            var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, System.Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                jwtOptions.Value.Issuer,
                jwtOptions.Value.Issuer,
                claims,
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
