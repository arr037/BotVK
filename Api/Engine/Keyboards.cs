using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine
{
    public static class Keyboards
    {
        public static Button[][] HelpKeyboard =>
            new Button[][]
            {
                new Button[]
                {
                    new Button
                    {
                        Action = new Models.Action
                        {
                            Type="text",
                            Payload="{\"button\": \"1\"}",
                            Label="Создать группу"
                        },
                        Color="positive",

                    },
                    new Button
                    {
                        Action = new Models.Action
                        {
                            Type="text",
                            Payload="{\"button\": \"1\"}",
                            Label="Вступить в группу"
                        },
                        Color="primary",

                    }
                }
            };

        public static Button[][] ListGroup =>
            new Button[][]
            {
                new Button[]
                {
                    new Button
                    {
                        Action = new Models.Action
                        {
                            Type="text",
                            Payload="{\"button\": \"1\"}",
                            Label="Список группы"
                        },
                        Color="positive",

                    },
                }
            };



    }
}
