using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;


namespace AudioTest
{
    public partial class Form1 : Form
    {
        bool completed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SpeechScriptInterpreter interpreter = new SpeechScriptInterpreter();
                //Call Initialize on the interpreter
            interpreter.init();
                //Call parse so we can hear it talk! Hopefully it can parse the test file. :D.
            interpreter.parse("Questionaire.txt");


                //Old stuff keeping as example below

            /*
            SpeechSynthesizer reader = new SpeechSynthesizer();
            reader.SelectVoice("IVONA 2 Salli");
            PromptBuilder b = new PromptBuilder();


            PromptStyle s = new PromptStyle(PromptRate.ExtraFast);
            s.Rate = PromptRate.Medium;
            s.Volume = PromptVolume.Loud;
            s.Emphasis = PromptEmphasis.Strong;
            PromptRate.


            
            b.StartSentence();
            b.StartStyle(s);
            b.AppendText("Hello Iridescent");
            b.EndStyle();
            b.AppendBreak(new TimeSpan(0, 0, 2));
            s.Emphasis = PromptEmphasis.Reduced;
            s.Rate = PromptRate.Fast;
            b.StartStyle(s);
            b.AppendText("This is less emphasized");
            b.EndStyle();
            b.EndSentence();
            
           

            b.AppendBreak(new TimeSpan(0, 0, (int) 1.5));
            b.StartSentence();
            b.AppendText("How", PromptEmphasis.Moderate);
            b.AppendText("Are", PromptVolume.Loud);
            b.AppendText("You?", PromptEmphasis.Strong);
            
            b.AppendText("WEEEEEEEEEEEEEEEEEEE");
            b.EndSentence();


            reader.Speak(b);
            

           // b.StartStyle(c );
           // b.EndStyle();
            b.StartSentence();
            b.AppendText("Hey");
            b.EndSentence();
            b.AppendBreak(new TimeSpan(0, 0, (int)3));

          
            reader.Speak(b);
            reader.SpeakCompleted += compl;
            reader.Speak(b);
           

           
            



            List<string> vnames = new List<string>();
            foreach (InstalledVoice voice in reader.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                //Console.WriteLine(" Voice Name: " + info.Name);
                vnames.Add(info.Name);
            }
            Console.WriteLine("hello");
           
            reader.Speak(b);
            


            for (int i=-10; i<10; i++)
            {
                reader.Rate = i;
                reader.Speak("You will obey");
                reader.Rate = 5;
                
            }
            
           
            */
            
            

        }

        private void compl(object sender, SpeakCompletedEventArgs e)
        {
            completed = true;
        }

    }
}
