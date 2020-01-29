using Api.Engine;
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public abstract class ModifierBase
    {
        public bool Run(SimleApiRequest request,State state, out ApiResponse response)
        {
            if (!Check(request, state))
            {
                response = null;
                return false;
            }
            var simple = Respont(request, state);

            response = new ApiResponse
            {
                Text = simple.Text,
                UserId = simple.UserId,
                KeyBoard= simple?.KeyBoard
            };
            return true;
        }

        protected abstract bool Check(SimleApiRequest request, State state);
        protected abstract ApiResponse Respont(SimleApiRequest request, State state);
        protected virtual string GenerateKeyBoard(Button[][] buttons,bool flag=true)
        {
            var keyboard = new Keyboard();
            keyboard.OneTime = flag;
            keyboard.Buttons = buttons;
            var stringKeyBoard = JsonConvert.SerializeObject(keyboard);

            return stringKeyBoard;
        }



    }
}
