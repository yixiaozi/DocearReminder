using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Config;

namespace DocearReminder
{
    public partial class ChatGPT : Form
    {
        string OpenAIKey = "";
        public ChatGPT()
        {
            OpenAIKey = DocearReminderForm.ini.ReadString("path", "OpenAIKey", "");
            InitializeComponent();
        }

        private async void ask_button_ClickAsync(object sender, EventArgs e)
        {
            //ask openai the question text box.
            OpenAIAPI api = new OpenAIAPI(OpenAIKey);
            //https://github.com/OkGoDoIt/OpenAI-API-dotnet
            var chat = api.Chat.CreateConversation();

            /// give instruction as System
            chat.AppendSystemMessage("You are an electronic secretary, please answer the owner's questions seriously.");

            // give a few examples as user and assistant
            chat.AppendUserInput(question.Text);
            results_richtext.AppendText(question.Text + Environment.NewLine); // "Yes"

            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();
            results_richtext.AppendText(response);

            // the entire chat history is available in chat.Messages
            foreach (ChatMessage msg in chat.Messages)
            {
                results_richtext.AppendText($"{msg.Role}: {msg.Content}");
            }
            results_richtext.AppendText(Environment.NewLine); 
        }
        private async void Demo(object sender, EventArgs e)
        {
            //ask openai the question text box.
            OpenAIAPI api = new OpenAIAPI(OpenAIKey);
            //https://github.com/OkGoDoIt/OpenAI-API-dotnet
            var chat = api.Chat.CreateConversation();

            /// give instruction as System
            chat.AppendSystemMessage("You are a teacher who helps children understand if things are animals or not.  If the user tells you an animal, you say \"yes\".  If the user tells you something that is not an animal, you say \"no\".  You only ever respond with \"yes\" or \"no\".  You do not say anything else.");

            // give a few examples as user and assistant
            chat.AppendUserInput("Is this an animal? Cat");
            chat.AppendExampleChatbotOutput("Yes");
            chat.AppendUserInput("Is this an animal? House");
            chat.AppendExampleChatbotOutput("No");

            // now let's ask it a question'
            chat.AppendUserInput("Is this an animal? Dog");
            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();
            results_richtext.AppendText(response); // "Yes"

            // and continue the conversation by asking another
            chat.AppendUserInput("Is this an animal? Chair");
            // and get another response
            response = await chat.GetResponseFromChatbotAsync();
            results_richtext.AppendText(response); // "No"

            // the entire chat history is available in chat.Messages
            foreach (ChatMessage msg in chat.Messages)
            {
                results_richtext.AppendText($"{msg.Role}: {msg.Content}");
            }

        }
    }
}
