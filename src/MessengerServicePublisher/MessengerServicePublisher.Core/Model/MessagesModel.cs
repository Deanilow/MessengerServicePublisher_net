namespace MessengerServicePublisher.Core.Model
{
    public class MessagesModel
    {
        public Guid id { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public bool isCall { get; set; } = false;
        public List<MessagesDetailModel> messages { get; set; }
    }
    public class MessagesDetailModel
    {
        public string text { get; set; }
        public string fileUrl { get; set; }
        public int order { get; set; }
    }

    public class Data
    {
        public MessagesModel data { get; set; }
    }
}
