using Lazada.Models;

namespace Lazada.Interface
{
    public interface IChatRepository
    {
        Conversation_US GetConversation_ById(long userid, long shopid);
    }
}
