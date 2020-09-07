using Microsoft.Extensions.Logging;
using MimeKit;
using SmartSolucionesCuba.SAPRESSC.Core.Api.Connect.Helpers;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Workers
{
    public class MailingWorker : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly ILogger<MailingWorker> logger;
        private readonly Microsoft.Extensions.Options.IOptions<Options.PerformanceRegulationOptions> performanceRegulationOptions;
        private readonly Microsoft.Extensions.Options.IOptions<Options.SmtpOptions> smtpOptions;

        public MailingWorker(Microsoft.Extensions.Options.IOptions<Options.SmtpOptions> smtpOptions, Microsoft.Extensions.Options.IOptions<Options.PerformanceRegulationOptions> performanceRegulationOptions, ILogger<MailingWorker> logger)
        {
            this.smtpOptions = smtpOptions;
            this.performanceRegulationOptions = performanceRegulationOptions;
            this.logger = logger;
        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(System.Threading.CancellationToken stoppingToken)
        {
            await System.Threading.Tasks.Task.Delay(performanceRegulationOptions.Value.IterationDesiredElapseTime);

            while (!stoppingToken.IsCancellationRequested)
            {
                #region Start Iteration Trace

                var startedAt = System.DateTime.Now;

                logger.LogDebug("Request work iteration started at {0}", startedAt);

                #endregion
                try
                {
                    var httpClientHelperProvider = new HttpClientHelperProvider();

                    var ordersApiClient = new Clients.UserVerificationMailSendOrdersApiClient(httpClientHelperProvider);

                    var ordersPaginationModel = await ordersApiClient.GetPaginateAsync(1, performanceRegulationOptions.Value.IterationRequestCount);
                    
                    using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        //accept all SSL certificates
                        smtpClient.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                        await smtpClient.ConnectAsync(smtpOptions.Value.Host, smtpOptions.Value.Port, smtpOptions.Value.Security);

                        await smtpClient.AuthenticateAsync(smtpOptions.Value.User, smtpOptions.Value.Password);

                        foreach (var order in ordersPaginationModel.List)
                        {
                            var messageBodyBuilder = new BodyBuilder();

                            messageBodyBuilder.HtmlBody = string.Format(smtpOptions.Value.BodyTemplate, order.UserId, order.Token);

                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress("TuTV Camaguey", smtpOptions.Value.User));
                            message.To.Add(new MailboxAddress(order.UserId, order.UserId));

                            message.Subject = string.Format(smtpOptions.Value.SubjectTemplate, order.UserId);
                            message.Body = messageBodyBuilder.ToMessageBody();

                            await smtpClient.SendAsync(message);

                            logger.LogDebug("Removing Order");

                            await ordersApiClient.Delete(order.Id);
                        }                        

                        await smtpClient.DisconnectAsync(true);
                    }
                }
                catch (SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException ex)
                {
                    logger.LogError(ex, "Cloud Api Mailing Handling Error! ({0})", ex.HttpStatusCode);
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, "Unexpected Mailing Handling Error!");
                }

                #region End Iteration Trace

                var endedAt = System.DateTime.Now;

                var elpaseTime = endedAt - startedAt;

                logger.LogDebug("Request work iteration ended at {0}", endedAt);

                var delayTime = performanceRegulationOptions.Value.IterationDesiredElapseTime - elpaseTime;

                if (delayTime < System.TimeSpan.Zero) continue;

                logger.LogDebug("Request work iteration weiting {0}", delayTime);

                await System.Threading.Tasks.Task.Delay(delayTime);

                #endregion
            }
        }

        private sealed class HttpClientHelperProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
        {
            public HttpClientHelper HttpClientHelper => new HttpClientHelper("http://localhost/api/", null, null);
        }
    }
}
