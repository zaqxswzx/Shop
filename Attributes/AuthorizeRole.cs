using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Shop.Attributes {
    public class AuthorizeRole : AuthorizeAttribute {
        public AuthorizeRole(Role role) {
            Roles = ((int)role).ToString();
        }
    }
}
