using ChatApp.Models.Message;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> s_messages = new List<KeyValuePair<string, string>>();

        [HttpGet]
        public IActionResult Show()
        {
            if (s_messages.Count() < 1)
            {
                return View(new ChatViewModel());
            }

            ChatViewModel chatModel = new ChatViewModel()
            {
                Messages = s_messages
                .Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    MessageText = m.Value
                }).ToList()
            };
            
            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chatModel)
        {
            s_messages.Add(new KeyValuePair<string, string>(chatModel.CurrentMessage.Sender, chatModel.CurrentMessage.MessageText));

            return RedirectToAction(nameof(Show));
        }
    }
}
