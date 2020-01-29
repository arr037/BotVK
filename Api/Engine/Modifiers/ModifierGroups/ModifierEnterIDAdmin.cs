using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierEnterIDAdmin : ModifierBase
    {
        protected override bool Check(SimleApiRequest request, State state)
        {
            return state.Step == Step.AwaitAddNewAdmin;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            using var db = new DatabaseContext();
            long value=0;
            var ch = long.TryParse(request.Text, out value) ;

            if (ch && value!=0)
            {
                var checkuser = db.Users.Where(x => x.Id == value).Select(x => x.Group.Name == state.User.Group.Name).FirstOrDefault();
                
                if (!checkuser)
                {
                    return new ApiResponse
                    {
                        Text = "Вы не можете добавить этого пользователя, так как он не состоит в группе",
                        UserId = request.PeerId,
                        KeyBoard = GenerateKeyBoard(Keyboards.ListGroup)
                    };
                }

                state.User.Group.Admins.Add(new Admin
                {
                    UserId = value
                });

                db.Update(state.User);
                db.SaveChanges();

                state.Step = Step.None;
                return new ApiResponse
                {
                    Text = "Пользователь успешно добавлен в администраторы",
                    UserId=request.PeerId,
                    KeyBoard= GenerateKeyBoard(Keyboards.HelpKeyboard)
                };
            }

            return new ApiResponse
            {
                Text = "Я не могу распознать ID\n\n" +
                "Повторите попытку",
                UserId= request.PeerId
            };
        }
    }
}
