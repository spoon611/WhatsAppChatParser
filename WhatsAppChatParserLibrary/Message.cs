﻿using System;

namespace WhatsAppChatParserLibrary
{
    /// <summary>
    /// An individual WhatsApp message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Date and Time when the message was sent
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The contact name of the person who sent the message
        /// </summary>
        public string MessageBy { get; set; }

        /// <summary>
        /// The message text
        /// </summary>
        public string Text { get; set; }

        internal static Message Parse(string chatLine)
        {
            var message = new Message();
            if(chatLine.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries).Length >= 2)
            {
                var dateTimeString = chatLine.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                var chatString = chatLine.Replace(dateTimeString, string.Empty).Trim().Trim('-');

                message.TimeStamp = GetMessageTimeStamp(dateTimeString);
                message.MessageBy = GetMessageBy(chatString)?.Trim();
                message.Text = GetMessageText(chatString, message.MessageBy)?.Trim();
            }
            else
                message.Text = chatLine.Trim();
            return message;
        }

        private static string GetMessageText(string chatString, string messageBy)
        {
            string messageText = null;

            if(!string.IsNullOrEmpty(chatString))
            {
                if (string.IsNullOrEmpty(messageBy))
                    messageText = chatString;
                else
                    messageText = chatString.Replace(messageBy, string.Empty).Trim().Trim(':');
            }

            return messageText;
        }

        private static string GetMessageBy(string chatString)
        {
            string messageBy = null;

            if(!string.IsNullOrEmpty(chatString) && chatString.Split(':').Length >= 2)
            {
                messageBy = chatString.Split(':')[0].Trim();
            }

            return messageBy;
        }

        private static DateTime GetMessageTimeStamp(string dateTimeString)
        {
            var timeStamp = default(DateTime);

            if(!string.IsNullOrEmpty(dateTimeString))
            {
                timeStamp = DateTime.Parse(dateTimeString);
            }

            return timeStamp;
        }
    }
}
