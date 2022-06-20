using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using System.Threading.Tasks;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsController: Controller
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
