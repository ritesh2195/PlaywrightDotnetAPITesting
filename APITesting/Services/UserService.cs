using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Services
{
    public class UserService
    {
        private IAPIRequestContext _requestContext;

        public UserService(IAPIRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
    }
}
