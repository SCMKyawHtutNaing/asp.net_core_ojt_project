using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services
{
	public class MessageService : IEmailSender
	{
		string sendGridKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

		public async Task SendEmailAsync(string toEmail, string subject, string message)
		{
			if (string.IsNullOrEmpty(sendGridKey))
			{
				throw new Exception("Null SendGridKey");
			}
			await Execute(sendGridKey, subject, message, toEmail);
		}

		public async Task Execute(string apiKey, string subject, string message, string toEmail)
		{
			var client = new SendGridClient(apiKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("scm.kyawhtutnaing@gmail.com", "Password Recovery"),
				Subject = subject,
				PlainTextContent = message,
				HtmlContent = message
			};
			msg.AddTo(new EmailAddress(toEmail));

			// Disable click tracking.
			// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
			msg.SetClickTracking(false, false);
			var response = await client.SendEmailAsync(msg);
		}
	}
}
