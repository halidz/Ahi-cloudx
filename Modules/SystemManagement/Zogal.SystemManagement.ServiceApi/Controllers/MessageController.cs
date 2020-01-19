using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zogal.SystemManagement.Business;
using Zogal.SystemManagement.ServiceApi.Messages;

namespace Zogal.SystemManagement.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageFacade _facade;
        public MessageController(IMessageFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        [HttpPost("create")]
        public MessageCreateResponse Create(MessageCreateRequest request)
        {
            var response = new MessageCreateResponse();

            response.Id = _facade.Create(request.Message);

            return response;


        }
        [HttpPost("search")]
        public MessageSearchResponse Search(MessageSearchRequest request)
        {
            var response = new MessageSearchResponse();
            response.List = _facade.Search(request.Filter, request.PaginationInfo);
            return response;
        }


        [HttpPost("delete")]
        public MessageDeleteResponse Delete(MessageDeleteRequest request)
        {
            var response = new MessageDeleteResponse();
             _facade.Delete(request.Id);
            return response;
        }
    }
}