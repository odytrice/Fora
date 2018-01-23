using Fora.Domain.Interface.Repositories;
using Fora.Domain.Models;
using Fora.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private DbContext _context;

        public TopicRepository(DbContext context)
        {
            _context = context;
        }

        public PostModel AddReply(PostModel model)
        {
            var post = MapPost(model);
            _context.Set<Post>().Add(post);
            _context.SaveChanges();
            model.PostId = post.PostId;
            model.DateCreated = post.DateCreated;
            return model;
        }

        public TopicModel Create(TopicModel model)
        {
            var entity = MapTopic(model);
            _context.Add(entity);
            _context.SaveChanges();
            model.TopicId = entity.TopicId;
            return model;
        }

        public PagedList<TopicModel> FindTopic(string search, int page, int pageSize)
        {
            var query = from topic in _context.Set<Topic>().Include(t => t.Author).Include(t => t.Replies)
                        where topic.Headline.Contains(search)
                        || topic.Description.Contains(search)
                        select topic;

            return query.ToPagedList(page, pageSize).Map(MapTopic);
        }

        public TopicModel GetTopic(int id)
        {
            var query = from topic in _context.Set<Topic>().Include(t => t.Author).Include(t => t.Replies)
                        where topic.TopicId == id
                        select topic;

            var record = query.FirstOrDefault();
            if (record == null) return null;
            return MapTopic(record);
        }

        public PagedList<TopicModel> GetTopics(int page, int pageSize)
        {
            var query = from topic in _context.Set<Topic>().Include(t => t.Author).Include(t => t.Replies)
                        select topic;

            return query.ToPagedList(page, pageSize).Map(MapTopic);
        }

        private TopicModel MapTopic(Topic topic)
        {
            var model = new TopicModel();
            model.Assign(topic);
            model.Author = new UserModel().Assign(topic.Author);
            model.Replies = topic.Replies.Select(p => new PostModel().Assign(p)).ToList();
            return model;
        }

        private Topic MapTopic(TopicModel model)
        {
            return new Topic
            {
                AuthorId = model.AuthorId,
                Description = model.Description,
                Headline = model.Headline,
            };
        }

        private Post MapPost(PostModel model)
        {
            var post = new Post
            {
                Body = model.Body,
                PostId = model.PostId,
                UserId = model.UserId,
                TopicId = model.TopicId
            };
            return post;
        }
    }
}
