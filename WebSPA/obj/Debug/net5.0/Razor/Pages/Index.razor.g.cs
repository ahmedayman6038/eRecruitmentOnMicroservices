#pragma checksum "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57e72ab6b615b0e6b3c123ecd552b5dff59b50da"
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
#nullable restore
#line 2 "D:\Codes\eRecruitmentOnMicroservices\WebSPA\Pages\Index.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Hello, world!</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<div class=\"alert alert-warning\" role=\"alert\">\r\n    Before authentication will function correctly, you must configure your provider details in <code>Program.cs</code></div>\r\n\r\nWelcome to your new app.\r\n\r\n");
            __builder.OpenComponent<WebSPA.Shared.SurveyPrompt>(2);
            __builder.AddAttribute(3, "Title", "How is Blazor working for you?");
            __builder.CloseComponent();
            __builder.AddMarkupContent(4, "\r\n\r\n");
            __builder.OpenElement(5, "div");
            __builder.OpenComponent<AntDesign.Button>(6);
            __builder.AddAttribute(7, "Type", "primary");
            __builder.AddAttribute(8, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(9, "Primary");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(10, "\r\n    ");
            __builder.OpenComponent<AntDesign.Button>(11);
            __builder.AddAttribute(12, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(13, "Default");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(14, "\r\n    ");
            __builder.OpenComponent<AntDesign.Button>(15);
            __builder.AddAttribute(16, "Type", "dashed");
            __builder.AddAttribute(17, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(18, "Dashed");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(19, "\r\n    ");
            __builder.OpenComponent<AntDesign.Button>(20);
            __builder.AddAttribute(21, "Type", "link");
            __builder.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddContent(23, "Link");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
