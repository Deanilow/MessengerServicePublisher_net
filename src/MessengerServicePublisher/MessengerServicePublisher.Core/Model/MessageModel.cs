namespace MessengerServicePublisher.Core.Model
{
    public class MessageModel
    {
        public string to { get; set; }
        public string from { get; set; }
        public MessagesDetailModel messages { get; set; }
    }
    public class MessagesDetailModel
    {
        public string text { get; set; }
        public string fileUrl { get; set; }
    }

    public class Data
    {
        public MessageModel message { get; set; }
    }
}
