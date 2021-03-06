#pragma checksum "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "356e8c83daeaaa2cf9374be253889ea594a827ab"
// <auto-generated/>
#pragma warning disable 1591
namespace WebSPA.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Wrappers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using AntDesign;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/claims")]
    public partial class Claims : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(0);
            __builder.AddAttribute(1, "Authorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(2, "h2");
                __builder2.AddMarkupContent(3, "\r\n            Hello ");
                __builder2.AddContent(4, 
#nullable restore
#line 6 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor"
                   context.User.Identity.Name

#line default
#line hidden
#nullable disable
                );
                __builder2.AddMarkupContent(5, ",\r\n            here\'s the list of your claims:\r\n        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(6, "\r\n        ");
                __builder2.OpenElement(7, "ul");
#nullable restore
#line 10 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor"
             foreach (var claim in context.User.Claims)
            {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(8, "li");
                __builder2.OpenElement(9, "b");
                __builder2.AddContent(10, 
#nullable restore
#line 12 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor"
                        claim.Type

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddContent(11, ": ");
                __builder2.AddContent(12, 
#nullable restore
#line 12 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor"
                                         claim.Value

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 13 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Claims.razor"
            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
            }
            ));
            __builder.AddAttribute(13, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(14, "<p>I\'m sorry, I can\'t display anything until you log in</p>");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
