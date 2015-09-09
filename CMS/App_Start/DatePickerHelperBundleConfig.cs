using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(CMS.App_Start.DatePickerHelperBundleConfig), "RegisterBundles")]

namespace CMS.App_Start
{
	public class DatePickerHelperBundleConfig
	{
		public static void RegisterBundles()
		{
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            "~/Scripts/bootstrap-datepicker.js",
            "~/Scripts/locales/bootstrap-datepicker.*"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/datepicker").Include(
            "~/Content/bootstrap-datepicker.css"));
		}
	}
}