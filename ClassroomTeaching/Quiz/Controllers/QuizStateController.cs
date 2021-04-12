using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    public class QuizStateController : ApiController
    {
        private readonly IInteractionStateService _interactionStateService = null;

        public QuizStateController(IInteractionStateService interactionStateService)
        {
            _interactionStateService = interactionStateService;
        }

        [HttpGet]
        public QuizState Get(String id)
        {
            return _interactionStateService.GetQuizState(id);
        }
    }
}
