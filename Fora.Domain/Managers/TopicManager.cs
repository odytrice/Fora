using Fora.Domain.Interface.Repositories;
using Fora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Domain.Managers
{
    public class TopicManager
    {
        private ITopicRepository _post;

        public TopicManager(ITopicRepository post)
        {
            _post = post;
        }
        public PagedList<TopicModel> GetTopics(string search, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _post.GetTopics(page, pageSize);
            }
            else
            {
                return _post.FindTopic(search, page, pageSize);
            }
        }

        public Operation<PostModel> AddReply(PostModel model)
        {
            return Operation.Create(() =>
            {
                model.Validate();
                return _post.AddReply(model);
            });
        }

        public Operation<TopicModel> Create(TopicModel model)
        {
            return Operation.Create(() =>
            {
                model.Validate();
                return _post.Create(model);
            });
        }

        public TopicModel GetTopic(int id)
        {
            return _post.GetTopic(id);
        }
    }
}
