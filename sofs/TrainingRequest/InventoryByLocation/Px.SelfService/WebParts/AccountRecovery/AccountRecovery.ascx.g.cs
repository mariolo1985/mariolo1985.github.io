﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Px.SelfService.WebParts.AccountRecovery {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class AccountRecovery {
        
        protected global::System.Web.UI.WebControls.RadioButton rbn_Password;
        
        protected global::System.Web.UI.WebControls.RadioButton rbn_Username;
        
        protected global::System.Web.UI.WebControls.Label lbl_Instruction;
        
        protected global::System.Web.UI.WebControls.Label lbl_Prompt;
        
        protected global::System.Web.UI.WebControls.TextBox txb_Prompt;
        
        protected global::System.Web.UI.WebControls.Button btn_Recover;
        
        protected global::System.Web.UI.WebControls.Button btn_Cancel;
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator;
        
        protected global::System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator;
        
        protected global::System.Web.UI.WebControls.Literal ltr_Debug;
        
        public static implicit operator global::System.Web.UI.TemplateControl(AccountRecovery target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RadioButton @__BuildControlrbn_Password() {
            global::System.Web.UI.WebControls.RadioButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RadioButton();
            this.rbn_Password = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "rbn_Password";
            @__ctrl.Text = "Reset Password";
            @__ctrl.Checked = true;
            @__ctrl.CssClass = "rbnf";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RadioButton @__BuildControlrbn_Username() {
            global::System.Web.UI.WebControls.RadioButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RadioButton();
            this.rbn_Username = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "rbn_Username";
            @__ctrl.Text = "Retrieve Username";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllbl_Instruction() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lbl_Instruction = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbl_Instruction";
            @__ctrl.Text = "Please Enter your username to reset your password:";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllbl_Prompt() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lbl_Prompt = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbl_Prompt";
            @__ctrl.Text = "Username";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxb_Prompt() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txb_Prompt = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txb_Prompt";
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(150D, global::System.Web.UI.WebControls.UnitType.Pixel);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtn_Recover() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btn_Recover = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btn_Recover";
            @__ctrl.Text = "Reset Password";
            @__ctrl.CssClass = "pxBtn";
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(28D, global::System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.Click -= new System.EventHandler(this.btn_Recover_Click);
            @__ctrl.Click += new System.EventHandler(this.btn_Recover_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtn_Cancel() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btn_Cancel = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btn_Cancel";
            @__ctrl.Text = "Cancel";
            @__ctrl.CssClass = "pxBtnCancel";
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(28D, global::System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.btn_Cancel_Click);
            @__ctrl.Click += new System.EventHandler(this.btn_Cancel_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlRequiredFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.RequiredFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "RequiredFieldValidator";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            @__ctrl.CssClass = "text-error";
            @__ctrl.ControlToValidate = "txb_Prompt";
            @__ctrl.ErrorMessage = "Please provide your username.";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RegularExpressionValidator @__BuildControlRegularExpressionValidator() {
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RegularExpressionValidator();
            this.RegularExpressionValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "RegularExpressionValidator";
            @__ctrl.Enabled = false;
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            @__ctrl.CssClass = "text-error";
            @__ctrl.ControlToValidate = "txb_Prompt";
            @__ctrl.ValidationExpression = "^[_a-z0-9-]+(\\.[_a-z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,4})$";
            @__ctrl.ErrorMessage = "Invalid email address.";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltr_Debug() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltr_Debug = @__ctrl;
            @__ctrl.ID = "ltr_Debug";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::Px.SelfService.WebParts.AccountRecovery.AccountRecovery @__ctrl) {
            global::System.Web.UI.WebControls.RadioButton @__ctrl1;
            @__ctrl1 = this.@__BuildControlrbn_Password();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            global::System.Web.UI.WebControls.RadioButton @__ctrl2;
            @__ctrl2 = this.@__BuildControlrbn_Username();
            @__parser.AddParsedSubObject(@__ctrl2);
            global::System.Web.UI.WebControls.Label @__ctrl3;
            @__ctrl3 = this.@__BuildControllbl_Instruction();
            @__parser.AddParsedSubObject(@__ctrl3);
            global::System.Web.UI.WebControls.Label @__ctrl4;
            @__ctrl4 = this.@__BuildControllbl_Prompt();
            @__parser.AddParsedSubObject(@__ctrl4);
            global::System.Web.UI.WebControls.TextBox @__ctrl5;
            @__ctrl5 = this.@__BuildControltxb_Prompt();
            @__parser.AddParsedSubObject(@__ctrl5);
            global::System.Web.UI.WebControls.Button @__ctrl6;
            @__ctrl6 = this.@__BuildControlbtn_Recover();
            @__parser.AddParsedSubObject(@__ctrl6);
            global::System.Web.UI.WebControls.Button @__ctrl7;
            @__ctrl7 = this.@__BuildControlbtn_Cancel();
            @__parser.AddParsedSubObject(@__ctrl7);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl8;
            @__ctrl8 = this.@__BuildControlRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl8);
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl9;
            @__ctrl9 = this.@__BuildControlRegularExpressionValidator();
            @__parser.AddParsedSubObject(@__ctrl9);
            global::System.Web.UI.WebControls.Literal @__ctrl10;
            @__ctrl10 = this.@__BuildControlltr_Debug();
            @__parser.AddParsedSubObject(@__ctrl10);
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write(@"

<script type=""text/javascript"">
    
    function Dialog(text) {
        var html = ""<div class='modal fade'><div class='modal-dialog'><div class='modal-content'><div class='modal-body'><p>"" + text + ""</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button></div></div></div></div>"";
        $(html).modal();
    }

    function PasswordSelected() {
        document.getElementById('");
                         @__w.Write( ((RadioButton)FindControl("rbn_Username")).ClientID );

            @__w.Write("\').checked = false;\r\n        document.getElementById(\'");
                         @__w.Write( ((Label)FindControl("lbl_Instruction")).ClientID );

            @__w.Write("\').innerHTML = \'Please enter your username to reset your password:\';\r\n        doc" +
                    "ument.getElementById(\'");
                         @__w.Write( ((Label)FindControl("lbl_Prompt")).ClientID );

            @__w.Write("\').innerHTML = \'Username\';\r\n        document.getElementById(\'");
                         @__w.Write( ((Button)FindControl("btn_Recover")).ClientID );

            @__w.Write("\').value = \'Reset Password\';\r\n        document.getElementById(\'");
                         @__w.Write( ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID );

            @__w.Write("\').style.display = \'none\';\r\n        document.getElementById(\'");
                         @__w.Write( ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID );

            @__w.Write("\').innerHTML = \"Please provide your username.\";\r\n        ValidatorEnable(document" +
                    ".getElementById(\'");
                                         @__w.Write(RegularExpressionValidator.ClientID);

            @__w.Write("\'), false);\r\n    }\r\n\r\n    function UsernameSelected() {        \r\n        document" +
                    ".getElementById(\'");
                         @__w.Write( ((RadioButton)FindControl("rbn_Password")).ClientID );

            @__w.Write("\').checked = false;\r\n        document.getElementById(\'");
                         @__w.Write( ((Label)FindControl("lbl_Instruction")).ClientID );

            @__w.Write("\').innerHTML = \'Please enter your email address to request your username:\';\r\n    " +
                    "    document.getElementById(\'");
                         @__w.Write( ((Label)FindControl("lbl_Prompt")).ClientID );

            @__w.Write("\').innerHTML = \'Email Address\';\r\n        document.getElementById(\'");
                         @__w.Write( ((Button)FindControl("btn_Recover")).ClientID );

            @__w.Write("\').value = \'Request Username\';\r\n        document.getElementById(\'");
                         @__w.Write( ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID );

            @__w.Write("\').style.display = \'none\';\r\n        document.getElementById(\'");
                         @__w.Write( ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID );

            @__w.Write("\').innerHTML = \"Please provide an email address.\";\r\n        ValidatorEnable(docum" +
                    "ent.getElementById(\'");
                                         @__w.Write(RegularExpressionValidator.ClientID);

            @__w.Write("\'), true);\r\n    }\r\n\r\n</script>\r\n\r\n<style type=\"text/css\">\r\n    #s4-workspace { bo" +
                    "rder-radius: 4px; width:450px !important; height: auto !important; position:abso" +
                    "lute; left:50%; top:50%; margin-left: -225px !important; margin-top: -100px !imp" +
                    "ortant; }\r\n    #s4-bodyContainer {padding-bottom: 0 !important; }\r\n    #Anonymou" +
                    "sHeader { background: #FFFFFF; margin: -15px 0 0 0; padding: 7px; }           \r\n" +
                    "    a, input[type=\'text\'], input[type=\'password\'] { display:block; padding-right" +
                    ": 10px; margin-top: 2px; }\r\n    label { display:inline }\r\n    .terms-input { pad" +
                    "ding: 4px 6px; border-radius: 4px; height: 20px; color: rgb(85, 85, 85); line-he" +
                    "ight: 20px; font-size: 14px; vertical-align: middle; display: inline-block; -web" +
                    "kit-border-radius: 4px; -moz-border-radius: 4px; }\r\n    .terms-input { border: 1" +
                    "px solid rgb(204, 204, 204); transition:border 0.2s linear, box-shadow 0.2s line" +
                    "ar; box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075); background-color: rgb(255, " +
                    "255, 255); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -moz-box-sh" +
                    "adow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -webkit-transition: border linear .2" +
                    "s, box-shadow linear .2s; -moz-transition: border linear .2s, box-shadow linear " +
                    ".2s; -o-transition: border linear .2s, box-shadow linear .2s; }\r\n    .terms-inpu" +
                    "t { overflow: hidden; overflow-y: scroll; width: 810px; height: 164px; }\r\n    .t" +
                    "erms-input:focus { border-color: rgba(82, 168, 236, 0.8); outline: dotted thin; " +
                    "box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075), 0px 0px 8px rgba(82,168,236,0.6" +
                    "); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 16" +
                    "8, 236, 0.6); -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgb" +
                    "a(82, 168, 236, 0.6); }\r\n    .rbnf { float:left; margin-right: 10px; }\r\n    .pxC" +
                    "ontent { padding: 10px 10px 10px 15px; font-family: Open Sans; color: #454545; }" +
                    "\r\n</style>\r\n\r\n<div id=\"AnonymousHeader\">\r\n    <div><img class=\"logo\" src=\"/_layo" +
                    "uts/15/Px.Branding/Images/Logo-195x50.png\" alt=\"\"></div>\r\n    <div style=\"clear:" +
                    "both\"></div>\r\n</div>\r\n\r\n<div class=\"pxContent\">\r\n   \r\n    <div style=\"margin-bot" +
                    "tom: 5px; margin-top: 8px; \">\r\n        <div>");
            parameterContainer.Controls[0].RenderControl(@__w);
            @__w.Write("</div>\r\n        <div>");
            parameterContainer.Controls[1].RenderControl(@__w);
            @__w.Write("</div>\r\n        <div class=\"clear\"></div>\r\n    </div>\r\n    \r\n    <div style=\"marg" +
                    "in-bottom: 5px;\">\r\n        <div>");
            parameterContainer.Controls[2].RenderControl(@__w);
            @__w.Write("</div>\r\n    </div>\r\n\r\n    <div class=\"pxFields\">\r\n        ");
            parameterContainer.Controls[3].RenderControl(@__w);
            @__w.Write("\r\n        ");
            parameterContainer.Controls[4].RenderControl(@__w);
            @__w.Write("        \r\n    </div>\r\n\r\n    <div class=\"pxFields\" style=\"margin-top: 27px;\">\r\n   " +
                    "     ");
            parameterContainer.Controls[5].RenderControl(@__w);
            @__w.Write("  <!--#286793-->\r\n        ");
            parameterContainer.Controls[6].RenderControl(@__w);
            @__w.Write("\r\n    </div>\r\n    <div class=\"clear\"></div>\r\n\r\n    <div>\r\n        ");
            parameterContainer.Controls[7].RenderControl(@__w);
            @__w.Write("\r\n        ");
            parameterContainer.Controls[8].RenderControl(@__w);
            @__w.Write("\r\n    </div>\r\n    \r\n    ");
            parameterContainer.Controls[9].RenderControl(@__w);
            @__w.Write("\r\n    \r\n</div>");
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
