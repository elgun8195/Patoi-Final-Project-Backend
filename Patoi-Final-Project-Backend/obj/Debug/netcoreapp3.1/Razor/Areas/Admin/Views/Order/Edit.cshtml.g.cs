#pragma checksum "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59fe67ac6646650a9a090d6cee36371bc77e49e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Order_Edit), @"mvc.1.0.view", @"/Areas/Admin/Views/Order/Edit.cshtml")]
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
#line 1 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\_ViewImports.cshtml"
using Patoi_Final_Project_Backend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\_ViewImports.cshtml"
using Patoi_Final_Project_Backend.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59fe67ac6646650a9a090d6cee36371bc77e49e0", @"/Areas/Admin/Views/Order/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab4c92cb1cf4072b908d62fab4cf056b5f65d47b", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Order_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Order>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Accept", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("accept btn btn-success btn-icon-text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Reject", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("reject btn btn-danger btn-icon-text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div style=\"display:flex;flex-direction:column\">\r\n\r\n    <ul style=\"list-style:none\">\r\n        <li>UserName: ");
#nullable restore
#line 9 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                 Write(Model.AppUser.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li> \r\n        <li>Email: ");
#nullable restore
#line 10 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
              Write(Model.AppUser.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>Country: ");
#nullable restore
#line 11 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                Write(Model.Country);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>Apartment   : ");
#nullable restore
#line 12 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                     Write(Model.Apartment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>Address: ");
#nullable restore
#line 13 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>Date: ");
#nullable restore
#line 14 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
             Write(Model.Date.ToString("HH:mm dd,MMMM,yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        <li>\r\n            <h3>Order items</h3>\r\n            <ul>\r\n");
#nullable restore
#line 18 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                 foreach (OrderItem item in Model.OrderItems)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li>\r\n                        ");
#nullable restore
#line 21 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                   Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </li>\r\n                    <li>\r\n                        $");
#nullable restore
#line 24 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                    Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <hr />\r\n                    </li>\r\n");
#nullable restore
#line 27 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n        </li>\r\n        <li>\r\n");
#nullable restore
#line 31 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
             if (Model.Status == null)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <span style=\"background:orange;display:inline-block; padding:3px 7px\">Pending</span>\r\n");
#nullable restore
#line 35 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"

            }
            else if (Model.Status == true)
            {



#line default
#line hidden
#nullable disable
            WriteLiteral("                <span style=\"background:green;display:inline-block; padding:3px 7px\">Accepted</span>\r\n");
#nullable restore
#line 42 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"

            }
            else
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <span style=\"background:red;display:inline-block; padding:3px 7px\">Rejected</span>\r\n");
#nullable restore
#line 48 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </li>\r\n        <li>\r\n            Total price: $");
#nullable restore
#line 52 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                     Write(Model.TotalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </li>\r\n    </ul>\r\n\r\n\r\n    <div>\r\n        <h4>Message</h4>\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "59fe67ac6646650a9a090d6cee36371bc77e49e010925", async() => {
                WriteLiteral("\r\n            <input class=\"Message\" cols=\"60\" rows=\"10\" />\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "59fe67ac6646650a9a090d6cee36371bc77e49e012461", async() => {
                WriteLiteral("\r\n                <i class=\"mdi mdi-file-check btn-icon-append\"></i>\r\n                Accept\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 64 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                                                            WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "59fe67ac6646650a9a090d6cee36371bc77e49e015074", async() => {
                WriteLiteral("\r\n                <i class=\"mdi mdi-file-check btn-icon-append\"></i>\r\n                Reject\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 68 "C:\Users\hp\Desktop\Patoi-Final-Project-Backend\Patoi-Final-Project-Backend\Areas\Admin\Views\Order\Edit.cshtml"
                                                            WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n\r\n\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $("".accept"").click(function (e) {
                e.preventDefault();
                var message = $("".Message"").val();
                var link = $(this).attr(""href"") + ""/?message="" + message
                fetch(link).then(res => res.json()).then(data => {
                    if (data.status == 200) {
                        location.href = ""https://localhost:5001/admin/order""
                    } else {

                    }
                })
            })
            $("".reject"").click(function (e) {
                e.preventDefault();
                var message = $("".Message"").val();
                var link = $(this).attr(""href"") + ""/?message="" + message
                fetch(link).then(res => res.json()).then(data => {
                    if (data.status == 200) {
                        location.href = ""https://localhost:5001/admin/order""
                    } else {
                    }
                }");
                WriteLiteral(")\r\n            })\r\n        })\r\n    </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Order> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
