//
using System.Web.Configuration;
using Ideastrike.Nancy.Models;
//
using Nancy;

namespace Ideastrike.Nancy.Modules
{
    public class SupportModule: NancyModule
    {
        private readonly IUserRepository _users;
        private readonly ISettingsRepository _settings;

        public SupportModule(IUserRepository users, ISettingsRepository settings)
            : base("/support")
        {
            _users = users;
            _settings = settings;
            
            Get["/"] = _ =>
            {
                var m = Context.Model(settings.Title);
                m.Name = settings.Name;
                string emailsTo = WebConfigurationManager.AppSettings["adminLinkTo"].ToString();
                m.AdminLinkTo = string.Format("mailto:{0}?Subject=IdeaStrike%20Question", emailsTo);
                m.HomePage = settings.HomePage;
                m.WelcomeMessage = _settings.WelcomeMessage;
                return View["Support/Index", m];
                
            };
        }
    }
}