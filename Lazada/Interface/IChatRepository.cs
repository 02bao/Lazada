using Lazada.Models;

namespace Lazada.Interface
{
    public interface IChatRepository
    {
        Conversation_US GetConversation_ById(long userid, long shopid);
        bool NewMessage(long userid, long shopid, string mess);
    }
}
