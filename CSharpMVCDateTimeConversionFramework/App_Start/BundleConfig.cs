﻿using System.Web.Optimization;

namespace CSharpMVCDateTimeConversionFramework 
{
    public class BundleConfig 
    {
        public static void RegisterBundles(BundleCollection bundles) 
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-1.9.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                                         "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                        "~/Scripts/jquery.unobtrusive*",
                                        "~/Scripts/jquery.validat*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/syntaxHighlighter").Include(
                                         "~/Scripts/SyntaxHighlighter/XRegExp.js",
                                         "~/Scripts/SyntaxHighlighter/shCore.js",
                                         "~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap/bootstrap.min.js",
                                         "~/Scripts/bootstrap/bootstrap-dropdown.js",
                                         "~/Scripts/bootstrap/bootstrap-modal.js",
                                         "~/Scripts/bootstrap/bootstrap-tooltip.js",
                                         "~/Scripts/bootstrap/bootstrap-tab.js",
                                         "~/Scripts/bootstrap/bootstrap-button.js",
                                         "~/Scripts/bootstrap/bootstrap-affix.js",
                                         "~/Scripts/bootstrap/bootstrap-alert.js"));

            bundles.Add(new ScriptBundle("~/bundles/prettify").Include(
                                         "~/Scripts/prettify.js"));

            //Bundle for testing of UiDateTimeModel as a Parameter
            bundles.Add(new ScriptBundle("~/bundles/testUiDateTimeModelAsParameter").Include(
                                         "~/Scripts/testUiDateTimeModelAsParameter*"));

            //CSS Bundles
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                                        "~/Content/bootstrap/bootstrap.css",
                                        "~/Content/bootstrap/bootstrap-responsive.css",
                                        "~/Content/bootstrap/nav-fix.css",
                                        "~/Content/bootstrap-local.css"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                                        "~/Content/themes/base/jquery.ui.core.css",
                                        "~/Content/themes/base/jquery.ui.resizable.css",
                                        "~/Content/themes/base/jquery.ui.selectable.css",
                                        "~/Content/themes/base/jquery.ui.accordion.css",
                                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                                        "~/Content/themes/base/jquery.ui.button.css",
                                        "~/Content/themes/base/jquery.ui.dialog.css",
                                        "~/Content/themes/base/jquery.ui.slider.css",
                                        "~/Content/themes/base/jquery.ui.tabs.css",
                                        "~/Content/themes/base/jquery.ui.datepicker.css",
                                        "~/Content/themes/base/jquery.ui.progressbar.css",
                                        "~/Content/jquery-ui-timepicker-addon.css",
                                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}