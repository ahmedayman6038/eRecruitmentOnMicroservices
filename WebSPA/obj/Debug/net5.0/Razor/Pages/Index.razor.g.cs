#pragma checksum "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7da12eae6cea5db2273c5822e6dbfd17768469b2"
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
#line 1 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "E:\DotNetProjects\eRecruitmentOnMicroservices\WebSPA\_Imports.razor"
using WebSPA.Wrappers;

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
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
