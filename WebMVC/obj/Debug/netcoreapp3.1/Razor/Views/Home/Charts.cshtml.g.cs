#pragma checksum "D:\Codes\eRecruitmentOnMicroservices\WebMVC\Views\Home\Charts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dc1b8200e780594579dc14d6a7ceaaf6028596ad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Charts), @"mvc.1.0.view", @"/Views/Home/Charts.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Codes\eRecruitmentOnMicroservices\WebMVC\Views\_ViewImports.cshtml"
using WebMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Codes\eRecruitmentOnMicroservices\WebMVC\Views\_ViewImports.cshtml"
using WebMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc1b8200e780594579dc14d6a7ceaaf6028596ad", @"/Views/Home/Charts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d07e873f05b36c9d83cd6a184d4bfbe1720fee4b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Charts : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Codes\eRecruitmentOnMicroservices\WebMVC\Views\Home\Charts.cshtml"
  
    ViewData["Title"] = "Charts";

#line default
#line hidden
#nullable disable
            WriteLiteral("<!-- HOME -->\r\n<section class=\"home-section section-hero inner-page overlay bg-image\"");
            BeginWriteAttribute("style", "\r\n         style=\"", 127, "\"", 206, 4);
            WriteAttributeValue("", 145, "background-image:", 145, 17, true);
            WriteAttributeValue(" ", 162, "url(\'", 163, 6, true);
#nullable restore
#line 6 "D:\Codes\eRecruitmentOnMicroservices\WebMVC\Views\Home\Charts.cshtml"
WriteAttributeValue("", 168, Url.Content("~/images/hero_1.jpg"), 168, 35, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 203, "\');", 203, 3, true);
            EndWriteAttribute();
            WriteLiteral(@" id=""home-section"">
    <div class=""container"">
        <div class=""row align-items-center justify-content-center"">
            <div class=""col-md-12"">
                <div class=""mb-5 text-center"">
                    <h1 class=""text-white font-weight-bold"">About Us</h1>
                    <p>Find your dream jobs in our powerful career website template.</p>
                </div>
            </div>
        </div>
    </div>
</section>

<section class=""site-section"">
    <div class=""container"">
        <div class=""row mb-5"">
            <div class=""col-12 text-center"" data-aos=""fade"">
                <h2 class=""section-title mb-3"">Statistics</h2>
            </div>
        </div>

        <div class=""row align-items-center block__69944"">

            <div class=""col-md-6"">
                <canvas id=""jobs-statistics-chart1""></canvas>
            </div>


            <div class=""col-md-6"">
                <canvas id=""jobs-statistics-chart2""></canvas>
            </div>


     ");
            WriteLiteral("   </div>\r\n    </div>\r\n</section>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        var token = prompt(""Please enter your token:"", """");
        if (token != null && token != """") {
            $.ajax({
                type: ""GET"",
                url: ""https://localhost:5005/api/Statistics/getJobAppliedCount"",
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader(""Authorization"", ""bearer "" + token)
                },
                success: function (data, status, xhr) {
                    debugger;
                    BuildBarChart('jobs-statistics-chart1', data[""data""].jobsName, data[""data""].applierCount, 'Top 5 Applied Jobs Bar Chart', 'Applier')
                    BuildPieChart('jobs-statistics-chart2', data[""data""].jobsName, data[""data""].applierCount, 'Top 5 Applied Jobs Pie Chart', 'Applier')
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    debugger;
                    alert(""An error occured while loading the data"");
  ");
                WriteLiteral("              }\r\n            });  \r\n        }     \r\n    </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
