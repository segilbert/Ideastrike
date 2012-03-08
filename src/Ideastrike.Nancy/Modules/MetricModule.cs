//
using System;
using System.Collections.Generic;
using System.Linq;
//
using Ideastrike.Nancy.Models;
using Ideastrike.Nancy.Models.Repositories;
//
using Ideastrike.Nancy.Models.ViewModels;
using Nancy;

namespace Ideastrike.Nancy.Modules
{
    public class MetricModule : NancyModule
    {
        private readonly IIdeaRepository _ideas;
        private readonly IUserRepository _users;
        private readonly ISettingsRepository _settings;
        
        public MetricModule(IIdeaRepository ideas, IUserRepository users, ISettingsRepository settings) : base("/metric")
        {
            _ideas = ideas;
            _users = users;
            _settings = settings;

            Get["/"] = _ => ListIdeas(_ideas.GetAll().GroupBy(a => a.Author).Select(result => new MetricViewModel() { Name = result.Key.UserName, Count = result.Count()}), MetricSelectedTab.MostInnovations, "");

            //Ideas + Votes + ACtivites + Features
            //Get["/active"] = _ => ListIdeas(_ideas.GetAll().GroupBy(a => a.Author).Select(result => new MetricViewModel() { Name = result.Key.UserName, Count = result.Count() }), MetricSelectedTab.MostActive, "");

            Get["/votes"] = _ => ListIdeas(_ideas.GetAll().GroupBy(a => a.Author).Select(result => new MetricViewModel() { Name = result.Key.UserName, Count = result.Key.Votes.Count }), MetricSelectedTab.MostVotes, "");
        }

        public Response ListIdeas(IEnumerable<MetricViewModel> metric, MetricSelectedTab selected, string ErrorMessage)
        {
            var m = Context.Model(_settings.Title);
            m.Name = _settings.Name;
            m.WelcomeMessage = _settings.WelcomeMessage;
            m.Metrics = metric;
            m.Selected = selected;
            m.IdeaCount = metric.Sum(c => c.Count);
            m.ErrorMessage = ErrorMessage;

            return View["Metric/Index", m];
        }
    }
}