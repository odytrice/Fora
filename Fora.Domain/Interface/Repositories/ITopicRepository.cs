using System;
using System.Collections.Generic;
using System.Text;
using Fora.Domain.Models;

namespace Fora.Domain.Interface.Repositories
{
    public interface ITopicRepository
    {
        PagedList<TopicModel> FindTopic(string search, int page, int pageSize);
        PagedList<TopicModel> GetTopics(int page, int pageSize);
        TopicModel Create(TopicModel model);
        TopicModel GetTopic(int id);
        PostModel AddReply(PostModel model);
    }
}
