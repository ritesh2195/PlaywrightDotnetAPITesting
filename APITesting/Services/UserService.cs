using APITesting.Constant;
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
        private readonly IAPIRequestContext _requestContext;

        public UserService(IAPIRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public async Task<IAPIResponse> GetUserDetailsAsync(string accountId)
        {
            return await _requestContext.GetAsync($"{EndPointConstants.UserEndPoint}/?accountId={accountId}");
        }
    }
}
