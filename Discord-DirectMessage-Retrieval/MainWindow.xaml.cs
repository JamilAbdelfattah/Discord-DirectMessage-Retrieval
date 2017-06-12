using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GhettoDiscordWPF
{
    public partial class MainWindow : Window
    {

        int previousUser = 999999;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PopulateCombo_Click(object sender, RoutedEventArgs e)
        {
            DiscordWebClient Discord = new DiscordWebClient(AuthBox.GetLineText(0) + AuthBox.GetLineText(1));
            GuildChannel[] channels = Discord.GetDMChannels();
            AuthLabel.Content = ("Authorization for " + Discord.GetUser() + ":");

            ListOfDM.Items.Clear();
            for (int i = 0; i <= channels.Length - 2; i++)
            {
                try
                    {ListOfDM.Items.Add(channels[i].recipients[0].username);}
                catch
                    { }
            }
        }

        private void MessagesButton_Click(object sender, RoutedEventArgs e)
        {
            DiscordWebClient Discord = new DiscordWebClient(AuthBox.GetLineText(0) + AuthBox.GetLineText(1));
            GuildChannel[] channels = Discord.GetDMChannels();
            int user = 0;

            for (int i = 0; i <= channels.Length; i++)
            {
                try
                {
                    if (channels[i].recipients[0].username == ListOfDM.SelectedValue.ToString())
                    {
                        user = i;
                        i = 99999;
                    }
                }
                catch
                {
                    return;
                }
            }

            string channelId = channels[user].id;
            string last = channels[user].last_message_id;

            if (previousUser != user)
            {
                Messages[] Messages = Discord.GetDMMessages(channelId, last);
                MessageContainer.Items.Clear();
                for (int i = 0; i <= Messages.Length; i++)
                {
                    try
                    {
                        MessageContainer.Items.Add(DateTime.Parse(Messages[i].timestamp)+ "\t" + Messages[i].author.username + ":  " + Messages[i].content);
                    }

                    catch
                    {
                        if (Messages.Length > 0)
                        {
                            MessageContainer.Items.Add("End of the line..... CHOO.... CHOOOOOO === \n" + "\n             .---- - -" + "\n           (   ,----- - -" + "\n           \\_//     ___" + "\n          c--U---^--'o  [_" + "\n          |------------'_| " + "\n         /_(o)(o)--(o)(o)" + "\n   ~ ~~~~~~~~~~~~~~~~~~~~~~~~ ~");
                            MessageContainer.Items.Add("\n" + Messages[i - 1].id);
                            previousUser = user;
                        }

                        else
                        {
                            MessageContainer.Items.Add("Unknown error, Somthing to do with the return packet is off.");
                            previousUser = 999999;
                        }
                    }
                }
            }

            else if (previousUser == user)
            {
                Messages[] Messages = Discord.GetDMMessages(channelId, MessageContainer.Items[MessageContainer.Items.Count - 1].ToString());
                for (int i = 0; i <= Messages.Length; i++)
                {
                    try
                    {
                        MessageContainer.Items.Add(DateTime.Parse(Messages[i].timestamp) + "\t" + Messages[i].author.username + ":  " + Messages[i].content);
                    }

                    catch
                    {
                        if (Messages.Length > 0)
                        {
                            MessageContainer.Items.Add("End of the line..... CHOO.... CHOOOOOO === \n" + "\n             .---- - -" + "\n           (   ,----- - -" + "\n           \\_//     ___" + "\n          c--U---^--'o  [_" + "\n          |------------'_| " + "\n         /_(o)(o)--(o)(o)" + "\n   ~ ~~~~~~~~~~~~~~~~~~~~~~~~ ~");
                            MessageContainer.Items.Add(Messages[i - 1].id);
                        }
                        
                        else
                        {
                            MessageContainer.Items.Add("No mas.       \n i.e it's finished you've seen the who shabang.");
                            MessageContainer.Items.Add(MessageContainer.Items[MessageContainer.Items.Count - 2].ToString());
                        }


                        previousUser = user;
                    }
                }
            }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = "Text (*.txt)|*.txt";
            if (saveFile.ShowDialog() == true)
            {
                using (var sw = new StreamWriter(saveFile.FileName, false))
                    foreach (var item in MessageContainer.Items)
                        sw.Write(item.ToString() + Environment.NewLine);
                MessageBox.Show("Success");
            }
        }

        private void AuthBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

