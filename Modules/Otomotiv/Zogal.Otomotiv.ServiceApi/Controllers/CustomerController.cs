using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.ServiceApi.Messages;


namespace Zogal.Otomotiv.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerFacade _facade;
       
        public CustomerController(ICustomerFacade facade)
        {
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
           

        }
        [HttpPost("create")]
        public CustomerCreateResponse Create([FromBody]CustomerCreateRequest request)
        {
           

            var response = new CustomerCreateResponse();

            response.Id = _facade.Create(request.Customer);

            return response;

        }

        [HttpPost("search")]
        public CustomerSearchResponse Search([FromBody]CustomerSearchRequest request)
        {
            

            var response = new CustomerSearchResponse();

            response.List = _facade.Search(request.Filter, request.PaginationInfo);

            return response;
        }

        [HttpPost("detail")]

        public CustomerGetResponse Get([FromBody]CustomerGetRequest request)
        {
           

            var response = new CustomerGetResponse();

            response.Customer = _facade.Get(request.Id);

            return response;
        }

        [HttpPost("update")]

        public CustomerUpdateResponse Update([FromBody]CustomerUpdateRequest request)
        {
            

            var response = new CustomerUpdateResponse();

           _facade.Update(request.Customer);

            return response;
        }

        [HttpPost("delete")]

        public CustomerDeleteResponse Delete([FromBody]CustomerDeleteRequest request)
        {
            

            var response = new CustomerDeleteResponse();
            _facade.Delete(request.Id);

            return response;
        }


    }
}