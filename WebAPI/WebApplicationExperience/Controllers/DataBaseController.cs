using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Http;
using System.Web.Http.Results;
using static WebApplicationExperience.Controllers.ValuesController;

namespace WebApplicationExperience.Controllers
{
    public class DataBaseController : ApiController
    {
        // GET: api/DataBase
        public IEnumerable<string> Get()
        {

            using (For_TestEntities db = new For_TestEntities())
            {
                var name = (from a in db.AccountData
                            select a.FirstName + " " + a.LastName).ToList();
                return name;
            }
        }

        // GET: api/DataBase/5
        public string Get(int id)
        {
            return "value";
        }

        public class PostClass
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string LoginAccount { get; set; }
            public string Password { get; set; }
        }

        // POST: api/DataBase
        public AccountData Post([FromBody] PostClass postClass)
        {
            using (For_TestEntities db = new For_TestEntities())
            {
                AccountData accountData = new AccountData();
                Guid id = Guid.NewGuid();
                accountData.ID = id;
                accountData.FirstName = postClass.FirstName;
                accountData.LastName = postClass.LastName;
                accountData.LoginAccount = postClass.LoginAccount;
                accountData.Password = postClass.Password;
                accountData.Status = 1;
                accountData.CreatedDate = DateTime.Now;
                db.AccountData.Add(accountData);
                db.SaveChanges();

                var account = (from a in db.AccountData
                               where a.ID == id
                               select a).FirstOrDefault();

                return account;
            }
        }

        public class PutClass
        {
            public string LoginAccount { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }
        public class PutResponse : AccountData
        {
            public string ResponseStatus { get; set; }
        }

        // PUT: api/DataBase
        public PutResponse Put([FromBody] PutClass putClass)
        {
            using (For_TestEntities db = new For_TestEntities())
            {
                var accountData = (from a in db.AccountData
                                   where a.LoginAccount == putClass.LoginAccount &&
                                   a.Password == putClass.Password
                                   select a).FirstOrDefault();
                PutResponse response = new PutResponse();
                if (accountData != null)
                {
                    accountData.Password = putClass.NewPassword;
                    db.SaveChanges();
                    response.ResponseStatus = "Success";

                    var changedAccountData = (from a in db.AccountData
                                              where a.LoginAccount == putClass.LoginAccount &&
                                              a.Password == putClass.NewPassword
                                              select a).FirstOrDefault();
                    response.ID = changedAccountData.ID;
                    response.FirstName = changedAccountData.FirstName;
                    response.LastName = changedAccountData.LastName;
                    response.LoginAccount = changedAccountData.LoginAccount;
                    response.Password = changedAccountData.Password;
                    response.Status = changedAccountData.Status;
                    response.CreatedDate = changedAccountData.CreatedDate;
                    response.StaffData = changedAccountData.StaffData;
                    response.TravelAgencyUserData = changedAccountData.TravelAgencyUserData;
                }
                else
                {
                    response.ResponseStatus = "Fail";
                }
                return response;
            }
        }

        public class DeleteClass
        {
            public string LoginAccount { get; set; }
            public string Password { get; set; }
        }

        // DELETE: api/DataBase
        public object Delete([FromBody] DeleteClass deleteClass)
        {
            using (For_TestEntities db = new For_TestEntities())
            {
                var accountData = (from a in db.AccountData
                                   where a.LoginAccount == deleteClass.LoginAccount &&
                                   a.Password == deleteClass.Password
                                   select a).FirstOrDefault();
                if (accountData == null)
                {
                    var response = new { Status = "Fail" };
                    return response;
                }
                else
                {
                    db.AccountData.Remove(accountData);
                    db.SaveChanges();
                    var response = new { Status = "Success" };
                    return response;
                }
            }
        }

        [HttpDelete]
        [Route("api/DatabaseDelete/{LoginAccount}/{Password}")]
        public object Delete(string LoginAccount, string Password)
        {
            using (For_TestEntities db = new For_TestEntities())
            {
                var accountData = (from a in db.AccountData
                                   where a.LoginAccount == LoginAccount &&
                                   a.Password == Password
                                   select a).FirstOrDefault();
                if (accountData == null)
                {
                    var response = new { Status = "Fail" };
                    return response;
                }
                else
                {
                    db.AccountData.Remove(accountData);
                    db.SaveChanges();
                    var response = new { Status = "Success" };
                    return response;
                }
            }
        }
    }
}
