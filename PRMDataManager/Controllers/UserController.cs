using Microsoft.AspNet.Identity;
using PRMDataManager.Library.DataAccess;
using PRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace PRMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {

        //Retrives the curren logged in users' id and gets their data
        [HttpGet]
        public UserModel GetById()
        {
            //Give me the current logged in users token.
            string userId = RequestContext.Principal.Identity.GetUserId();

            /***
             * Create a UserData Object, which is a data access object that retrieves data from the sql db 
             * and returns the data in a UserModel object.
             */

            UserData data = new UserData();

            return data.GetUserById(userId).First();
        }


       
        }  
}
