using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly LazadaDBContext _context;

        public ChatRepository(LazadaDBContext context)
        {
            _context = context;
        }

        public Conversation_US GetConversation_ById(long userid, long shopid)
        {
            Conversation_US response = new Conversation_US();
            List<Message_US> respone_message = new List<Message_US>();
            User user = _context.Users.SingleOrDefault(s => s.Id == userid);
            if(user == null)
            {
                return response;
            }
            Shop shop = _context.Shops.SingleOrDefault(s => s.Id == shopid);
            if(shop == null)
            {
                return response;
            }
            response.username = user.Name;
            response.shopname = shop.Name;
            Conversation conversation  = _context.Conversations.Include( s => s.Shop).Include(s => s.User)
                .Where( s => s.Shop == shop && s.User == user ).Include(s => s.Message).FirstOrDefault();
            if(conversation == null)
            {
                return response;
            }
            else
            {
                if(conversation.Message.Any())
                {
                    List<Message> messages = conversation.Message;
                    foreach(Message message in messages)
                    {
                        Message_US item = new Message_US
                        {
                            message = message.message,
                            type = message.type,
                            time = message.time.AddHours(7),
                        };
                        respone_message.Add(item);
                    }
                    response.messages = respone_message.OrderByDescending(s => s.time).ToList();
                }
            }
            return response;
        }
    }
}
