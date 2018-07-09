using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.GoogleAnalytics
{
    /// <summary>
    /// Google Analytics plugin
    /// </summary>
    public class GoogleAnalyticsPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly GoogleAnalyticsSettings _googleAnalyticsSettings;

        #endregion

        #region Ctor

        public GoogleAnalyticsPlugin(ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService,
            GoogleAnalyticsSettings googleAnalyticsSettings)
        {
            this._localizationService = localizationService;
            this._webHelper = webHelper;
            this._settingService = settingService;
            this._googleAnalyticsSettings = googleAnalyticsSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HeadHtmlTag };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsGoogleAnalytics/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsGoogleAnalytics";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new GoogleAnalyticsSettings
            {
                GoogleId = "UA-0000000-0",
                TrackingScript = @"<!-- Global site tag (gtag.js) - Google Analytics -->
                <script async src='https://www.googletagmanager.com/gtag/js?id={GOOGLEID}'></script>
                <script>
                  window.dataLayer = window.dataLayer || [];
                  function gtag(){dataLayer.push(arguments);}
                  gtag('js', new Date());

                  gtag('config', '{GOOGLEID}');
                  {CUSTOMER_TRACKING}
                  {ECOMMERCE_TRACKING}
                </script>",
                UseJsToSendEcommerceInfo = true
            };
            _settingService.SaveSetting(settings);

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.GoogleId", "ID");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.GoogleId.Hint", "Enter Google Analytics ID.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.TrackingScript", "Tracking code");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.TrackingScript.Hint", "Paste the tracking code generated by Google Analytics here. {GOOGLEID} and {CUSTOMER_TRACKING} will be dynamically replaced.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.EnableEcommerce", "Enable eCommerce");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.EnableEcommerce.Hint", "Check to pass information about orders to Google eCommerce feature.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.UseJsToSendEcommerceInfo", "Use JS to send eCommerce info");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.UseJsToSendEcommerceInfo.Hint", "Check to use JS code to send eCommerce info from the order completed page. But in case of redirection payment methods some customers may skip it. Otherwise, e-commerce information will be sent using HTTP request. Information is sent each time an order is paid but UTM is not supported in this mode.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludeCustomerId", "Include customer ID");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludeCustomerId.Hint", "Check to include customer identifier to script.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludingTax", "Include tax");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludingTax.Hint", "Check to include tax when generating tracking code for eCommerce part.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.Instructions", "<p>Google Analytics is a free website stats tool from Google. It keeps track of statistics about the visitors and eCommerce conversion on your website.<br /><br />Follow the next steps to enable Google Analytics integration:<br /><ul><li><a href=\"http://www.google.com/analytics/\" target=\"_blank\">Create a Google Analytics account</a> and follow the wizard to add your website</li><li>Copy the Tracking ID into the 'ID' box below</li><li>Click the 'Save' button below and Google Analytics will be integrated into your store</li></ul><br />If you would like to switch between Google Analytics (used by default) and Universal Analytics, then please use the buttons below:</p>");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<GoogleAnalyticsSettings>();

            //locales
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.GoogleId");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.GoogleId.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.TrackingScript");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.TrackingScript.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.EnableEcommerce");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.EnableEcommerce.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.UseJsToSendEcommerceInfo");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.UseJsToSendEcommerceInfo.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludeCustomerId");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludeCustomerId.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludingTax");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.IncludingTax.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.GoogleAnalytics.Instructions");

            base.Uninstall();
        }

        #endregion
    }
}