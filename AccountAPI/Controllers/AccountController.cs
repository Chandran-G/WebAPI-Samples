using Account.DomainModel;
using Account.ServiceLayer.Contract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Account.API.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiBaseController
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Account Controller
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// List All Account
        /// </summary>
        /// <returns>IEnumerabe<Account></Account></returns>
        /// 
        [HttpGet]
        [Route("", Name = "List All Account")]
        public IHttpActionResult GetAllAccounts()
        {
            var response = _accountService.GetAll();
            return Ok(response);
        }


        /// <summary>
        /// Get User Account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "Get User Account")]
        public IHttpActionResult Get(int id)
        {
            var item = _accountService.GetUserAccount(id);

            if (null == item)
                return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Create User Account
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "Create User Account")]
        public IHttpActionResult Post([FromBody] UserAccount userAccount)
        {
            var response = _accountService.Add(userAccount);
            if (null == response)
                return InternalServerError();
            return Created(new Uri(GetLink<AccountController>("Get User Account", new { id = response.Id })), response);
        }

        /// <summary>
        /// Update User Account
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("", Name = "Update User Account")]
        public IHttpActionResult Put([FromBody] UserAccount userAccount)
        {
            var currentUserAccount = _accountService.GetUserAccount(userAccount.Id);
            if (null == currentUserAccount)
            {
                return NotFound();
            }

            var response = _accountService.Update(userAccount);
            return Ok(response);
        }

        /// <summary>
        /// Delete User Account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}", Name = "Delete User Account")]
        public IHttpActionResult Delete(int id)
        {
            _accountService.Remove(id);
            return Ok();
        }

    }
}
