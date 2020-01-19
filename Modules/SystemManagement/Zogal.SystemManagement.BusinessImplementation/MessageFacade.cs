using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.SystemManagement.Business;
using Zogal.SystemManagement.EntityModel;
using Zogal.SystemManagement.ViewModel;

namespace Zogal.SystemManagement.BusinessImplementation
{
    public class MessageFacade : IMessageFacade
    {
        private readonly IRepository _repository;

        public MessageFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public long Create(MessageView message)
        {
            return _repository.Save(new Message
            {
                Name=message.Name,
                Email=message.Email,
                PhoneNumber=message.PhoneNumber,
                Subject=message.Subject,
                Body=message.Body,
                Source=message.Source
               
            });
        }

        public PaginatedList<MessageView> Search(MessageSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var messageQuery = _repository.Query<Message>();
            if (!string.IsNullOrEmpty(filter.Name))
                messageQuery = messageQuery.Where(x => x.Name == filter.Name);
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                messageQuery = messageQuery.Where(x => x.Name == filter.PhoneNumber);
            if (!string.IsNullOrEmpty(filter.Email))
                messageQuery = messageQuery.Where(x => x.Name == filter.Email);
            if (!string.IsNullOrEmpty(filter.Subject))
                messageQuery = messageQuery.Where(x => x.Name == filter.Subject);
            var newQuery = messageQuery.Select(x => new MessageView
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Subject = x.Subject,
                Body = x.Body,
                Source=x.Source
            });
            PaginatedList<MessageView> paginatedList = new PaginatedList<MessageView>(paginationInfo, newQuery);
            return paginatedList;
        }
        public void Delete(long id)
        {
            var entity = _repository.Get<Message>(id);
            _repository.Delete(entity);
        }
    }
}
