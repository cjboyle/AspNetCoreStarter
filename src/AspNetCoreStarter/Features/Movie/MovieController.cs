using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Features.Movie
{
    [AutoValidateAntiforgeryToken]
    [Route("/[controller]/[action]")]
    public class MovieController : Controller
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Index.Query query)
        {
            var movies = await _mediator.Send(query);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createViewModel = await _mediator.Send(new CreateEdit.Query());
            return View(createViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEdit.Command command)
        {
            if (ModelState.IsValid)
            {
                var movieId = await _mediator.Send(command);
                if (movieId > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(CreateEdit.Query query)
        {
            var createViewModel = await _mediator.Send(query);
            return View(createViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateEdit.Command command)
        {
            if (ModelState.IsValid)
            {
                var movieId = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }


    }
}