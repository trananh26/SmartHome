using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using static Twilio.Rest.Api.V2010.Account.MessageResource;
using Twilio.Clients;
using System.Windows;

namespace FireAlert
{
    public class clsAlert
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// gửi cảnh báo ra tin nhắn và mạng xã hội
        /// </summary>
        /// <param name="mess"></param>
        public void SendAlert(string mess)
        {
            try
            {
                ///send alert to telegram
                SendTelegramAlert(mess);
                SendSMSAlert(mess);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        /// <summary>
        /// gửi alert qua sms
        /// </summary>
        /// <param name="mess"></param>
        private void SendSMSAlert(string mess)
        {
            // Thông tin Twilio Account SID và Auth Token
            const string accountSid = "ACf776adac527a68541586aba65fad0f87";
            const string authToken = "450c76b7833f75be737ae80077c09274";

            // Khởi tạo Twilio Client
            TwilioClient.Init(accountSid, authToken);

            // Số điện thoại người nhận (Recipient Phone Number)
            var toPhoneNumber = new PhoneNumber("+84962174807");// "+84778333572");

            // Số điện thoại Twilio của bạn (Twilio Phone Number)
            var fromPhoneNumber = new PhoneNumber("+12296004039");

            // Nội dung tin nhắn
            var messageBody = mess;

            // Gửi tin nhắn
            var message = MessageResource.Create(
                body: messageBody,
                from: fromPhoneNumber,
                to: toPhoneNumber
            );
        }

        /// <summary>
        /// gửi alert qua Telegram
        /// </summary>
        /// <param name="mess"></param>
        private void SendTelegramAlert(string mess)
        {
            var botToken = "7392260020:AAEhopg8eft7d371AgvxXZimb6mbY44k2GI";
            var chatId = "5132467178";
            var message = mess;

            var url = $"https://api.telegram.org/bot{botToken}/sendMessage";

            var postData = new
            {
                chat_id = chatId,
                text = message
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(postData), System.Text.Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content);
        }
    }
}
