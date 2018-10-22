using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace AudioTest
{
    public class SpeechScriptInterpreter
    {
        //Set everything up, read the sample file
        String[] lines;
        Dictionary<string, PromptEmphasis> Emphasisvalues = new Dictionary<string, PromptEmphasis>();
        Dictionary<string, PromptRate> Ratevalues = new Dictionary<string, PromptRate>();
        public SpeechSynthesizer reader = new SpeechSynthesizer();
        PromptBuilder builder = new PromptBuilder();
        PromptStyle style_ = new PromptStyle();
        Dictionary<int, string> random_lines = new Dictionary<int, string>();
        List<String> Deepener_names = new List<string>();
        List<Anchor> File_Anchors = new List<Anchor>();
        public int index = 0;

        public void init()
        {
            //fill the lookups for match cases with external script
            Emphasisvalues.Add("Moderate", PromptEmphasis.Moderate);
            Emphasisvalues.Add("None", PromptEmphasis.None);
            Emphasisvalues.Add("NotSet", PromptEmphasis.NotSet);
            Emphasisvalues.Add("Reduced", PromptEmphasis.Reduced);
            Emphasisvalues.Add("Strong", PromptEmphasis.Strong);

            Ratevalues.Add("ExtraFast", PromptRate.ExtraFast);
            Ratevalues.Add("ExtraSlow", PromptRate.ExtraSlow);
            Ratevalues.Add("Fast", PromptRate.Fast);
            Ratevalues.Add("Medium", PromptRate.Medium);
            Ratevalues.Add("NotSet", PromptRate.NotSet);
            Ratevalues.Add("Slow", PromptRate.Slow);

            //Fill Deepener names
            Deepener_names.Add("Deepener_1.txt");
            Deepener_names.Add("Deepener_2.txt");
            string query = "<QueryMulti:<Anchor1>,<Anchor2>,<Anchor3>,<Anchor4>,Name1,Name2,Name3,Name4>>";
            //QueryMulti(query);
            
            

            List<String> vnames = new List<String>();
            foreach (InstalledVoice voice in reader.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                //Console.WriteLine(" Voice Name: " + info.Name);
                vnames.Add(info.Name);
            }

            string use_name = "";
            foreach(string n in vnames)
            {
                if (n.Contains("Zira"))
                {
                    string name = n;
                    use_name = n;
                    break;
                }
            }

            //reader.SelectVoice("IVONA 2 Salli");
            //reader.SelectVoice("Microsoft Anna");
            //reader.SelectVoice(use_name);

            QueryMulti selectvoice = new QueryMulti();
            string[] voices = vnames.ToArray();
            selectvoice.One.Text = voices[0];
            selectvoice.Two.Text = voices[1];
            selectvoice.Three.Text = voices[2];
            selectvoice.Four.Text = voices[3];
            selectvoice.Text = "Select a voice";
            selectvoice.ShowDialog();
            string voiceselected = selectvoice.returnvalue;

            switch (voiceselected)
            {
                case "One":
                    reader.SelectVoice(voices[0]);
                    break;
                case "Two":
                    reader.SelectVoice(voices[1]);
                    break;
                case "Three":
                    reader.SelectVoice(voices[2]);
                    break;
                case "Four":
                    reader.SelectVoice(voices[3]);
                    break;

            }
            //reader.SelectVoice("IVONA 2 Emma");
            //reader.SelectVoice("Microsoft Anna");
            
        }

        //Allows this object class to easily say anything
        //Without providing specifics.
        public void say_default(string input)
        {
            string voice_string = input;
            //reader.SelectVoice("IVONA 2 Salli");
            style_.Rate = PromptRate.Medium;
            style_.Emphasis = PromptEmphasis.NotSet;
            builder.ClearContent();
            builder.StartSentence();
            builder.StartStyle(style_);
            builder.AppendText(voice_string);
            builder.EndStyle();
            builder.EndSentence();
            //lastly let the ball roll and see if we can hear some chatter.
            reader.Speak(builder);
        }

        public void Parse_Anchors()
        {
            int currentindex = 0;
            foreach (string l in lines)
            {
                if (l.StartsWith("<Anchor:")) {
                    string Anchor_Name = l.Substring(8);
                    Anchor tempanchor = new Anchor();
                    tempanchor.line_number = currentindex;
                    tempanchor.name = Anchor_Name;
                    //now account for that floating '>' in tempanchor.name
                    tempanchor.name = tempanchor.name.Substring(0, tempanchor.name.Length - 1);

                    File_Anchors.Add(tempanchor);

                }
                currentindex += 1;
            }
        }

            //Making the FetchAnchor Command more generic.
        public Anchor FetchAnchor(string input, string command)
        {
            int substring_length = 0;
            if (command == "Goto") { substring_length = 6; }
            if (command == "QueryGeneric") { substring_length = 0; }
            string Goto_Anchor = input.Substring(substring_length);
                //Special Case for Goto string extraction.
            if (command == "Goto")
            {
                Goto_Anchor = Goto_Anchor.Substring(0, Goto_Anchor.Length - 1);
            }
            //Perform a search
            Anchor return_anchor = new Anchor();
            return_anchor.name = "!";

            foreach (Anchor A in File_Anchors)
            {
                if (A.name == Goto_Anchor)
                {
                    return_anchor = A;
                    break;

                }
            }
            return return_anchor;
         }

        public void QueryMulti(string input)
        {

            //Example Query
                //<QueryMulti:<Anchor1>,<Anchor2>,<Anchor3>,<Anchor4>,Name1,Name2,Name3,Name4>>
            string QueryStart = "<QueryMulti:";
            int length = QueryStart.Length;
            string QueryRaw = input.Substring(length);
            string[] split = QueryRaw.Split(',');
            string[] Anchors = new string[4];
            string[] DisplayNames = new string[4];

                //Extract Display names and Anchor names
            for (int i=0; i<4; i++)
            {
                if (i <= 4) {
                    Anchors[i] = split[i].Substring(1, split[i].Length - 2);
                    DisplayNames[i] = split[i + 4];
                    if (i == 3)
                    {
                        DisplayNames[i] = DisplayNames[i].Substring(0, DisplayNames[i].Length - 2);
                    }
                }
             
            }

            //Create the new Dialoge
            String return_option = "error";
            QueryMulti multiquery = new QueryMulti();
                //Set the display name for the buttons and presto.
            multiquery.One.Text = DisplayNames[0];
            multiquery.Two.Text = DisplayNames[1];
            multiquery.Three.Text = DisplayNames[2];
            multiquery.Four.Text = DisplayNames[3];
            multiquery.ShowDialog();
            string returnvalue = multiquery.returnvalue;

            //Now we check what was clicked
                //And also create a default anchor to return to.
            Anchor targetanchor = new Anchor();
            switch (returnvalue)
            {
                case "One":
                    targetanchor = FetchAnchor(Anchors[0], "QueryGeneric");
                    break;
                case "Two":
                    targetanchor = FetchAnchor(Anchors[1], "QueryGeneric");
                    break;
                case "Three":
                     targetanchor = FetchAnchor(Anchors[2], "QueryGeneric");
                    break;
                case "Four":
                    targetanchor = FetchAnchor(Anchors[3], "QueryGeneric");
                    break;
            }

            Console.WriteLine("Stuff");

                //We have found our anchor
            if (targetanchor.name != "!")
            {
                int Anchorlineindex = targetanchor.line_number;
                //Now we will do the jump
                index = Anchorlineindex;
                //Jump performed.
            }

            Console.WriteLine("Stuff");
           

        }

        public void HandleQuery(string input)
        {
            string QueryString = input.Substring(14);
                //Now eliminate that annoying floating '>' once again =p
            QueryString = QueryString.Substring(0, QueryString.Length - 1);
            string[] anchors = QueryString.Split(',');
            string AnchorYes = anchors[0];
            string AnchorNo = anchors[1];


            String command = "QueryGeneric";
                
                //Now Get the Anchor Objects to reference line numbers
            Anchor Anchor_Y = FetchAnchor(AnchorYes, command);
            Anchor Anchor_N = FetchAnchor(AnchorNo, command);

                //Create The Dialoge for the Generic Query
            Dialouge GenericQuery = new Dialouge();
            GenericQuery.ShowDialog();
            if (GenericQuery.ReturnValue == "Yes")
            {
                int line_index = Anchor_Y.line_number;
                index = line_index;
            }

            if (GenericQuery.ReturnValue == "No")
            {
                int line_index = Anchor_N.line_number;
                index = line_index;
            }
        }
        
            //parse the entire thing out and say the stuff.
        public void parse(string filename)
        {
            //Load the file
            lines = System.IO.File.ReadAllLines(filename);

            //Parse the anchors
            Parse_Anchors();

           
            while(index< lines.Length) { 

                string input = lines[index];
                index++;

                    //Parse the context
                        if (input.StartsWith("SetRate:"))
                {
                    string rate = input.Substring(8);
                    if (Ratevalues.ContainsKey(rate))
                    {
                        PromptRate  pmt_rate = Ratevalues[rate];
                        style_.Rate = pmt_rate;
                    }
                   
                    
                }

                        if (input.StartsWith("SetEmp:"))
                {
                    string Empsetting = input.Substring(7);
                    if (Emphasisvalues.ContainsKey(Empsetting)){
                        PromptEmphasis emp_val = Emphasisvalues[Empsetting];
                        style_.Emphasis = emp_val;
                    }


                }
                        if (input.StartsWith("<Deepener>"))
                {
                    //Make note of the deepender here.
                    if (!random_lines.ContainsKey(index))
                    {
                        random_lines.Add(index, "<Deepener>");
                    }
                    //now we will run a Deepener script here.

                    int Deepener_Count = Deepener_names.Count();
                    Random r = new Random();
                    int next_Deepener = r.Next(Deepener_Count);
                    string Deepener_name = Deepener_names[next_Deepener];
                        //Some recursive magic now
                    SpeechScriptInterpreter Deepener_interpreter = new SpeechScriptInterpreter();
                    Deepener_interpreter.init(); //Initialize the new interpreter instance
                    Deepener_interpreter.parse(Deepener_name);

                }

                        if (input.StartsWith("<Goto:"))
                {
                    string command = "Goto";
                    //Get the anchor object.
                    Anchor A = FetchAnchor(input, command);

                        //The '!' character is reserved and anchors cannot use it
                            //It is used to denote a failed fetch. To prevent NullErrors.
                                
                    if (A.name != "!")
                    {
                        int line_index = A.line_number;
                            //Assign indirectly the index of this trance to the Anchor index.
                        index = line_index;
                    }



                }

                    //Change inputs by setting functions for them for more efficient parsing and
                        //Templating of code blocks.
                 if (input.StartsWith("<QueryMulti:"))
                {
                    QueryMulti(input);

                }

                        if (input.StartsWith("<QueryDeepEnough>"))
                {
                    Dialouge get_state = new Dialouge();
                    say_default("Are you deep enough yet?");
                    get_state.ShowDialog();
                        //Find out yes or no
                    string result = get_state.ReturnValue;
                    if (result == "Yes") { continue; }
                    if (result == "No")
                    {
                        for (int i=index; i>0; i--)
                        {
                            //search backwards for the previous Deepener
                                    //Did we find a '<Deepener> tag?
                                if (random_lines.ContainsKey(i))
                            {
                             if (random_lines[i] == "<Deepener>")
                                {
                                    //does it match?
                                    //if it does, we go backwards in the script
                                    //All the way to this point
                                    index = i-1;
                                    break; //We want to quit the search

                                        
                                }
                            }
                            

                        }

                    }

                }

                        //Generic Query
                        if (input.StartsWith("<QueryGeneric:"))
                        {
                        HandleQuery(input);    
                        

                        }

                        
                            //Say the stuff, treating our rate and emphasis as part of a state machine
                                //Which we re-apply each time something is to be spoken.
                        if (input.StartsWith("Say:"))
                {
                    string voice_string = input.Substring(4);
                    builder.ClearContent();
                    builder.StartSentence();
                    builder.StartStyle(style_);
                    builder.AppendText(voice_string);
                    builder.EndStyle();
                    builder.EndSentence();
                        //lastly let the ball roll and see if we can hear some chatter.
                    reader.Speak(builder);

                }
            }
        }

    }
}


/*
 * 
Params for Emphasis:
Moderate
None
NotSet
Reduced
Strong

Params for speed/rate:
ExtraFast
ExtraSlow
Fast
Medium
NotSet
Slow

    */
